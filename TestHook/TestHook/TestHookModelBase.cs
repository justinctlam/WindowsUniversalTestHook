using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Windows.Storage;

namespace TestHook.TestHook
{
    /// <summary>
    /// TestHookModelBase
    /// </summary>
    /// <typeparam name="T">Type of value</typeparam>
    public class TestHookModelBase<T> : INotifyPropertyChanged, ITestHookModel
    {
        private bool _isEnabled;
        private string IsEnabledKeyString => Id + "_IsEnabled";

        protected readonly T DefaultValue;
        protected T CurrentValue;

        /// <summary>
        /// Property changed handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// UI display name
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Reference Id
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        /// UI display order index
        /// </summary>
        public int DisplayOrderIndex { get; set; }

        /// <summary>
        /// Is the test hook enabled
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                var returnedObject = ApplicationData.Current.LocalSettings.Values[IsEnabledKeyString];
                if (returnedObject != null)
                {
                    _isEnabled = (bool)returnedObject;
                }

                return _isEnabled;
            }

            set
            {
                if (_isEnabled != value)
                {
                    ApplicationData.Current.LocalSettings.Values[IsEnabledKeyString] = value;
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }

        /// <summary>
        /// Show the IsEnabled UI
        /// </summary>
        public bool ShowIsEnabled { get; set; }

        /// <summary>
        /// Template name
        /// </summary>
        public string TemplateName { get; }

        /// <summary>
        /// Gets and sets the value. If in debug, it can be overriden.
        /// </summary>
        public T Value
        {
            get
            {
                GetDebugValue();
                return CurrentValue;
            }

            set
            {
                SetDebugValue(value);
                OnPropertyChanged(nameof(Value));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Reference Id</param>
        /// <param name="displayName">UI display name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="templateName">Template name</param>
        /// <param name="showIsEnabled">Should show enabled/disabled UI</param>
        public TestHookModelBase(string id, string displayName, T defaultValue, string templateName, bool showIsEnabled)
        {
            Id = id;
            DefaultValue = defaultValue;
            CurrentValue = defaultValue;

            DisplayName = displayName;
            TemplateName = templateName;

            _isEnabled = false;
            ShowIsEnabled = showIsEnabled;
        }

        /// <summary>
        /// Restore test hook to default value
        /// </summary>
        virtual public void RestoreDefaultState()
        {
            ApplicationData.Current.LocalSettings.Values.Remove(Id);
            CurrentValue = DefaultValue;
            OnPropertyChanged(nameof(Value));

            ApplicationData.Current.LocalSettings.Values.Remove(IsEnabledKeyString);
            _isEnabled = false;
            OnPropertyChanged(nameof(IsEnabled));
        }

        /// <summary>
        /// Get debug value from local settings
        /// </summary>
        [Conditional("DEBUG")]
        private void GetDebugValue()
        {
            object outputObject;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Id, out outputObject))
            {
                CurrentValue = (T)outputObject;
            }
            else
            {
                CurrentValue = DefaultValue;
            }
        }

        /// <summary>
        /// Set debug value to local settings
        /// </summary>
        /// <param name="value"></param>
        [Conditional("DEBUG")]
        private void SetDebugValue(T value)
        {
            if (!EqualityComparer<T>.Default.Equals(CurrentValue, value))
            {
                ApplicationData.Current.LocalSettings.Values[Id] = value;
            }
        }

        /// <summary>
        /// On property changed event
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}