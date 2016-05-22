using System.Collections.ObjectModel;
using System.Linq;

namespace TestHook.TestHook
{
    public class TestHookMenuViewModel
    {
        public ObservableCollection<TestHookManager.Groups> GroupList
            => new ObservableCollection<TestHookManager.Groups>(TestHookManager.Instance.TestHookGroups.Keys.OrderBy(s => s));

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

        public ObservableCollection<ITestHookModel> GetListForGroup(TestHookManager.Groups group)
        {
            return
                new ObservableCollection<ITestHookModel>(
                    TestHookManager.Instance.TestHookGroups[group].TestHooks.Values.ToList().OrderBy(o => o.DisplayOrderIndex));
        }
    }
}