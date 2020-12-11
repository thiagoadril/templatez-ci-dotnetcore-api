using System.Collections.Generic;

namespace Templatez.Api.Http.Errors
{
    public class ErrorRequestValidationValue
    {
        public ErrorRequestValidationValue()
        {
            Messages = new List<string>();
        }
        public string Field { get; set; }
        public List<string> Messages { get; set; }
    }
}
