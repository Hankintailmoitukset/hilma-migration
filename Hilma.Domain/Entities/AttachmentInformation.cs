using System.Collections.Generic;
using AutoMapper;
using Hilma.Domain.Attributes;
using Hilma.Domain.Enums;
using Hilma.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Hilma.Domain.Entities
{
    [Contract]
    public class AttachmentInformation
    {
        /// <summary>
        /// Description for attachments and links
        /// </summary>
        public string[] Description { get; set; } = new string[0];

        /// <summary>
        /// Links attached to the notice.
        /// </summary>
        public Link[] Links { get; set; } = new Link[0];

        /// <summary>
        /// Is the attachments sub-page valid?
        /// </summary>
        public ValidationState ValidationState { get; set; }

        public void Trim()
        {
            foreach (var link in Links)
            {
                link.Url = link.Url.CleanUrl();
            }
        }
    }
}
