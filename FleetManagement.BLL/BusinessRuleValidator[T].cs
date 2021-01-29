using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.BLL
{
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

        public async Task<IBusinessRuleListenerResponse> Validate(T contract, CancellationToken token = default)
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

            var businessRuleType = typeof(IBusinessRule<T>);
            var contractType = typeof(IContract);

            var interfaces = typeof(T)
                                .GetInterfaces();

            foreach (var type in interfaces)
            {
                var x = loadableTypes
                    .Where(type.IsAssignableFrom)
                    .Where(type => type.IsInterface is false);

            }

            //TODO Find all parent interfaces, make list, find all classes who implement each interface in that list and instantiate as businessrule.

            return loadableTypes
                .Where(businessRuleType.IsAssignableFrom)
                .Where(type => type.IsInterface is false)
                .ToList();
        }
        #endregion
    }
}
