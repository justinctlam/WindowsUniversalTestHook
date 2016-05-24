using System.Windows.Input;

namespace TestHook.TestHook
{
    /// <summary>
    /// Test hook for trigger events
    /// </summary>
    public class TestHookEvent : ITestHookModel
    {
        private ICommand _customCommand;

        /// <summary>
        /// The display name
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Unique reference ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Display order in the UI
        /// </summary>
        public int DisplayOrderIndex { get; set; }

        /// <summary>
        /// Set enabled/disabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Should this have an enabled/disabled UI
        /// </summary>
        public bool ShowIsEnabled { get; set; }

        /// <summary>
        /// Template name
        /// </summary>
        public string TemplateName => "TestHookEventTemplate";

        /// <summary>
        /// Context for the event
        /// </summary>
        public object Context { get; set; }

        /// <summary>
        /// Command handler
        /// </summary>
        /// <param name="context">Parameters</param>
        public delegate void CustomHandler(object context);

        /// <summary>
        /// Command event
        /// </summary>
        public event CustomHandler OnInvoke;

        /// <summary>
        /// Command to execute
        /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Reference ID</param>
        /// <param name="displayName">UI display name</param>
        /// <param name="showIsEnabled">Should show enabled/disabled UI.</param>
        public TestHookEvent(string id, string displayName, bool showIsEnabled = false)
        {
            Id = id;
            DisplayName = displayName;
            IsEnabled = false;
            ShowIsEnabled = showIsEnabled;
        }

        /// <summary>
        /// Restore to default
        /// </summary>
        public void RestoreDefaultState()
        {
        }
    }
}