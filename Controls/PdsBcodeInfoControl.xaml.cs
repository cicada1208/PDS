using Models;
using System.Windows.Controls;
using System.Windows.Data;
using ViewModels;

namespace Controls
{
    /// <summary>
    /// PdsBcodeInfoControl.xaml 的互動邏輯
    /// </summary>
    public partial class PdsBcodeInfoControl : UserControl
    {
        public PdsBcodeInfoControl()
        {
            InitializeComponent();

            this.DataContextChanged += (sender, args) =>
            {
                var vm = args.NewValue as PdsBcodeInfoViewModel;
                if (vm != null)
                {
                    CollectionView dayGroupView = CollectionViewSource.GetDefaultView(vm.DayList) as CollectionView;
                    PropertyGroupDescription dayGroupDesp = new PropertyGroupDescription(nameof(Ch_bcode_daygrp.day_group));
                    dayGroupView?.GroupDescriptions.Clear();
                    dayGroupView?.GroupDescriptions.Add(dayGroupDesp);

                    foreach (var d in vm.DayList)
                    {
                        CollectionView codeGroupView = CollectionViewSource.GetDefaultView(d.codeList) as CollectionView;
                        PropertyGroupDescription codeGroupDesp = new PropertyGroupDescription(nameof(Ch_bcode_codegrp.code_group));
                        codeGroupView?.GroupDescriptions.Clear();
                        codeGroupView?.GroupDescriptions.Add(codeGroupDesp);
                    }
                }
            };
        }

        private PdsBcodeInfoViewModel PdsBcodeInfoViewModel =>
            this.DataContext as PdsBcodeInfoViewModel;
    }
}
