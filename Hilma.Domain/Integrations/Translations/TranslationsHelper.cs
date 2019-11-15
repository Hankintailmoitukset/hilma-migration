using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hilma.Domain.Integrations.Translations
{
    /// <summary>
    ///     Helpers for dealing with translations on back-end.
    /// </summary>
    public static class TranslationsHelper
    {
        private static readonly SemaphoreSlim Lock = new SemaphoreSlim(1, 1);
        private static JObject _translations = null;

        /// <summary>
        /// Extends JObject with a capability to fetch a translation key value.
        /// </summary>
        /// <param name="dictionary">JObject dictionary to search from</param>
        /// <param name="translation">Translation key</param>
        /// <param name="language">Language</param>
        /// <returns>Translation string</returns>
        public static string GetTranslation(this JObject dictionary, string translation, string language)
        {
            var currentDictionary = dictionary[language];
            var translationParts = translation.Split('.');

            foreach (var translationPart in translationParts)
            {
                currentDictionary = currentDictionary[translationPart];
            }

            return currentDictionary.ToString();
        }

        /// <summary>
        ///     Get translations from static cache or remote.
        /// </summary>
        /// <param name="remoteEndpoint">Endpoint to get the translations from.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Translations in a <see cref="JObject">JObject</see></returns>
        public static async Task<JObject> GetTranslations(
            string remoteEndpoint,
            CancellationToken token)
        {
            if (_translations != null)
            {
                return _translations;
            }

            await Lock.WaitAsync(token);

            if (_translations != null)
            {
                return _translations;
            }

            try
            {
                using (var client = new WebClient())
                {
                    var translationsStringBlob = await client
                        .DownloadStringTaskAsync(new Uri(remoteEndpoint));
                    _translations = JObject.Parse(translationsStringBlob);
                }
            }
            finally
            {
                Lock.Release();
            }

            return _translations;
        }
    }
}
