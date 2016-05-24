namespace TestHook.TestHook
{
    /// <summary>
    /// Test hook for integer values
    /// </summary>
    public class TestHookInt : TestHookModelBase<int>
    {
        /// <summary>
        /// Minimum value allowed
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Maximum value allowed
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Reference ID</param>
        /// <param name="displayName">UI display name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="min">Minimum value allowed</param>
        /// <param name="max">Maximum value allowed</param>
        /// <param name="showIsEnabled">Should show enabled/disabled UI</param>
        public TestHookInt(string id, string displayName, int defaultValue, int min, int max, bool showIsEnabled = false)
           : base(id, displayName, defaultValue, "TestHookIntTemplate", showIsEnabled)
        {
            Min = min;
            Max = max;
        }
    }
}