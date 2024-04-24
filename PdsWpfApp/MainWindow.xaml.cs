using MahApps.Metro.Controls;
using System.Windows;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //FunctionViewModel.ShowWindow = ShowWindow;
            this.SizeChanged += MainWindow_SizeChanged;
        }

        private PdsMainViewModel PdsMainViewModel =>
             this.DataContext as PdsMainViewModel;

        private FunctionViewModel _functionViewModel;
        private FunctionViewModel FunctionViewModel =>
            _functionViewModel ?? (_functionViewModel = TryFindResource("FunctionViewModel") as FunctionViewModel);


        private void FunctionExpander_Expanded(object sender, RoutedEventArgs e)
        {
            FunctionColumn.Width = new GridLength(1, GridUnitType.Star);
            ContentColumn.Width = new GridLength(6, GridUnitType.Star);
        }

        private void FunctionExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            FunctionColumn.Width = new GridLength(30, GridUnitType.Auto);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void ZoomButton_Click(object sender, RoutedEventArgs e)
        {
            this.ZoomPopup.IsOpen = true;
            this.ZoomSlider.Focus();
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ZoomView();
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ZoomView();
        }

        private void ZoomView()
        {
            double zoom = (this.ZoomSlider.Value - 100) * 7;
            double widthOffset, heightOffset;
            double width, height;

            if (this.WindowState == WindowState.Maximized)
            {
                widthOffset = 15;
                heightOffset = 45;
            }
            else
            {
                widthOffset = 0;
                heightOffset = 30; // 視窗title
            }

            width = this.ActualWidth - widthOffset - zoom;
            width = width < 0 ? 0 : width;
            height = (this.ActualHeight - heightOffset) * (this.ActualWidth - widthOffset - zoom) / (this.ActualWidth - widthOffset);
            height = height < 0 ? 0 : height;
            this.MainDockPanel.Width = width;
            this.MainDockPanel.Height = height;
        }

        //private bool? ShowWindow(string content)
        //{
        //    return true;
        //}

        //private void FunctionTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    // Binding ItemsSource
        //    if (e.OriginalSource != FunctionTreeView) return;
        //    var selectedFunc = e.NewValue as Function;
        //    if (selectedFunc == null) return;
        //    var selectedTabItem = ContentTabControl.Items.Cast<ClosableTabItem>()
        //        .Where(tabItem => tabItem.Name == selectedFunc.Name).FirstOrDefault();
        //    if (selectedTabItem != null)
        //    {
        //        selectedTabItem.IsSelected = true;
        //        // If selectedTabItem.IsSelected = true doesn't always really work. 
        //        // The problem is some subtle thing about the order of property changes. 
        //        // To work around it you need to let the WPF invoke your tab-selection code in its own time.
        //        //Dispatcher.BeginInvoke((Action)(() => selectedTabItem.IsSelected = true));
        //    }
        //    else
        //    {
        //        if (selectedFunc.Type == FunctionParam.FuncType.None || 
        //            selectedFunc.Content.NullableToStr() == string.Empty) return;
        //        switch (selectedFunc.Type)
        //        {
        //            case FunctionParam.FuncType.Inner:
        //                var newTabItem = new ClosableTabItem();
        //                newTabItem.Name = selectedFunc.Name;
        //                newTabItem.Title = selectedFunc.Title;
        //                Frame frame = new Frame();
        //                frame.Navigate(new Uri($"{selectedFunc.Content}.xaml", UriKind.RelativeOrAbsolute));
        //                newTabItem.Content = frame;
        //                ContentTabControl.Items.Add(newTabItem);
        //                newTabItem.IsSelected = true;
        //                break;
        //        }
        //    }

        //    //// No Binding ItemsSource
        //    //if (e.OriginalSource != FunctionTreeView) return;
        //    //var selectedTreeViewItem = e.NewValue as TreeViewItem;
        //    //if (selectedTreeViewItem == null) return;
        //    //var selectedTabItem = ContentTabControl.Items.Cast<ClosableTabItem>()
        //    //    .Where(tabItem => tabItem.Name == selectedTreeViewItem.Name).FirstOrDefault();
        //    //if (selectedTabItem != null)
        //    //{
        //    //    selectedTabItem.IsSelected = true;
        //    //    // If selectedTabItem.IsSelected = true doesn't always really work. 
        //    //    // The problem is some subtle thing about the order of property changes. 
        //    //    // To work around it you need to let the WPF invoke your tab-selection code in its own time.
        //    //    //Dispatcher.BeginInvoke((Action)(() => selectedTabItem.IsSelected = true));
        //    //}
        //    //else
        //    //{
        //    //    var newTabItem = new ClosableTabItem();
        //    //    newTabItem.Name = selectedTreeViewItem.Name;
        //    //    newTabItem.Title = selectedTreeViewItem.Header as string;
        //    //    Frame frame = new Frame();
        //    //    frame.Navigate(new Uri($"{newTabItem.Name}.xaml", UriKind.RelativeOrAbsolute));
        //    //    newTabItem.Content = frame;
        //    //    ContentTabControl.Items.Add(newTabItem);
        //    //    newTabItem.IsSelected = true;
        //    //}
        //}

    }
}
