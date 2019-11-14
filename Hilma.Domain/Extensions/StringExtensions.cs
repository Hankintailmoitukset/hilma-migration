using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
