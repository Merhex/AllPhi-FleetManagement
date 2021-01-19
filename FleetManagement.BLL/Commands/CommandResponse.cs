using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace FleetManagement.BLL.Commands
{
    public record CommandResponse
    {
        public string Type { get; init; }
        public int Status { get; init; }
        public string Title { get; init; }
        public IEnumerable<object> Errors { get; init; }

        public static CommandResponse Ok() => new CommandResponse 
        { 
            Status = 200,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.3.1",
            Title = "The 200 (OK) status code indicates that the request has succeeded."
        };

        public static CommandResponse Created() => new CommandResponse 
        { 
            Status = 201,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.3.2",
            Title = "The 201 (Created) status code indicates that the request has been fulfilled and has resulted in one or more new resources being created."
        };

        public static CommandResponse BadRequest(ValidationResult validationResult, string message = "One or more validation errors have occured.") => new CommandResponse 
        { 
            Status = 400,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = message,
            Errors = ConvertValidationResultToErrorMessages(validationResult)
        };

        public static CommandResponse BadRequest(string message) => new CommandResponse
        {
            Status = 400,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = message,
        };

        public static CommandResponse NoContent() => new CommandResponse
        {
            Status = 204,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.3.2",
            Title = "The 204 (No Content) status code indicates that the server has successfully fulfilled the request and that there is no additional content to send in the response payload body."
        };


        private static IEnumerable<object> ConvertValidationResultToErrorMessages(ValidationResult result)
        {
            if (result.IsValid is not true)
            {
                foreach (var error in result.Errors)
                    yield return new { FieldName = error.PropertyName, Error = error.ErrorMessage };
            }
        }
    }
}
