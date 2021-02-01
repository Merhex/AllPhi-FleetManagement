using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.BLL
{
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
                var businessRuleName = failure.Key;
                var errors = failure.Value;

                foreach (var error in errors)
                {
                    var businessRuleFailure = new BusinessRuleFailure()
                    {
                        ErrorMessage = error,
                        Rule = businessRuleName.GetType().Name
                    };

                    response.Failures.Add(businessRuleFailure);
                }
            }

            return response;
        }

        #region PRIVATE
        private void SubscribeToFailureEvent(IBusinessRule<T> rule)
        {
            rule.Failure += OnFailureEventHandler;
        }

        protected virtual void OnFailureEventHandler(IBusinessRule<T> source, BusinessRuleFailureEventArgs arguments)
        {
            _failures.Add(source, arguments.Messages);
        }
        #endregion
    }
}
