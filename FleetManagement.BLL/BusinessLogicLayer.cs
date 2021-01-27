using FleetManagement.BLL.MotorVehicles.Contracts;
using FleetManagement.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
    /// <summary>
    /// The BLL assembly handle, for the registrations in the service container.
    /// </summary>
    public class BusinessLogicLayer { }


    public class BusinessRuleHandler<T> where T : IContract
    {
        private readonly List<BusinessRule<T>> _businessRules = new List<BusinessRule<T>>();
        private readonly BusinessRuleListener<T> _listener;

        public BusinessRuleHandler(params BusinessRule<T>[] businessRules)
        {
            _listener = new BusinessRuleListener<T>(businessRules);
        }

        //Make a listener
        //Get all business rules based on the contract,
        //subscribe on the failure events together with the listener
        //Handle them
        //raise success event if no failures were reported. give back listener
        //If fail, give back listener
        public async Task<BusinessRuleListener<T>> Handle(T contract)
        {
            foreach (var rule in _businessRules)
                await rule.Handle(contract);

            return _listener;
        }   
    }

    public class BusinessRuleHandlerBuilder<T> where T : IContract
    {
        private readonly List<BusinessRule<T>> _businessRules = new List<BusinessRule<T>>();

        public BusinessRuleHandlerBuilder<T> Handle(BusinessRule<T> rule)
        {
            _businessRules.Add(rule);

            return this;
        }

        public BusinessRuleHandler<T> Build()
        {
            return new BusinessRuleHandler<T>(_businessRules.ToArray());
        }
    }

    public class BusinessRuleListener<T> where T : IContract
    {
        private readonly IDictionary<BusinessRule<T>, string> _failures = new Dictionary<BusinessRule<T>, string>();
        private readonly List<BusinessRule<T>> _businessRules;

        public bool Success => _failures.Count is 0;

        public BusinessRuleListener(params BusinessRule<T>[] businessRules)
        {
            if (businessRules.Any())
            {
                _businessRules.AddRange(businessRules);

                foreach (var rule in _businessRules)
                    rule.Failure += OnFailureEventHandler;
            }
        }

        private void OnFailureEventHandler(BusinessRule<T> source, BusinessRuleFailureEventArgs arguments)
        {
            _failures.Add(source, arguments.Message);
        }

        public IBusinessHandlerResponse<T> Speak()
        {
            var response = new BusinessRuleResponse<T>(success: true);

            if (_failures.Any())
                foreach (var failure in _failures)
                    response.AddFailure(failure.Key, failure.Value);

            return response;
        }
    }


    public class BusinessRuleResponse<T> : IBusinessHandlerResponse<T> where T : IContract
    {
        public bool Success { get; init; }
        public IList<IErrorResponseModel> Errors { get; private set; }

        public BusinessRuleResponse(bool success = true)
        {
            Success = success;
        }

        public void AddFailure(BusinessRule<T> rule, string failureMessage)
        {
            Errors.Add(new ErrorResponseModel 
            { 
                Type = rule.GetType().Name,
                Error = failureMessage 
            });
        }
    }

    public interface IBusinessHandlerResponse<T> where T : IContract
    {
        public bool Success { get; }
        public IList<IErrorResponseModel> Errors { get; }


        void AddFailure(BusinessRule<T> rule, string failureMessage);
    }


    public abstract class ExistsBusinessRule<T> : BusinessRule<T> where T : IContract
    {

    }

    public class LicensePlateExists : ExistsBusinessRule<ICreateLicensePlateContract>
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
                OnFailure(new BusinessRuleFailureEventArgs 
                { 
                    Message = $"The license plate with given identifier {contract.Identifier} could not be found." 
                });
        }
    }

    public abstract class BusinessRule<T> where T : IContract
    {
        //public delegate void SuccessEventHandler(BusinessRule<T> source);
        //public event SuccessEventHandler Success;

        public delegate void FailureEventHandler(BusinessRule<T> source, BusinessRuleFailureEventArgs arguments);
        public event FailureEventHandler Failure;

        public abstract Task Handle(T contract, CancellationToken token = default);

        //protected virtual void OnSucces()
        //{
        //    Success?.Invoke(this);
        //}

        protected virtual void OnFailure(BusinessRuleFailureEventArgs arguments = default)
        {
            Failure?.Invoke(this, arguments);
        }
    }

    public interface IContract
    {

    }

    public class BusinessRuleFailureEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
