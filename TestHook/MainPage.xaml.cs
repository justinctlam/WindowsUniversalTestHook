using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TestHook.TestHook;

namespace TestHook
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            var group = TestHookManager.Instance.TestHookGroups[TestHookManager.Groups.TestGroup];
            var eventHook = group?.TestHooks[TestHookManager.EventTestId] as TestHookEvent;
            if (eventHook != null)
            {
                eventHook.OnInvoke += EventHook_OnInvoke;
            }
        }

        private void EventHook_OnInvoke(object context)
        {
            OnTestExecute(null, null);
        }

        private void OnOpenTestHookPageClick(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = true;
            MyTestHookControl.Initialize();
        }

        private void OnTestExecute(object sender, RoutedEventArgs e)
        {
            var group = TestHookManager.Instance.TestHookGroups[TestHookManager.Groups.TestGroup];
            var intHook = group?.TestHooks[TestHookManager.IntTestId] as TestHookInt;
            if (intHook != null && intHook.IsEnabled)
            {
                TestIntText.Text = $"Test Int: {intHook.Value}";
            }

            var boolHook = group?.TestHooks[TestHookManager.BoolTestId] as TestHookBool;
            if (boolHook != null && boolHook.IsEnabled)
            {
                TestBoolText.Text = $"Test Bool: {boolHook.Value}";
            }

            var enumHook = group?.TestHooks[TestHookManager.EnumTestId] as TestHookEnum<TestHookManager.TestEnum>;
            if (enumHook != null && enumHook.IsEnabled)
            {
                TestEnumText.Text = $"Test Enum: {enumHook.GetSelectedItem()}";
            }
        }
    }
}
