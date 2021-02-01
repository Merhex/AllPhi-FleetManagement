﻿namespace FleetManagement.BLL
{
    public interface IBusinessRuleListener
    {
        public bool Success { get; }
        public IBusinessRuleListenerResponse Speak();
        void Listen(params IBusinessRule<IContract>[] businessRules);
    }
}
