using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hilma.Domain.Entities {
    /// <summary>
    /// Contains information about 
    /// </summary>
    public class WatcherUserBatch
    {
        /// <summary>
        /// Runnable watchers by this user / to be ran in the context of this report.
        /// </summary>
        public List<WatcherRunnable> Runnables { get; set; }

        /// <summary>
        /// Delivery address for the message generated, in case watcher has found a match.
        /// </summary>
        public string DeliveryEmail { get; set; }

        /// <summary>
        /// Language of the user.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Fetch notices newer than.
        /// </summary>
        public DateTime NoticesSince { get; set; }

        /// <summary>
        /// Factory for creating a watchers batch.
        /// </summary>
        /// <param name="users"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<WatcherUserBatch>> CreateWatchers(DbSet<User> users,
            CancellationToken token)
        {
            return await users
                .AsNoTracking()
                .Where(x => x.WatcherCount > 0)
                .Select(x => new WatcherUserBatch {
                    DeliveryEmail = x.ContactEmail,
                    Language = x.Language,
                    Runnables = x.SavedWatchers.Select(y => new WatcherRunnable
                    {
                        Name = y.Name,
                        SearchParameters = y.SearchParameters
                    }).ToList()
                })
                .ToListAsync(token);
        }
    }
}
