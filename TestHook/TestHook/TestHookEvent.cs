using System.Windows.Input;

namespace TestHook.TestHook
{

    public class TestHookEvent : ITestHookModel
    {
        public string DisplayName { get; }

        public string Id { get; }

        public int DisplayOrderIndex { get; set; }

        public bool IsEnabled { get; set; }

        public bool ShowIsEnabled { get; set; }

        public string TemplateName => "TestHookEventTemplate";

        public object Context { get; set; }

        public delegate void CustomHandler(object context);
        public event CustomHandler OnInvoke;
        private ICommand _customCommand;

        public TestHookEvent(string id, string name, bool showIsEnabled = false)
        {
            Id = id;
            DisplayName = name;
            IsEnabled = false;
            ShowIsEnabled = showIsEnabled;
        }

        public void RestoreDefaultState()
        {
        }

        public ICommand ExecuteCommand
        {
            get
            {
                if (OnInvoke != null)
                {
                    return _customCommand ?? (_customCommand = new RelayCommand(param => OnInvoke(Context)));
                }

                return null;
            }
        }
    }
}