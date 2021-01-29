using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class ComponentResponse : IComponentResponse
    {
        public static ComponentResponse Success => new ComponentResponse { };
        public IDictionary<string, ICollection<string>> Failures { get; set; } = new Dictionary<string, ICollection<string>>();

        public ComponentResponse WithResponse(IBusinessRuleListenerResponse businessRuleListenerResponse)
        {
            var failureDictionary = new Dictionary<string, ICollection<string>>();

            foreach (var failure in businessRuleListenerResponse.Failures)
            {
                var key = failure.Rule;
                var value = failure.ErrorMessage;

                if (failureDictionary.ContainsKey(key))
                    failureDictionary[key].Add(value);
                else
                    failureDictionary.Add(key, new List<string> { value });
            }

            Failures = failureDictionary;

            return this;
        }
    }
}
