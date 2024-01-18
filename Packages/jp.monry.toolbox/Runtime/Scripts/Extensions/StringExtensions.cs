using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Monry.Toolbox.Extensions
{
    [PublicAPI]
    public static class StringExtensions
    {
        private static char[] Separators { get; } = { ' ', '-', '_' };

        public static string ToLowerFirst(this string self)
        {
            return char.ToLowerInvariant(self[0]) + self[1..];
        }

        public static string ToUpperFirst(this string self)
        {
            return char.ToUpperInvariant(self[0]) + self[1..];
        }

        public static string ToCamelCase(this string self)
        {
            return Regex
                .Replace(
                    self,
                    "[ _-]([a-zA-Z])",
                    m => $"{m.Groups[1].Value.ToUpperInvariant()}"
                )
                .ToLowerFirst();
        }

        public static string ToPascalCase(this string self)
        {
            return Regex
                .Replace(
                    self,
                    "[ _-]([a-zA-Z])",
                    m => $"{m.Groups[1].Value.ToUpperInvariant()}"
                )
                .ToUpperFirst();
        }

        public static string ToSnakeCase(this string self)
        {
            return Regex.Replace(self, "[A-Z]", "_$0")
                .ToLowerInvariant()
                .Replace('-', '_')
                .Replace(' ', '_')
                .Replace("__", "_")
                .TrimStart('_');
        }

        public static string ToKebabCase(this string self)
        {
            return Regex.Replace(self, "[A-Z]", "-$0")
                .ToLowerInvariant()
                .Replace('_', '-')
                .Replace(' ', '-')
                .Replace("--", "-")
                .TrimStart('-');
        }
    }
}
