namespace core
{
    /// <summary>
    /// Access to a group of settings shared across the application
    /// </summary>
    public interface ISettings
    {
        string ModelPath { get; }
    }
}