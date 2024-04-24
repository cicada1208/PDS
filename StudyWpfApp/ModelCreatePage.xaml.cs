using Lib;
using Models;
using Params;
using Repositorys;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StudyWpfApp
{
    /// <summary>
    /// ModelCreatePage.xaml 的互動邏輯
    /// </summary>
    public partial class ModelCreatePage : Page
    {
        private FileUtil _fileUtil;
        public FileUtil FileUtil =>
            _fileUtil ?? (_fileUtil = new FileUtil());

        private ObservableCollection<RecSt> _optionList;
        /// <summary>
        /// 選項清單
        /// </summary>
        public ObservableCollection<RecSt> OptionList
        {
            get
            {
                if (_optionList == null)
                {
                    _optionList = new ObservableCollection<RecSt>();
                    _optionList.Add(new RecSt { Status = "0", StatusName = "C#" });
                    _optionList.Add(new RecSt { Status = "1", StatusName = "WPF" });
                    _optionList.Add(new RecSt { Status = "2", StatusName = "TS" });
                }
                return _optionList;
            }
            set => _optionList = value;
        }

        private bool ModelCsToTs { get; set; } = false;

        public ModelCreatePage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            DBParam.DBName dbNames = new DBParam.DBName();
            var fields = typeof(DBParam.DBName).GetFields();
            fields.ToList().ForEach(field => DBComboBox.Items.Add(field.GetValue(dbNames)));

            OptionComboBox.ItemsSource = OptionList;
            OptionComboBox.DisplayMemberPath = "StatusName";
            OptionComboBox.SelectedValuePath = "Status";
            OptionComboBox.SelectedIndex = 0;
        }

        private void ModelCreateButton_Click(object sender, RoutedEventArgs e)
        {
            //string sql = @"
            //select top 1 u.userId, u.userName, r.roleId
            //from ni_Users as u
            //left join ni_UserRole as r
            //on (u.userId=r.userId)
            //where u.userId='10964'";

            //string sql = @"
            //select top 1 *
            //from ni_Users as u
            //where u.userId='10964'";

            //SchemaRepository schemaRep = new SchemaRepository();
            //string model = schemaRep.CreateModel(sql, DBParam.DBName.NIS);
            //ModelTextBox.Text = model;

            if (SQLTextBox.Text == string.Empty) return;
            if (DBComboBox.SelectedItem == null) return;

            var result = ApiUtil.HttpClientEx<ApiResult<string>>(
                RouteParam.Service(),
                RouteParam.Schema.CreateModel,
                new Schema
                {
                    Sql = SQLTextBox.Text,
                    DBName = DBComboBox.SelectedItem.ToString()
                },
                new { option = OptionComboBox.SelectedValue });

            ModelTextBox.Text = result.Data;
            MessageBox.Show(result.Msg);
            ModelCsToTs = false;
        }

        private void ModelExportButton_Click(object sender, RoutedEventArgs e) =>
            FileUtil.WriteFile(ModelTextBox.Text, filter: ModelCsToTs ? "ts" :
                (OptionComboBox.SelectedValue.NullableToStr() == "2" ? "ts" : "cs"));

        private void ModelCsToTsButton_Click(object sender, RoutedEventArgs e)
        {
            string csModel = ModelTextBox.Text;
            SchemaRepository schemaRepository = new SchemaRepository();
            ModelTextBox.Text = schemaRepository.ModelCsToTs(csModel);
            ModelCsToTs = true;
        }

    }
}
