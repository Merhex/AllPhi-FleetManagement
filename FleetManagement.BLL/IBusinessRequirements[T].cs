using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public interface IBusinessRequirements<T> where T : IContract
    {
        public List<IBusinessRule> BusinessRules { get; }
        void AddBusinessRules(T contract);
    }
}
