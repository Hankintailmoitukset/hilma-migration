using Hilma.Domain.Attributes;
using System;
using System.Collections.Generic;

namespace Hilma.Domain.Entities
{
    /// <summary>
    /// Defines all possible changes according to:
    /// #  XSD name : F14_2014
    /// #  RELEASE : "R2.0.9.S04"                                                      
    /// #  Intermediate release number 007-20181030                               
    /// #  Last update : 08/06/2018 
    /// #  Form : Corrigendum
    /// 
    /// <element name="NOTHING" type="empty"/>
    /// <element ref="CPV_MAIN"/>
    /// <element ref="CPV_ADDITIONAL" maxOccurs="100"/>
    /// <element name="TEXT" type="text_ft_multi_lines"/>
    /// <sequence>
    ///	    <element name="DATE" type="date_full"/>
    ///	    <element name="TIME" type="time" minOccurs="0"/>
    /// </sequence>
    /// </summary>
    [Contract]
    public class Change
    {

        /// <summary>
        /// Section from Ted attribute. Eg. I.1
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Translated label of Ted attribute.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Lot number, if applicable.
        /// </summary>
        public string LotNumber { get; set; }

        /// <summary>
        /// Text changes - old
        /// </summary>
        public string[] OldText { get; set; }

        /// <summary>
        /// Text changes - new
        /// </summary>
        public string[] NewText { get; set; }

        /// <summary>
        /// Main cpv code changes - new
        /// </summary>
        public CpvCode NewMainCpvCode { get; set; }

        /// <summary>
        /// Nuts codes (used in defence corrigendums) - new
        /// </summary>
        public string[] NewNutsCodes { get; set; }

        /// <summary>
        /// Additional cpv code changes - new
        /// </summary>
        public List<CpvCode> NewAdditionalCpvCodes { get; set; }

        /// <summary>
        /// Main cpv code changes - old
        /// </summary>
        public CpvCode OldMainCpvCode { get; set; }

        /// <summary>
        /// Additional cpv code changes - old
        /// </summary>
        public List<CpvCode> OldAdditionalCpvCodes { get; set; }

        /// <summary>
        /// Nuts codes (used in defence corrigendums) - old
        /// </summary>
        public string[] OldNutsCodes { get; set; }

        /// <summary>
        /// Date changes - new
        /// </summary>
        public DateTime? NewDate { get; set; }

        /// <summary>
        /// Date changes - old
        /// </summary>
        public DateTime? OldDate { get; set; }
    }
}
