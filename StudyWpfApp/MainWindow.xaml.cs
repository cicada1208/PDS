using Controls;
using Lib;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace StudyWpfApp
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitTab();
        }

        private void InitTab()
        {
            Dictionary<string, string> tabs = new Dictionary<string, string>();
            tabs.Add("ModelCreatePage", "Model Create");
            //tabs.Add("ApiPage", "API");
            //tabs.Add("MvvmPage", "MVVM");
            tabs.Cast<KeyValuePair<string, string>>().ToList().ForEach(tab =>
            {
                var tabItem = new ClosableTabItem(); //TabItem
                tabItem.Name = tab.Key;
                tabItem.Title = tab.Value; //tabItem.Header = tab.Value;
                MainTabControl.Items.Add(tabItem);
            });
        }

        /// <summary>
        /// 點選頁籤載入內容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!(e.OriginalSource is TabControl)) return;
            //if ((e.OriginalSource as TabControl)?.Name != MainTabControl.Name) return;
            if (e.OriginalSource != MainTabControl) return;
            var tabControl = sender as TabControl;
            var tabItem = tabControl.SelectedItem as ClosableTabItem;  //TabItem
            if (tabItem == null) return;
            if (tabItem?.Content != null) return;
            Frame frame = new Frame();
            frame.Navigate(new Uri($"{tabItem.Name}.xaml", UriKind.RelativeOrAbsolute));
            tabItem.Content = frame;

            // Page 只能使用 Window 或 Frame 做為父項目。
            //Type pageType = Type.GetType($"StudyWpfApp.{tabItem.Name}");
            //Page page = (Page)System.Activator.CreateInstance(pageType);
            //tabItem.Content = page;
        }

    }
}