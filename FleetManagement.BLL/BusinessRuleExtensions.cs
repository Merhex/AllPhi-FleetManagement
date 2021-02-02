using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace FleetManagement.BLL
{
    public static class BusinessRuleExtensions
    {
        public static IBusinessRuleResponse ConvertValidationResult(this IBusinessRuleResponse response, ValidationResult result)
        {
            response = new BusinessRuleResponse { Name = response.GetType().Name };
            foreach (var error in result.Errors)
                response.Messages.Add(error.ErrorMessage);

            return response;
        }

        public static IBusinessRuleResponse Failure(this IBusinessRuleResponse response, IBusinessRule source, IEnumerable<string> messages)
        {
            response = new BusinessRuleResponse { Name = DisplayName(source), Messages = messages.ToList() };

            return response;
        }
        public static IBusinessRuleResponse Failure(this IBusinessRuleResponse response, IBusinessRule source, string message)
        {
            response = new BusinessRuleResponse { Name = DisplayName(source), Messages = { message } };

            return response;
        }

        #region PRIVATE
        private static string DisplayName(object source) => $"Check: {source.GetType().Name}";
        #endregion
    }
}
