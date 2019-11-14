using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [EnumContract][Flags]
    public enum MainActivityUtilities
    {
        Undefined = 0,
        /// <summary>
        /// Production, transport and distribution of gas and heat
        /// </summary>
        MainactivGasProduct = 1 << 0,
        /// <summary>
        /// Electricity
        /// </summary>
        MainactivElectricity = 1 << 1,
        /// <summary>
        /// Extraction of gas and oil
        /// </summary>
        MainactivGasExplor = 1 << 2,
        /// <summary>
        /// Exploration and extraction of coal and other solid fuels
        /// </summary>
        MainactivCoal = 1 << 3,
        /// <summary>
        /// Water
        /// </summary>
        MainactivWater = 1 << 4,
        /// <summary>
        /// Postal services
        /// </summary>
        MainactivPostal = 1 << 5,
        /// <summary>
        /// Railway services
        /// </summary>
        MainactivRailway = 1 << 6,
        /// <summary>
        /// Urban railway, tramway, trolleybus or bus services
        /// </summary>
        MainactivBus = 1 << 7,
        /// <summary>
        /// Port-related activities
        /// </summary>
        MainactivPort = 1 << 8,
        /// <summary>
        /// Airport-related activities
        /// </summary>
        MainactivAirportrelated = 1 << 9,
        /// <summary>
        /// Other activity
        /// </summary>
        OtherActivity = 1 << 10
    }
}
