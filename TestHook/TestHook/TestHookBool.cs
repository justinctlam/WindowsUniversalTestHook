namespace TestHook.TestHook
{
    /// <summary>
    /// Test hook for boolean values
    /// </summary>
    public class TestHookBool : TestHookModelBase<bool>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Reference ID</param>
        /// <param name="displayName">UI display name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="showIsEnabled">Should show enabled/disabled UI</param>
        public TestHookBool(string id, string displayName, bool defaultValue, bool showIsEnabled = false)
            : base(id, displayName, defaultValue, "TestHookBoolTemplate", showIsEnabled)
        {
        }
    }
}