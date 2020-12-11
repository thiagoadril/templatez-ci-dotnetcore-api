using Newtonsoft.Json;

namespace Templatez.Infra.CrossCutting.Extensions
{
    public static class JsonObjectValidationExtension
    {
        public static bool NoFields(this object obj)
        {
            if (obj == null)
                return true;

            string converted = JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return converted == "{}" || converted == "[]" ? true : false;
        }
    }
}