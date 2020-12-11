using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Templatez.Api.Http.Errors
{
    public class ErrorRequestModelValidation
    {
        public ErrorRequestModelValidation(ActionContext context)
        {
            Errors = new List<ErrorRequestValidationValue>();

            foreach (KeyValuePair<string, ModelStateEntry> state in context.ModelState)
            {
                var key = state.Key.ToLower();
                var errors = state.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                        Errors.Add(new ErrorRequestValidationValue()
                        {
                            Field = key,
                            Messages = new List<string>()
                            {
                                GetErrorMessage(errors[0])
                            }
                        });
                    else
                    {
                        var messages = new List<string>();
                        for (var i = 0; i < errors.Count; i++)
                            messages.Add(GetErrorMessage(errors[i]));

                        if (messages.Count > 0)
                            Errors.Add(new ErrorRequestValidationValue()
                            {
                                Field = key,
                                Messages = messages
                            });
                    }
                }
            }
        }

        [Required]
        public List<ErrorRequestValidationValue> Errors { get; set; }

        private string GetErrorMessage(ModelError error) =>
            string.IsNullOrEmpty(error.ErrorMessage)
            ? "input not valid"
            : error.ErrorMessage.ToLower();
    }
}
