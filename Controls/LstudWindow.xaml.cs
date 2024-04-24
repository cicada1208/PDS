using MahApps.Metro.Controls;
using Models;
using System.Windows.Data;
using ViewModels;

namespace Controls
{
    /// <summary>
    /// LstudWindow.xaml 的互動邏輯
    /// </summary>
    public partial class LstudWindow : MetroWindow
    {
        public LstudWindow()
        {
            InitializeComponent();

            this.DataContextChanged += (sender, args) =>
            {
                var vm = args.NewValue as LstudViewModel;
                if (vm != null)
                {
                    CollectionView groupView = CollectionViewSource.GetDefaultView(vm.LstudList) as CollectionView;
                    PropertyGroupDescription groupDesp = new PropertyGroupDescription(nameof(Mr_lstud.lstud_grade));
                    groupView?.GroupDescriptions.Clear();
                    groupView?.GroupDescriptions.Add(groupDesp);
                }
            };
        }

    }
}
