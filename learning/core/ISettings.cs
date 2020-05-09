namespace core
{
    /// <summary>
    /// Access to a group of settings shared across the application
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// File path to a model that is created through the first call to categorization
        /// </summary>
        string ModelPath { get; }
    }
}