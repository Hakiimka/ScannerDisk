using DiskScanner.Classes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace DiskScanner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DirectoryPath.Text = "Выберите путь";
        }
        public static ObservableCollection<FolderData> staticdatas { get; set; }
        public static ObservableCollection<FolderData> DBstaticdatas { get; set; }
        private FileInfo[] fiArr;
        private FileInfo[] fiArr2;
        private string path = "";
        private ScannerDBContext context;
        private ObservableCollection<FolderData> foldercollection;
        private ObservableCollection<ExeFolderData> exefoldercollection;

        private void ChangePath(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryPath.Text = dialog.SelectedPath;
                path = dialog.SelectedPath;
            }
        }


        private void Analysis(object sender, RoutedEventArgs e)
        {
            if (foldercollection == null || exefoldercollection == null)
            {
                foldercollection = new ObservableCollection<FolderData>();
                exefoldercollection = new ObservableCollection<ExeFolderData>();
            }

            try
            {
                DirectoryInfo di = new DirectoryInfo(path);
                fiArr = di.GetFiles("*", SearchOption.AllDirectories);
                fiArr2 = di.GetFiles();

                foldercollection.Add(new FolderData(fiArr, path));
                exefoldercollection.Add(new ExeFolderData(fiArr2, path));
                ExeList.ItemsSource = exefoldercollection;
                FolderList.ItemsSource = foldercollection;
                staticdatas = foldercollection;
                System.Windows.MessageBox.Show(exefoldercollection[0].timestart.ToString());
            }

            catch { System.Windows.MessageBox.Show("Ошибка. Возможно вы не выбрали путь"); }

        }

        private void SortByFoldersSize(object sender, RoutedEventArgs e)
        {
            FolderList.Items.SortDescriptions.Add(
            new SortDescription("summ", ListSortDirection.Ascending));
        }
        public void SortByFoldersFilesCount(object sender, RoutedEventArgs e)
        {
            FolderList.Items.SortDescriptions.Add(
            new SortDescription("filescount", ListSortDirection.Ascending));

        }

        private void UploadData(object sender, RoutedEventArgs e)
        {
            context = new ScannerDBContext();

            if (foldercollection != null)
            {
                for (int i = 0; i < foldercollection.Count; i++)
                {
                    ScannerDataRepository.InsertData(new ScannerData()
                    {
                        FoldersJSON = JsonConvert.SerializeObject(foldercollection[i]),
                        ExeFoldersJSON = JsonConvert.SerializeObject(exefoldercollection[i])
                    }, context);
                }

                System.Windows.MessageBox.Show("Данные успешно загружены");
            }
            else System.Windows.MessageBox.Show("Ошибка");

        }
        private void GetData(object sender, RoutedEventArgs e)
        {
            if (foldercollection == null || exefoldercollection == null)
            {
                foldercollection = new ObservableCollection<FolderData>();
                exefoldercollection = new ObservableCollection<ExeFolderData>();
            }
            exefoldercollection.Clear(); foldercollection.Clear();
            context = new ScannerDBContext();
            try
            {
                var list = context.data.ToList();

                foreach (var i in list)
                {
                    foldercollection.Add(JsonConvert.DeserializeObject<FolderData>(i.FoldersJSON));
                    exefoldercollection.Add(JsonConvert.DeserializeObject<ExeFolderData>(i.ExeFoldersJSON));
                }
                ExeList.ItemsSource = exefoldercollection;
                FolderList.ItemsSource = foldercollection;
                System.Windows.MessageBox.Show("Данные успешно загружены");
            }
            catch { System.Windows.MessageBox.Show("Произошла ошибка. Возможно вы не подключены к базе данных"); }

        }

        private void SortByExeFoldersSize(object sender, RoutedEventArgs e)
        {
            ExeList.Items.SortDescriptions.Add(
            new SortDescription("filescount", ListSortDirection.Ascending));
        }
        private void SortByExeFoldersName(object sender, RoutedEventArgs e)
        {
            ExeList.Items.SortDescriptions.Add(
            new SortDescription("exe.Count", ListSortDirection.Ascending));
        }

        private void OpenComparisonWindow(object sender, RoutedEventArgs e)
        {
            if (foldercollection != null)
            {
                staticdatas = foldercollection;
                ComparisonWindow window = new ComparisonWindow();
                window.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("Сначала проведите анализ,либо загрузите из базы данных");
            }

        }
    }
}
