namespace Hilma.Domain.DataContracts {
    public class CpvDocumentNode
    {
        public string Id { get; set; }
        public string LabelFi { get; set; }
        public string LabelSv { get; set; }
        public string LabelEn { get; set; }
        public bool IsDisabled { get; set; }
        public bool Works { get; set; }
        public bool Supplies { get; set; }
        public bool Services { get; set; }
        public bool Social { get; set; }
        public bool NatSocial { get; set; }
        public bool NatOther { get; set; }
    }
}
