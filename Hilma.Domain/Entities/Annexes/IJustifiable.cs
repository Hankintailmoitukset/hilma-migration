namespace Hilma.Domain.Entities.Annexes {
    /// <summary>
    ///     Interface for justifiable stuffs.
    /// </summary>
    public interface IJustifiable
    {
        /// <summary>
        ///     Justification for direct purchase.
        /// </summary>
        string[] Justification { get; }
    }
}
