using Hilma.Domain.Attributes;

namespace Hilma.Domain.DataContracts
{
    [EnumContract]
    public enum QualificationSystemDurationType
    {
        Undefined = 0,
        BeginAndEndDate = 1,
        Indefinite = 2
    }
}
