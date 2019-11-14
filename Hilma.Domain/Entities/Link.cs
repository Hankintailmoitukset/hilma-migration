using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [Contract]
    public class Link   
    {
        /// <summary>
        /// Url for the linked document or website
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Optional description for the link or website
        /// </summary>
        public string Description { get; set; }

    }
}
