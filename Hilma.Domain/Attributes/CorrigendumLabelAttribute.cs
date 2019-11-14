using System;

namespace Hilma.Domain.Attributes
{
    /// <summary>
    /// Attributes related TED -forms
    /// </summary>
    public class CorrigendumLabelAttribute : Attribute
    {
        public string Label;
        public string Section;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="label">TED label that is also in Loco - eg. Title</param>
        /// <param name="section">Section - eg. II.1.1</param>
        public CorrigendumLabelAttribute(string label, string section)
        {
            Label = label;
            Section = section;
        }
    }

}
