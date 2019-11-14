using Hilma.Domain.Attributes;

namespace Hilma.Domain.Entities
{
    [EnumContract]
    public enum TendersMustBeValidOption
    {
        TimeNotSet = 0,
        Date = 1,
        Months = 2
    }
}
