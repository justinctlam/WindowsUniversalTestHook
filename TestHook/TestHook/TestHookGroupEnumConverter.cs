using System;
using Windows.UI.Xaml.Data;

namespace TestHook.TestHook
{
    /// <summary>
    /// Converts a group enum to a displayable string
    /// </summary>
    public class TestHookGroupEnumConverter : IValueConverter
    {
        /// <summary>
        /// Converts group enum to string
        /// </summary>
        /// <param name="value">Group enum</param>
        /// <param name="targetType">String</param>
        /// <param name="parameter">Not used</param>
        /// <param name="language">Not used</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return TestHookManager.Instance.GetDescription((TestHookManager.Groups)value);
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