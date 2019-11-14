
namespace Hilma.Domain.Integrations.Configuration
{
    /// <summary>
    /// Configuration object of DefenceAdministrativeInformation for Ted integration
    /// </summary>
    public class DefenceAdministrativeInformationConfiguration
    {
        
        
        public bool PreviousPublicationExists {get; set;} = false;
        public bool PreviousContractType {get; set;} = false;
        public OjsNumberConfiguration PreviousNoticeOjsNumber {get; set;} = new OjsNumberConfiguration();
        public bool HasPreviousContractNoticeOjsNumber {get; set;} = false;
        public OjsNumberConfiguration PreviousContractNoticeOjsNumber {get; set;} = new OjsNumberConfiguration();
        public bool HasPreviousExAnteOjsNumber {get; set;} = false;
        public OjsNumberConfiguration PreviousExAnteOjsNumber {get; set;} = new OjsNumberConfiguration();
        public bool TimeLimitForReceipt {get; set;} = false;
        public bool PayableDocuments {get; set;} = false;
        public ValueContractConfiguration DocumentPrice {get; set;} = new ValueContractConfiguration();
        public bool PaymentTermsAndMethods {get; set;} = false;
        public bool LanguageType {get; set;} = false;
        public bool Languages {get; set;} = false;
        public bool OtherLanguage {get; set;} = false;
        public bool OtherLanguages {get; set;} = false;
    }
}
