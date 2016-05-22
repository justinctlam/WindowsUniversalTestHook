using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestHook.TestHook
{
    public class TestHookGroup
    {
        private readonly ConcurrentDictionary<string, ITestHookModel> _testHooks = new ConcurrentDictionary<string, ITestHookModel>();

        private int _displayOrderIndex = 0;

        public TestHookManager.Groups Id { get; }

        public string DisplayName { get; }

        public IReadOnlyDictionary<string, ITestHookModel> TestHooks => _testHooks;

        public TestHookGroup(TestHookManager.Groups id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        [Conditional("DEBUG")]
        public void AddHook(ITestHookModel testHook)
        {
            testHook.DisplayOrderIndex = _displayOrderIndex;
            _displayOrderIndex++;

            _testHooks.TryAdd(testHook.Id, testHook);
        }
    }
}
