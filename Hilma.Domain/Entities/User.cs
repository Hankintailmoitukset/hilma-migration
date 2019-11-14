using System;
using System.Collections.Generic;

namespace Hilma.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Language { get; set; }

        public string Name { get; set; }
        public string ContactEmail { get; set; }

        public List<int> FavouritedNotices { get; set; }
        public List<Watcher> SavedWatchers { get; set; }
        public int WatcherCount { get; set; }

        public IList<ProjectCollaborators> CollaboratingProjects { get; set; }

        #region Navigation
        public List<ProcurementProject> MyProjects { get; set; }
        public List<OrganisationUser> OrganisationUsers { get; set; }
        #endregion
    }
}
