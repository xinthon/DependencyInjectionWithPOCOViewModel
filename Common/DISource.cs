using System;
using System.Windows.Markup;

namespace Common
{
    public class DISource : MarkupExtension
    {
        public static Func<Type, object, string, object>? Resolver { get; set; }

        public Type? Type { get; set; }

        public object Key { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            if (Type == null)
                return null;
            return Resolver?.Invoke(Type, Key, Name);
        }
    }
}
