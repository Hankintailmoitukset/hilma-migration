// Responsible developer:
// Responsible team:

using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hilma.Tests
{
    public static class TestHelpers
    {
        public static string GetEmbeddedResourceAsString(string endsWith, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            var name = assembly.GetManifestResourceNames().First(s => s.EndsWith(endsWith));
            string result;

            using (Stream stream = assembly.GetManifestResourceStream(name))
            using (StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}