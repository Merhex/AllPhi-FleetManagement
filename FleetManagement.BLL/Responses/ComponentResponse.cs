using FluentValidation.Results;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class ComponentResponse : IComponentResponse
    {
        public string Reference { get; private set; }
        public int Status { get; private set; }
        public string Title { get; private set; }
        public IList<IErrorResponseModel> Errors { get; private set; }
        public bool Valid { get; private set; }

        public ComponentResponse()
        {
            Errors = new List<IErrorResponseModel>();
        }

        public ComponentResponse Ok()
        {
            Status = 200;
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.3.1";
            Title = "The 200 (OK) status code indicates that the request has succeeded";
            Valid = true;

            return this;
        }

        public ComponentResponse BadRequest()
        {
            Status = 400;
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            Title = "The 400 (Bad Request) status code indicates that the server cannot or will not process the request due to something that is perceived to be a client error(e.g., malformed request syntax, invalid request message framing, or deceptive request routing).";
            Valid = false;
            
            return this;
        }

        public ComponentResponse Created()
        {
            Status = 201;
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.3.2";
            Title = "The 201 (Created) status code indicates that the request has been fulfilled and has resulted in one or more new resources being created.";
            Valid = true;

            return this;
        }

        public ComponentResponse InternalServerError()
        {
            Status = 500;
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
            Title = "The 500 (Internal Server Error) status code indicates that the server encountered an unexpected condition that prevented it from fulfilling the request.";
            Valid = false;

            return this;
        }

        public ComponentResponse NotFound()
        {
            Status = 404;
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
            Title = "The 404 (Not Found) status code indicates that the origin server did not find a current representation for the target resource or is not willing to disclose that one exists.";
            Valid = false;

            return this;
        }

        public ComponentResponse AddErrorMessage(string message)
        {
            Errors.Add(new ErrorResponseModel
            {
                Error = message,
                Type = "Error"
            });

            Valid = false;

            return this;
        }

        public ComponentResponse WithTitle(string title)
        {
            Title = title;

            return this;
        }

        public ComponentResponse AlreadyExists()
        {
            BadRequest().WithTitle("The given entity already exists.");

            return this;
        }

        public ComponentResponse ValidationFailure(ValidationResult result)
        {
            BadRequest().WithTitle("One ore more validation errors have occured.");
            
            if (result.IsValid is not true)
            {
                foreach (var error in result.Errors)
                    Errors.Add(new ErrorResponseModel
                    {
                        Error = error.ErrorMessage,
                        Field = error.PropertyName,
                        Type = "Validation"
                    });
            }

            return this;
        }

        public ComponentResponse PersistanceFailure()
        {
            InternalServerError().WithTitle("Something went wrong persisting the data.");

            return this;
        }
    }
}
