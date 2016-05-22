using System;
using Windows.UI.Xaml.Data;

namespace TestHook.TestHook
{
    public class TestHookGroupEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return TestHookManager.Instance.GetDescription((TestHookManager.Groups)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}