using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reviso.TimeTracker.UI.Tests.Utilities
{
    public static class ModelValidator
    {
        public static IList<ValidationResult> Validate(object model)
        {
            // This is a helper method to validate ViewModel data annotations
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, results, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return results;
        }
    }
}
