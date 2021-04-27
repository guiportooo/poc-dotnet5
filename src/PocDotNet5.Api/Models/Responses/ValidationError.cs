using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PocDotNet5.Api.Models.Responses
{
    public record ValidationErrors
    {
        public IEnumerable<ValidationError> Errors { get; }
        
        public ValidationErrors(ModelStateDictionary modelState)
            => Errors = modelState.Keys
                .SelectMany(key => modelState[key]
                    .Errors
                    .Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList(); 
    }

    public record ValidationError(string Field, string Message);
}