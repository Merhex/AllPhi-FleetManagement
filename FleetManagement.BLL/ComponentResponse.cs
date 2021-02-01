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

        public ComponentResponse WithResponse(IBusinessHandlerResponse businessHandlerResponse)
        {
            var responseDictionary = new Dictionary<string, ICollection<string>>();

            foreach (var response in businessHandlerResponse.Responses)
            {
                var key = response.Name;

                foreach (var message in response.Messages)
                {
                    if (responseDictionary.ContainsKey(key))
                        responseDictionary[key].Add(message);
                    else
                        responseDictionary.Add(key, new List<string> { message });
                }
            }

            Failures = responseDictionary;

            return this;
        }
    }
}
