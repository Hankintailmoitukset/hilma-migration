using System;
using System.Collections.Generic;
using AutoMapper.Attributes;
using Hilma.Domain.Attributes;
using Hilma.Domain.Entities;

namespace Hilma.Domain.DataContracts
{
    /// <summary>
    ///     Describes an user.
    /// </summary>
    [MapsFrom(typeof(User))]
    [Contract]
    public class UserContract 
    {
        /// <summary>
        ///     Hilma assigned primary key..
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     User selected preferred language.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        ///     User inputted name from the AADB2C policy.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     User inputted email (login) from AADB2C policy
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        ///     Notices favourited by an user.
        /// </summary>
        public List<int> FavouritedNotices { get; set; }

        /// <summary>
        ///     Watchers saved by the user.
        /// </summary>
        public List<Watcher> SavedWatchers { get; set; }
    }
}
