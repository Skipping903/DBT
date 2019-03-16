using System;

namespace DBTR.Extensions
{
    public static class TypeExtensions
    {
        public static string GetTexturePathFromType(this Type type)
        {
            string[] segments = type.Namespace.Split('.');
            return string.Join("/", segments, 1, segments.Length - 1) + '/' + type.Name;
        }
    }
}
