using System;
using System.Collections.Generic;

namespace Hilma.Domain.Entities
{
    /// <summary>
    ///     Ets API user entity. Automatically created when a new subscription
    ///     makes a request to the api. Also automatically updated when name
    ///     seems to have changed.
    /// </summary>
    public class EtsUser
    {
        /// <summary>
        ///     Hilma assigned primary key for the user.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        ///     Subscription identifier passed in the headers from APIM. Surrogate key.
        /// </summary>
        public string SubscriptionId { get; set; }
        /// <summary>
        ///     Subscription name passed in headers from APIM. Kinda whatever thing, but
        ///     since other keys are GUIDs, makes it easier to look for and identify the users.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Navigational property for all notices created by the API subscription.
        /// </summary>
        public List<Notice> Notices { get; set; }
        /// <summary>
        ///     Navigational property for all projects created by the api subscription.
        /// </summary>
        public List<ProcurementProject> Projects { get; set; }
    }
}
