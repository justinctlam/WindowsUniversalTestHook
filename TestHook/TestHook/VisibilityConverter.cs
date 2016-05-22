using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TestHook.TestHook
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var visible = false;
            if (value is bool)
            {
                visible = (bool) value;
            }

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}