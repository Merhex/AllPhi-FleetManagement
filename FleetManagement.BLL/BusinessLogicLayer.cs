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


    public interface IBusinessRuleHandler<T> where T : IContract
    {
        Task<IBusinessRuleListenerResponse> ValidateBusinessRules(CancellationToken token = default);
    }

    public interface IBusinessRuleListenerResponse
    {
        public IBusinessRuleFailureResponse Failures { get; }
        public bool Success { get; }
    }

    public class BusinessRuleListenerResponse : IBusinessRuleListenerResponse
    {
        public bool Success => Failures.Errors.Count is 0;
        public IBusinessRuleFailureResponse Failures { get; set; } = new BusinessRuleFailureResponse();
    }

    public class BusinessRuleFailureResponse : IBusinessRuleFailureResponse
    {
        public IList<IBusinessRuleFailure> Errors { get; set; } = new List<IBusinessRuleFailure>();
    }

    public interface IBusinessRuleFailureResponse
    {
        public IList<IBusinessRuleFailure> Errors { get; }
    }

    public class BusinessRuleFailure : IBusinessRuleFailure
    {
        public string Rule { get; set; }
        public string Error { get; set; }
    }

    public interface IBusinessRuleFailure
    {
        public string Rule { get; set; }
        public string Error { get; set; }
    }

    public interface IBusinessRuleHandlerResponse
    {
        public bool Success { get; }
        public IBusinessRuleFailureResponse Failures { get; }
    }

    public class BusinessRuleHandlerResponse : IBusinessRuleHandlerResponse
    {
        public IBusinessRuleFailureResponse Failures { get; init; } = new BusinessRuleFailureResponse();

        public static BusinessRuleHandlerResponse Empty => new BusinessRuleHandlerResponse { };

        public bool Success => Failures.Errors.Count is 0;
    }

    public class BusinessRuleHandlerProvider
    {
        private readonly IServiceProvider _provider;

        public BusinessRuleHandlerProvider(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IBusinessRuleHandler<IContract> GetBusinessRuleHandler()
        {
            return _provider.GetService<IBusinessRuleHandler<IContract>>();
        }
    }

    public interface IBusinessRuleHandler
    {
        Task<IBusinessRuleListenerResponse> ValidateBusinessRules(CancellationToken token = default);
    }

    public class BusinessRuleHandler<T> : IBusinessRuleHandler<T> where T : IContract
    {
        private readonly List<BusinessRule<T>> _businessRules = new List<BusinessRule<T>>();
        private readonly BusinessRuleListener<T> _businessRuleListener;
        private readonly T _contract;

        public BusinessRuleHandler(T contract)
        {
            _contract = contract;

            GetBusinessRulesBasedOnContract();

            if (_businessRules.Any())
                _businessRuleListener = new BusinessRuleListener<T>(_businessRules.ToArray());
            else
                throw new Exception("The are no business rules defined.");
        }

        public async Task<IBusinessRuleListenerResponse> ValidateBusinessRules(CancellationToken token = default)
        {
            foreach (var rule in _businessRules)
                await rule.Handle(_contract, token);

            return _businessRuleListener.Speak();
        }

        public async Task Handle(Func<Task> successFunction)
        {
            if (_businessRuleListener.Success)
                await successFunction?.Invoke();
        }

        public async Task Handle(Task successTask)
        {
            if (_businessRuleListener.Success)
                await successTask;
        }

        #region PRIVATE
        private void GetBusinessRulesBasedOnContract()
        {
            var businessRules = GetBusinessRules(Assembly.GetExecutingAssembly());

            foreach (var rule in businessRules)
            {
                var businessRule = ActivatorUtilities.CreateInstance(_serviceProvider, rule) as BusinessRule<T>;

                _businessRules.Add(businessRule);
            }
        }

        private static IEnumerable<Type> GetBusinessRules(Assembly assembly)
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

    public class BusinessRuleListener<T> where T : IContract
    {
        private readonly IDictionary<BusinessRule<T>, List<string>> _failures;
        private readonly List<BusinessRule<T>> _businessRules;

        public bool Success => _failures.Count is 0;

        public BusinessRuleListener(params BusinessRule<T>[] businessRules)
        {
            _failures = new Dictionary<BusinessRule<T>, List<string>>();
            _businessRules = new List<BusinessRule<T>>();

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

            if (_failures.Any())
            {
                response.Failures = new BusinessRuleFailureResponse();

                foreach (var failure in _failures)
                {
                    var businessRule = failure.Key;

                    foreach (var failureMessage in _failures[businessRule])
                    {
                        var businessRuleFailure = new BusinessRuleFailure()
                        {
                            Error = failureMessage,
                            Rule = _failures[businessRule].GetType().Name
                        };

                        response.Failures.Errors.Add(businessRuleFailure);
                    }
                }
            }

            return response;
        }

        #region PRIVATE
        private void SubscribeToFailureEvent(BusinessRule<T> rule)
        {
            rule.Failure += OnFailureEventHandler;
        }

        private void OnFailureEventHandler(BusinessRule<T> source, BusinessRuleFailureEventArgs arguments)
        {
            _failures.Add(source, arguments.Messages);
        }
        #endregion
    }

    public class LicensePlateExists : BusinessRule<ICreateLicensePlateContract>
    {
        private readonly ILicensePlateRepository _licensePlateRepository;

        public LicensePlateExists(ILicensePlateRepository licensePlateRepository)
        {
            _licensePlateRepository = licensePlateRepository;
        }

        public async override Task Handle(ICreateLicensePlateContract contract, CancellationToken token = default)
        {
            var licensePlate = await _licensePlateRepository.FindByIdentifierAsync(contract.Identifier, token);

            if (licensePlate is null)
            {
                OnFailure(new BusinessRuleFailureEventArgs
                {
                    Messages = { $"The license plate with given identifier {contract.Identifier} could not be found." }
                });
            }
        }
    }

    public class LicensePlateValidation : BusinessRule<ICreateLicensePlateContract>
    {
        private readonly LicensePlateValidator _validator;

        public LicensePlateValidation(LicensePlateValidator validator)
        {
            _validator = validator;
        }

        public override async Task Handle(ICreateLicensePlateContract contract, CancellationToken token = default)
        {
            var licensePlate = new LicensePlate { Identifier = contract.Identifier };

            var validation = await _validator.ValidateAsync(licensePlate, token);

            if (validation.IsValid is false)
            {
                var arguments = new BusinessRuleFailureEventArgs();

                foreach (var error in validation.Errors)
                    arguments.Messages.Add(error.ErrorMessage);

                OnFailure(arguments);
            }
        }
    }

    public abstract class BusinessRule<T> : IBusinessRule<T> where T : IContract
    {
        public delegate void FailureEventHandler(BusinessRule<T> source, BusinessRuleFailureEventArgs arguments);
        public event FailureEventHandler Failure;

        public abstract Task Handle(T contract, CancellationToken token = default);

        protected virtual void OnFailure(BusinessRuleFailureEventArgs arguments = default)
        {
            Failure?.Invoke(this, arguments);
        }
    }

    public interface IBusinessRule<T> where T : IContract
    {

    }

    public interface IContract
    {

    }

    public class BusinessRuleFailureEventArgs : EventArgs
    {
        public List<string> Messages { get; set; } = new List<string>();
    }
}
