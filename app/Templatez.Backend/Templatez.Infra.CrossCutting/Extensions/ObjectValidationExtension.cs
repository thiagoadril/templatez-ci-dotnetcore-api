using System;

namespace Templatez.Infra.CrossCutting.Extensions
{
    public static class ObjectValidationExtension
    {
        public static bool IsNullable(this object obj) => obj == null;

        public static void ThrowIfNullable(this object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
        }
    }
}
