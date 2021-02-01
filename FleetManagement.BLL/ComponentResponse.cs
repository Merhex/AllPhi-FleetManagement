using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class ComponentResponse : IComponentResponse
    {
        public static ComponentResponse Success => new ComponentResponse { };
        public IDictionary<string, ICollection<string>> Failures { get; set; } = new Dictionary<string, ICollection<string>>();

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
