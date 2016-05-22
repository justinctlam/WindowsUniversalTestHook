namespace TestHook.TestHook
{
    public class TestHookInt : TestHookModelBase<int>
    {
        public int Min { get; set; }

        public int Max { get; set; }

         public TestHookInt(string id, string displayName, int defaultValue, int min, int max, bool showIsEnabled = false)
            : base(id, displayName, defaultValue, "TestHookIntTemplate", showIsEnabled)
        {
            Min = min;
            Max = max;
        }
    }
}