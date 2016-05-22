using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Windows.Storage;

namespace TestHook.TestHook
{
    public class TestHookModelBase<T> : INotifyPropertyChanged, ITestHookModel
    {
        private bool _isEnabled;
        public event PropertyChangedEventHandler PropertyChanged;
        public string DisplayName { get; }
        public string Id { get; protected set; }
        public int DisplayOrderIndex { get; set; }
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

        public bool ShowIsEnabled { get; set; }

        public string TemplateName { get; }

        protected readonly T DefaultValue;

        protected T CurrentValue;

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

        private string IsEnabledKeyString => Id + "_IsEnabled";

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

        virtual public void RestoreDefaultState()
        {
            ApplicationData.Current.LocalSettings.Values.Remove(Id);
            CurrentValue = DefaultValue;
            OnPropertyChanged(nameof(Value));

            ApplicationData.Current.LocalSettings.Values.Remove(IsEnabledKeyString);
            _isEnabled = false;
            OnPropertyChanged(nameof(IsEnabled));
        }

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

        [Conditional("DEBUG")]
        private void SetDebugValue(T value)
        {
            if (!EqualityComparer<T>.Default.Equals(CurrentValue, value))
            {
                ApplicationData.Current.LocalSettings.Values[Id] = value;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}