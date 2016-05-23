using System.Collections.ObjectModel;
using System.Linq;

namespace TestHook.TestHook
{
    /// <summary>
    /// View model class for test hook menu control
    /// </summary>
    public class TestHookMenuViewModel
    {
        /// <summary>
        /// Gets an observation collection of test hook groups
        /// </summary>
        public ObservableCollection<TestHookManager.Groups> GroupList
            => new ObservableCollection<TestHookManager.Groups>(TestHookManager.Instance.TestHookGroups.Keys.OrderBy(s => s));

        /// <summary>
        /// Restores all the test hooks within a group to their defaut value.
        /// If a none group is specified, then restore all test hooks in every group.
        /// </summary>
        /// <param name="group">Group to restore test hooks</param>
        public void RestoreToDefaults(TestHookManager.Groups group)
        {
            if (group == TestHookManager.Groups.None)
            {
                var groupList = TestHookManager.Instance.TestHookGroups.Values.ToList();
                groupList.ForEach(g =>
                {
                    var list = g.TestHooks.Values.ToList();
                    list.ForEach(p => p.RestoreDefaultState());
                });
            }
            else
            {
                var list = TestHookManager.Instance.TestHookGroups[group].TestHooks.Values.ToList();
                list.ForEach(p => p.RestoreDefaultState());
            }
        }

        /// <summary>
        /// Gets an observable collection of test hooks for the group.
        /// They will be ordered by the display order index.
        /// </summary>
        /// <param name="group">Group to get test hooks</param>
        /// <returns>An observable collection of test hooks</returns>
        public ObservableCollection<ITestHookModel> GetListForGroup(TestHookManager.Groups group)
        {
            return
                new ObservableCollection<ITestHookModel>(
                    TestHookManager.Instance.TestHookGroups[group].TestHooks.Values.ToList().OrderBy(o => o.DisplayOrderIndex));
        }
    }
}