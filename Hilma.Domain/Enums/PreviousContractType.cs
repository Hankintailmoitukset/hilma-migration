using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    /// <summary>
    /// Directive 2009/81/EC (Defence contracts only!)
    /// IV.3.2) Previous publication(s) concerning the same contract 
    /// </summary>
    [EnumContract]
    [Flags]
    public enum PreviousContractType : long
    {
        Undefined = 0,

        /// <summary>
        /// F01_2014
        /// Prior information notice
        /// </summary>
        PriorInformation = 1 << 0,

        /// <summary>
        /// F08_2014
        /// Notice on a buyer profile
        /// </summary>
        BuyerProfile = 1 << 1,
    }
}
