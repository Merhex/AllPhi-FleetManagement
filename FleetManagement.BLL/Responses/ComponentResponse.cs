using FluentValidation.Results;
using System.Collections.Generic;

namespace FleetManagement.BLL
{
    public class ComponentResponse : IComponentResponse
    {
        public string Title { get; private set; }
        public IList<IErrorResponseModel> Errors { get; private set; }
        public bool Valid { get; private set; }

        private readonly Dictionary<int, string> _referenceDictionary;

        private int _status;
        public int Status
        {
            get { return _status; }
            private set
            {
                if (value > _status) _status = value;
                _reference = _referenceDictionary[_status];
            }
        }

        private string _reference;
        public string Reference { get { return _reference; } }



        public ComponentResponse()
        {
            Errors = new List<IErrorResponseModel>();

            _referenceDictionary = new Dictionary<int, string>
            {
                { 200, "https://tools.ietf.org/html/rfc7231#section-6.3.1" },
                { 201, "https://tools.ietf.org/html/rfc7231#section-6.3.2" },
                { 400, "https://tools.ietf.org/html/rfc7231#section-6.5.1" },
                { 404, "https://tools.ietf.org/html/rfc7231#section-6.5.4" },
                { 500, "https://tools.ietf.org/html/rfc7231#section-6.6.1" }
            };
        }

        public ComponentResponse Ok()
        {
            if (Status is not 0) return this;

            Status = 200;
            Valid = true;

            return this;
        }

        public ComponentResponse BadRequest()
        {
            Status = 400;
            Valid = false;
            
            return this;
        }

        public ComponentResponse Created()
        {
            Status = 201;
            Valid = true;

            return this;
        }

        public ComponentResponse InternalServerError()
        {
            Status = 500;
            Valid = false;

            return this;
        }

        public ComponentResponse NotFound(object entity)
        {
            Status = 404;
            Valid = false;

            AddErrorMessage($"The entity {entity}, could not be found.");

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

        public ComponentResponse AlreadyExists(object entity)
        {
            BadRequest().AddErrorMessage($"The given entity: {entity}, already exists.");

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
