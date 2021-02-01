using FluentValidation.Results;

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
    }
}
