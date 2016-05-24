using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TestHook.TestHook
{
    /// <summary>
    /// Template selector for the different types of test hooks
    /// </summary>
    public class TestHookDataTemplateSelector : DataTemplateSelector
    {
        private readonly TestHookControl _parent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent">TestHookControl</param>
        public TestHookDataTemplateSelector(TestHookControl parent)
        {
           _parent = parent;
        }

        /// <summary>
        /// Select the template for use
        /// </summary>
        /// <param name="item">ITestHookModel</param>
        /// <param name="container">Container parameter</param>
        /// <returns>DataTemplate</returns>
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var model = item as ITestHookModel;
            if (model != null)
            {
                var dataTemplate = model.TemplateName;
                return _parent.Resources[dataTemplate] as DataTemplate;
            }

            return null;
        }
    }

    /// <summary>
    /// Test hook control class
    /// </summary>
    public partial class TestHookControl : UserControl
    {
        private readonly TestHookMenuViewModel _viewModel = new TestHookMenuViewModel();
        private TestHookManager.Groups _currentGroup;

        /// <summary>
        /// Constructor
        /// </summary>
        public TestHookControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public void Initialize()
        {
            GoToRoot();
        }

        /// <summary>
        /// Go back to root menu
        /// </summary>
        private void GoToRoot()
        {
            _currentGroup = TestHookManager.Groups.None;
            RestoreButton.Content = "Restore All";
            BackButton.Visibility = Visibility.Collapsed;

            foreach (var i in TestHookStackPanel.Children)
            {
                var item = i as ItemsControl;
                if (item != null)
                {
                    item.ItemsSource = null;
                }
            }

            var itemsControl = new ItemsControl
            {
                ItemsSource = _viewModel.GroupList
            };

            var dataTemplate = Resources["HyperLinkButtonTemplate"] as DataTemplate;
            itemsControl.ItemTemplate = dataTemplate;
            TestHookStackPanel.Children.Add(itemsControl);
        }

        /// <summary>
        /// Event when a group is clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RoutedEventArgs parameter</param>
        private void OnClickHyperLink(object sender, RoutedEventArgs e)
        {
            BackButton.Visibility = Visibility.Visible;

            foreach (var i in TestHookStackPanel.Children)
            {
                var itemsControl = i as ItemsControl;
                if (itemsControl != null)
                {
                    itemsControl.ItemsSource = null;
                }
            }

            var hyperLink = sender as HyperlinkButton;
            if (hyperLink != null)
            {
                var isGroupParameter = hyperLink.Tag is TestHookManager.Groups;
                if (isGroupParameter)
                {
                    _currentGroup = (TestHookManager.Groups)hyperLink.Tag;
                    RestoreButton.Content = "Restore current";

                    var itemsControl = new ItemsControl
                    {
                        ItemsSource = _viewModel.GetListForGroup(_currentGroup),
                        ItemTemplateSelector = new TestHookDataTemplateSelector(this)
                    };

                    TestHookStackPanel.Children.Add(itemsControl);
                }
            }
        }

        /// <summary>
        /// Event when restore button is pressed
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RoutedEventArgs parameter</param>
        private void OnClickRestoreDefaults(object sender, RoutedEventArgs e)
        {
            _viewModel.RestoreToDefaults(_currentGroup);
        }

        /// <summary>
        /// Event when click back is pressed
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RoutedEventArgs parameter</param>
        private void OnClickBack(object sender, RoutedEventArgs e)
        {
            GoToRoot();
        }
    }
}