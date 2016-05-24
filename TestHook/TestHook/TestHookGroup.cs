using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestHook.TestHook
{
    /// <summary>
    /// TestHookGroup
    /// </summary>
    public class TestHookGroup
    {
        private readonly ConcurrentDictionary<string, ITestHookModel> _testHooks = new ConcurrentDictionary<string, ITestHookModel>();
        private int _displayOrderIndex = 0;

        /// <summary>
        /// Unique reference ID
        /// </summary>

        public TestHookManager.Groups Id { get; }

        /// <summary>
        /// Display UI name
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Dictionary of test hooks
        /// </summary>
        public IReadOnlyDictionary<string, ITestHookModel> TestHooks => _testHooks;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Unique reference ID</param>
        /// <param name="displayName">Display UI name</param>
        public TestHookGroup(TestHookManager.Groups id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        /// <summary>
        /// Add a test hook to the group
        /// </summary>
        /// <param name="testHook">Test hook to add</param>
        [Conditional("DEBUG")]
        public void AddHook(ITestHookModel testHook)
        {
            testHook.DisplayOrderIndex = _displayOrderIndex;
            _displayOrderIndex++;

            _testHooks.TryAdd(testHook.Id, testHook);
        }
    }
}
