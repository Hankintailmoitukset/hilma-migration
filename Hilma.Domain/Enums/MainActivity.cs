using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract][Flags]
    public enum MainActivity
    {
        Undefined = 0,
        /// <summary>
        /// General public services
        /// </summary>
        MainactivGeneral = 1 << 0,
        /// <summary>
        /// Defence
        /// </summary>
        MainactivDefence = 1 << 1,
        /// <summary>
        /// Public order and safety
        /// </summary>
        MainactivSafety = 1 << 2,
        /// <summary>
        /// Environment
        /// </summary>
        MainactivEnvironment = 1 << 3,
        /// <summary>
        /// Economic and financial affairs
        /// </summary>
        MainactivEconomic = 1 << 4,
        /// <summary>
        /// Health
        /// </summary>
        MainactivHealth = 1 << 5,
        /// <summary>
        /// Housing and community amenities
        /// </summary>
        MainactivHousing = 1 << 6,
        /// <summary>
        /// Social protection
        /// </summary>
        MainactivSocial = 1 << 7,
        /// <summary>
        /// Recreation, culture and religion
        /// </summary>
        MainactivCulture = 1 << 8,
        /// <summary>
        /// Education
        /// </summary>
        MainactivEducation = 1 << 9,

        /// <summary>
        /// Other activity
        /// </summary>
        OtherActivity = 1 << 10
    }
}
