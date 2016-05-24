using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TestHook.TestHook
{
    /// <summary>
    /// Test Hook manager used to build and register your test hooks for the app
    /// </summary>
    public class TestHookManager
    {
        /// <summary>
        /// Example constants for control IDs
        /// </summary>
        public static string EventTestId = "EventTestId";
        public static string BoolTestId = "BoolTestId";
        public static string IntTestId = "IntTestId";
        public static string EnumTestId = "EnumTestId";

        /// <summary>
        /// Sample enum
        /// </summary>
        public enum TestEnum
        {
            Test1,
            Test2,
            Test3
        }

        /// <summary>
        /// Test hook groups
        /// </summary>
        public enum Groups
        {
            None,
            TestGroup
        }

        /// <summary>
        /// Returns a readonly dictionary of the test hook groups
        /// </summary>
        public IReadOnlyDictionary<Groups, TestHookGroup> TestHookGroups => _testHookGroups;

        /// <summary>
        /// Stores all the test hook groups
        /// </summary>
        private readonly ConcurrentDictionary<Groups, TestHookGroup> _testHookGroups =
            new ConcurrentDictionary<Groups, TestHookGroup>();

        /// <summary>
        /// Lazy creation of the test hook manager singleton
        /// </summary>
        private static readonly Lazy<TestHookManager> TestHookManagerInstance =
            new Lazy<TestHookManager>(() => new TestHookManager());

        /// <summary>
        /// Gets the instance of the test hook manager
        /// </summary>
        public static TestHookManager Instance => TestHookManagerInstance.Value;

        /// <summary>
        /// Constructor, private because it is a singleton
        /// </summary>
        private TestHookManager()
        {
        }

        /// <summary>
        /// Registers all the test hook into the manager, debug only
        /// </summary>
        [Conditional("DEBUG")]
        public void RegisterTestHooks()
        {
            BuildTestHooks();
        }

        /// <summary>
        /// Gets the description for the test hook group
        /// </summary>
        /// <param name="group">A test hook group</param>
        /// <returns>A description string</returns>
        public string GetDescription(Groups group)
        {
            return _testHookGroups[group].DisplayName;
        }

        /// <summary>
        /// Tries to get a test hook group for the given ID
        /// </summary>
        /// <param name="testHookGroupId">Test hook group ID</param>
        /// <returns>A test hook group</returns>
        public TestHookGroup TryGetTestHookGroup(Groups testHookGroupId)
        {
            TestHookGroup testHookGroup;
            _testHookGroups.TryGetValue(testHookGroupId, out testHookGroup);
            return testHookGroup;
        }

        /// <summary>
        /// Adds a new test hook group to the manager.
        /// Will assert if there is already an existing group.
        /// </summary>
        /// <param name="testHookGroup">The test hook group to be added</param>
        [Conditional("DEBUG")]
        internal void AddGroup(TestHookGroup testHookGroup)
        {
            Debug.Assert(_testHookGroups.TryAdd(testHookGroup.Id, testHookGroup), "Trying to add duplicate entries.");
        }

        /// <summary>
        /// Build a group of test hooks
        /// </summary>
        [Conditional("DEBUG")]
        private void BuildTestHooks()
        {
            var group = new TestHookGroup(Groups.TestGroup, "Test Group");
            AddGroup(group);

            group.AddHook(new TestHookEvent(EventTestId, "Event test"));
            group.AddHook(new TestHookEnum<TestEnum>(EnumTestId, "Enum test", 0, true));
            group.AddHook(new TestHookBool(BoolTestId, "Bool test", true, true));
            group.AddHook(new TestHookInt(IntTestId, "Int test", 0, 0, 100, true));
        }
    }
}
