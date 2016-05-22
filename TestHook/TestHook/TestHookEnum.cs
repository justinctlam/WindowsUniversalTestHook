using System;
using System.Collections.Generic;
using System.Linq;

namespace TestHook.TestHook
{
    /// <summary>
    /// Test hook for enum values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestHookEnum<T> : TestHookModelBase<int>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Reference ID</param>
        /// <param name="displayName">UI display name</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="showIsEnabled">Should show enabled/disabled UI.</param>
        public TestHookEnum(string id, string displayName, int defaultValue, bool showIsEnabled = false)
           : base(id, displayName, defaultValue, "TestHookEnumTemplate", showIsEnabled)
        {
            EnumStrings = Enum.GetNames(typeof(T)).ToList();
        }

        /// <summary>
        /// Returns a list of enum strings
        /// </summary>
        public List<string> EnumStrings { get; }

        /// <summary>
        /// Get selected item in the list
        /// </summary>
        /// <returns>The enum value</returns>
        public T GetSelectedItem()
        {
            return (T)Enum.Parse(typeof(T), EnumStrings.ElementAt(Value), true);
        }
    }
}