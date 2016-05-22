namespace TestHook.TestHook
{
    /// <summary>
    /// Interface for a test hook.
    /// </summary>
    public interface ITestHookModel
    {
        /// <summary>
        /// The display name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Unique reference ID.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Template name.
        /// </summary>
        string TemplateName { get; }

        /// <summary>
        /// Display order in the UI.
        /// </summary>
        int DisplayOrderIndex { get; set; }

        /// <summary>
        /// Set enabled/disabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Should this have an enabled/disabled UI.
        /// </summary>
        bool ShowIsEnabled { get; set; }

        /// <summary>
        /// Restore test hook to default value.
        /// </summary>
        void RestoreDefaultState();
    }
}