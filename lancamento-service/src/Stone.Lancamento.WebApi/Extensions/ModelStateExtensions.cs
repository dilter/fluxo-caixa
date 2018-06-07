using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Stone.Lancamento.WebApi
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<dynamic> AllErrors(this ModelStateDictionary modelState)
        {
            var result = new List<dynamic>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                    .Select(error => new { Field = fieldKey, Message = error.ErrorMessage });
                result.AddRange(fieldErrors);
            }

            return result;
        }
    }
}