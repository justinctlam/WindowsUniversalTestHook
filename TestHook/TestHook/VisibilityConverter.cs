using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TestHook.TestHook
{
    /// <summary>
    /// Converts a boolean value to a visibility value
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to a visibility value
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Boolean</param>
        /// <param name="parameter">Not used</param>
        /// <param name="language">Not used</param>
        /// <returns>Visible if true, Collpased if false</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var visible = false;
            if (value is bool)
            {
                visible = (bool) value;
            }

            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// ConvertBack
        /// </summary>
        /// <param name="value">Not used</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="language">Not used</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}