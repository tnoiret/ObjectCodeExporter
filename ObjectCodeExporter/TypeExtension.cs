using System;
using System.Text;

namespace ObjectCodeExporter
{
    /// <summary>
    /// Thanks to https://stackoverflow.com/questions/17480990/get-name-of-generic-class-without-tilde
    /// </summary>
    public static class TypeExtension
    {
        public static string GetRealTypeName(this Type t)
        {
            if (!t.IsGenericType)
                return t.Name;

            StringBuilder sb = new StringBuilder();
            sb.Append(t.Name.Substring(0, t.Name.IndexOf('`')));
            sb.Append('<');
            bool appendComma = false;
            foreach (Type arg in t.GetGenericArguments())
            {
                if (appendComma) sb.Append(',');
                sb.Append(GetRealTypeName(arg));
                appendComma = true;
            }
            sb.Append('>');
            return sb.ToString();
        }
    }
}
