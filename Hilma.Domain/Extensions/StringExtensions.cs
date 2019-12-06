using System.Linq;

namespace Hilma.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool HasAnyContent( this string[] array )
        {
            return array != null && array.Any(value => !string.IsNullOrEmpty(value));
        }
    }
}
