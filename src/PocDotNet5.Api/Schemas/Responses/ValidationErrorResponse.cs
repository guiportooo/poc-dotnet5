namespace PocDotNet5.Api.Schemas.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public record ValidationErrorsResponse
    {
        public IEnumerable<ValidationErrorResponse> Errors { get; }
        
        public ValidationErrorsResponse(ModelStateDictionary modelState)
            => Errors = modelState.Keys
                .SelectMany(key => modelState[key]
                    .Errors
                    .Select(x => new ValidationErrorResponse(key, x.ErrorMessage)))
                .ToList(); 
    }

    public record ValidationErrorResponse(string Field, string Message);
}