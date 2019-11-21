using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    /// <summary>
    /// Yleinen avoimuusilmoitus: Ilmoituksen tyyppi
    /// </summary>
    [EnumContract]
    public enum TransparencyType
    {
        Undefined = 0,

        /// <summary>
        /// Tällä ilmoituksella ilmoitetaan julkisista hankinnoista ja käyttöoikeussopimuksista annetun lain 15 §:ssä tarkoitettu sidosyksikön ulosmyyntiaie
        /// </summary>
        TransparencyLaw15 = 1,

        /// <summary>
        /// Tällä ilmoituksella ilmoitetaan julkisista hankinnoista ja käyttöoikeussopimuksista annetun lain 16 §:ssä tarkoitettu ulosmyyntiaie toiminnasta, jota hankintayksiköt harjoittavat yhteistyössä
        /// </summary>
        TransparencyLaw16 = 2,

        /// <summary>
        /// Tällä ilmoituksella ilmoitetaan muusta järjestelystä
        /// </summary>
        TransparencyOther = 3
    }
}
