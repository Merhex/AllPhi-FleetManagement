using FluentValidation.Results;
using System.Collections.Generic;

namespace FleetManagement.BLL.Shared
{
    public record ComponentResponse : IComponentResponse
    {
        public string Type { get; init; }
        public int Status { get; init; }
        public string Title { get; init; }
        public IEnumerable<object> Errors { get; init; }

        /// <summary>
        /// "The 200 (OK) status code indicates that the request has succeeded."
        /// </summary>
        /// <returns></returns>
        public static ComponentResponse Ok() => new ComponentResponse
        { 
            Status = 200,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.3.1",
            Title = "The 200 (OK) status code indicates that the request has succeeded."
        };

        /// <summary>
        /// "The 201 (Created) status code indicates that the request has been fulfilled and has resulted in one or more new resources being created."
        /// </summary>
        /// <returns></returns>
        public static ComponentResponse Created() => new ComponentResponse 
        { 
            Status = 201,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.3.2",
            Title = "The 201 (Created) status code indicates that the request has been fulfilled and has resulted in one or more new resources being created."
        };


        /// <summary>
        /// The 400 (Bad Request) status code indicates that the server cannot or will not process the request due to something that is perceived to be
        /// a client error(e.g., malformed request syntax, invalid request message framing, or deceptive request routing).
        /// </summary>
        /// <param name="validationResult">The validation result object used to extract validation errors from.</param>
        /// <param name="message">A custom message, set as title of the response object.</param>
        /// <returns></returns>
        public static ComponentResponse BadRequest(ValidationResult validationResult, string message = "One or more validation errors have occured.") => new ComponentResponse 
        { 
            Status = 400,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = message,
            Errors = ConvertValidationResultToErrorMessages(validationResult)
        };

        /// <summary>
        /// The 400 (Bad Request) status code indicates that the server cannot or will not process the request due to something that is perceived to be
        /// a client error(e.g., malformed request syntax, invalid request message framing, or deceptive request routing).
        /// </summary>
        /// <param name="message">A custom message, set as title of the response object.</param>
        /// <returns></returns>
        public static ComponentResponse BadRequest(string message) => new ComponentResponse
        {
            Status = 400,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = message,
        };

        /// <summary>
        ///  The 204 (No Content) status code indicates that the server has successfully fulfilled the request and that there is no additional
        ///  content to send in the response payload body.Metadata in the response header fields refer to the target resource and its selected
        ///  representation after the requested action was applied.
        /// </summary>
        /// <returns></returns>
        public static ComponentResponse NoContent() => new ComponentResponse
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
