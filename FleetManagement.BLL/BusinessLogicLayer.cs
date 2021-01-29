using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    /// <summary>
    /// The BLL assembly handle, for the registrations in the service container.
    /// </summary>
    public class BusinessLogicLayer { }



    #region RESPONSES
    public interface IBusinessRuleListenerResponse
    {
        public IList<IBusinessRuleFailure> Failures { get; set; }
        public bool Success { get; }
    }

    public class BusinessRuleListenerResponse : IBusinessRuleListenerResponse
    {
        public bool Success => Failures.Count is 0;
        public IList<IBusinessRuleFailure> Failures { get; set; } = new List<IBusinessRuleFailure>();
    }

    public class BusinessRuleFailure : IBusinessRuleFailure
    {
        public string Rule { get; set; }
        public string ErrorMessage { get; set; }
    }

    public interface IBusinessRuleFailure
    {
        public string Rule { get; set; }
        public string ErrorMessage { get; set; }
    }

    public interface IBusinessRuleValidatorResponse
    {
        public bool Success { get; }
        public IBusinessRuleListenerResponse Responses { get; }
    }

    public class BusinessRuleValidatorResponse : IBusinessRuleValidatorResponse
    {
        public IBusinessRuleListenerResponse Responses { get; init; } = new BusinessRuleListenerResponse();

        public static BusinessRuleValidatorResponse Empty => new BusinessRuleValidatorResponse { };

        public bool Success => Responses.Failures.Count is 0;
    }
    #endregion

    public interface IBusinessRuleValidator
    {
        Task<IBusinessRuleListenerResponse> Validate<T>(T contract, CancellationToken token = default) where T : IContract;
    }

    public class BusinessRuleValidator : IBusinessRuleValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public BusinessRuleValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IBusinessRuleListenerResponse> Validate<T>(T contract, CancellationToken token = default) where T : IContract
        {
            var businessRuleHandler = _serviceProvider.GetService<IBusinessRuleValidator<T>>();

            return await businessRuleHandler.ValidateBusinessRules(contract, token);
        }
    }

    public interface IBusinessRuleValidator<T> where T : IContract
    {
        Task<IBusinessRuleListenerResponse> ValidateBusinessRules(T contract, CancellationToken token = default);
    }

    public class BusinessRuleValidator<T> : IBusinessRuleValidator<T> where T : IContract
    {
        private readonly List<IBusinessRule<T>> _businessRules = new List<IBusinessRule<T>>();
        private readonly IBusinessRuleListener<T> _businessRuleListener;
        private readonly IServiceProvider _provider;

        public BusinessRuleValidator(IServiceProvider provider)
        {
            _provider = provider;
            _businessRules = GetBusinessRulesBasedOnContract().ToList();

            if (_businessRules.Any())
                _businessRuleListener = new BusinessRuleListener<T>(_businessRules.ToArray());
            else
                throw new Exception("The are no business rules defined.");
        }

        public async Task<IBusinessRuleListenerResponse> ValidateBusinessRules(T contract, CancellationToken token = default)
        {
            foreach (var rule in _businessRules)
                await rule.Handle(contract, token);

            return _businessRuleListener.Speak();
        }

        public async Task Handle(Func<Task> successFunction)
        {
            if (_businessRuleListener.Success)
                await successFunction?.Invoke();
        }

        #region PRIVATE
        private IEnumerable<IBusinessRule<T>> GetBusinessRulesBasedOnContract()
        {
            var businessRules = LocateBusinessRules(Assembly.GetExecutingAssembly());

            return CreateBusinessRuleInstances(businessRules);
        }

        private IEnumerable<IBusinessRule<T>> CreateBusinessRuleInstances(IEnumerable<Type> businessRules)
        {
            foreach (var rule in businessRules)
                yield return ActivatorUtilities.CreateInstance(_provider, rule) as IBusinessRule<T>;
        }

        private static IEnumerable<Type> LocateBusinessRules(Assembly assembly)
        {
            var loadableTypes = new List<Type>();

            try
            {
                loadableTypes.AddRange(assembly.GetTypes());
            }
            catch (ReflectionTypeLoadException ex)
            {
                loadableTypes.AddRange(ex.Types.Where(type => type is not null));
            }

            var businessRuleInterface = typeof(IBusinessRule<T>);

            return loadableTypes
                .Where(businessRuleInterface.IsAssignableFrom)
                .Where(type => type.IsInterface is false)
                .ToList();
        }
        #endregion
    }

    public interface IBusinessRuleListener<T> where T : IContract
    {
        public bool Success { get; }
        public IBusinessRuleListenerResponse Speak();
    }

    public class BusinessRuleListener<T> : IBusinessRuleListener<T> where T : IContract
    {
        private readonly IDictionary<IBusinessRule<T>, List<string>> _failures;
        private readonly List<IBusinessRule<T>> _businessRules;

        public bool Success => _failures.Count is 0;

        public BusinessRuleListener(params IBusinessRule<T>[] businessRules)
        {
            _failures = new Dictionary<IBusinessRule<T>, List<string>>();
            _businessRules = new List<IBusinessRule<T>>();

            if (businessRules.Any())
            {
                _businessRules.AddRange(businessRules);

                foreach (var rule in _businessRules)
                    SubscribeToFailureEvent(rule);
            }
        }

        public IBusinessRuleListenerResponse Speak()
        {
            var response = new BusinessRuleListenerResponse();
            if (_failures.Any() is false) return response;

            response.Failures = new List<IBusinessRuleFailure>();
            foreach (var failure in _failures)
            {
                var businessRule = failure.Key;

                foreach (var failureMessage in _failures[businessRule])
                {
                    var businessRuleFailure = new BusinessRuleFailure()
                    {
                        ErrorMessage = failureMessage,
                        Rule  = businessRule.GetType().Name
                    };

                    response.Failures.Add(businessRuleFailure);
                }
            }


            return response;
        }

        #region PRIVATE
        private void SubscribeToFailureEvent(IBusinessRule<T> rule)
        {
            rule.OnFailure += OnFailureEventHandler;
        }

        protected virtual void OnFailureEventHandler(IBusinessRule<T> source, BusinessRuleFailureEventArgs arguments)
        {
            _failures.Add(source, arguments.Messages);
        }
        #endregion
    }

    public class LicensePlateCheckIfExists : IBusinessRule<ICreateLicensePlateContract>
    {
        public event BusinessRuleFailureEventHandler<ICreateLicensePlateContract> OnFailure;

        private readonly ILicensePlateRepository _licensePlateRepository;

        public LicensePlateCheckIfExists(ILicensePlateRepository licensePlateRepository)
        {
            _licensePlateRepository = licensePlateRepository;
        }

        public async Task Handle(ICreateLicensePlateContract contract, CancellationToken token = default)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, token);

            if (licensePlate is not null)
            {
                RaiseOnFailureEvent(new BusinessRuleFailureEventArgs
                {
                    Messages = { $"The license plate with given identifier {contract.Identifier} already exists." }
                });
            }
        }

        private void RaiseOnFailureEvent(BusinessRuleFailureEventArgs args)
        {
            OnFailure?.Invoke(this, args);
        }
    }

    public class LicensePlateValidation : IBusinessRule<ICreateLicensePlateContract>
    {
        public event BusinessRuleFailureEventHandler<ICreateLicensePlateContract> OnFailure;
        
        private readonly LicensePlateValidator _validator;
        
        public LicensePlateValidation(LicensePlateValidator validator)
        {
            _validator = validator;
        }

        public async Task Handle(ICreateLicensePlateContract contract, CancellationToken token = default)
        {
            var licensePlate = new LicensePlate { Identifier = contract.Identifier };

            var validation = await _validator.ValidateAsync(licensePlate, token);

            if (validation.IsValid is false)
            {
                var arguments = new BusinessRuleFailureEventArgs();

                foreach (var error in validation.Errors)
                    arguments.Messages.Add(error.ErrorMessage);

                RaiseOnFailureEvent(arguments);
            }
        }

        private void RaiseOnFailureEvent(BusinessRuleFailureEventArgs args)
        {
            OnFailure?.Invoke(this, args);
        }
    }

    public delegate void BusinessRuleFailureEventHandler<T>(IBusinessRule<T> source, BusinessRuleFailureEventArgs args) where T : IContract;

    public interface IBusinessRule<T> where T : IContract
    {
        event BusinessRuleFailureEventHandler<T> OnFailure;
        Task Handle(T contract, CancellationToken token = default);
    }

    public interface IContract
    {

    }

    public class BusinessRuleFailureEventArgs : EventArgs
    {
        public List<string> Messages { get; set; } = new List<string>();
    }
}
