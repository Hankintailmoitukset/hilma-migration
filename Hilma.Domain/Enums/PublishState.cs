using System;
using Hilma.Domain.Attributes;

namespace Hilma.Domain.Enums
{
    [Flags]
    [EnumContract]
    public enum PublishState
    {
        Undefined = 0 << 0,
        /// <summary>
        /// Work in progress
        /// </summary>
        Draft = 1 << 0,
        /// <summary>
        /// Notice has been published in TED and Hilma
        /// </summary>
        Published = 1 << 1,
        /// <summary>
        /// Notice is pending publication from TED
        /// </summary>
        WaitingToBePublished = 1 << 2,
        /// <summary>
        /// Notice sent to ted and either the publication has been cancelled or the notice is set not to be available to the public.
        /// </summary>
        NotPublic = 1 << 3,
        /// <summary>
        /// Notice had been published by the 48h rule in Finland, but was rejected by TED
        /// </summary>
        PublishedThenRejected = 1 <<4
    }
}
