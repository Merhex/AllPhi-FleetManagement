using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.BLL
{
    public static class BusinessRuleExtensions
    {
        public static IBusinessRuleResponse ConvertValidationResult(this IBusinessRuleResponse response, IBusinessRule source, ValidationResult result)
        {
            response.Name = DisplayName(source);
            foreach (var error in result.Errors)
                response.Messages.Add(error.ErrorMessage);

            return response;
        }

        public static IBusinessRuleResponse Failure(this IBusinessRuleResponse response, IBusinessRule source, IEnumerable<string> messages)
        {
            response.Name = DisplayName(source);
            response.Messages = messages.ToList();

            return response;
        }

        public static IBusinessRuleResponse Failure(this IBusinessRuleResponse response, IBusinessRule source, string message)
        {
            response.Name = DisplayName(source);
            response.Messages.Add(message);

            return response;
        }

        #region PRIVATE
        private static string DisplayName(IBusinessRule source) => $"Check: {source.GetType().Name}";
        #endregion
    }
}
