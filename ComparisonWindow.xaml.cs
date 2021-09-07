using DiskScanner.Classes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DiskScanner
{
    /// <summary>
    /// Логика взаимодействия для ComparisonWindow.xaml
    /// </summary>
    public partial class ComparisonWindow : Window
    {
        public ComparisonWindow()
        {
            InitializeComponent();
        }
        private ObservableCollection<FolderData> foldercollection;
        private ObservableCollection<FolderData> DBfoldercollection;
        private ScannerDBContext context;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foldercollection = MainWindow.staticdatas;
            context = new ScannerDBContext();

            var list = context.data.ToList();
            DBfoldercollection = new ObservableCollection<FolderData>();
            foreach (var i in list)
            {
                DBfoldercollection.Add(JsonConvert.DeserializeObject<FolderData>(i.FoldersJSON));
            }
            OldFolderList.ItemsSource = DBfoldercollection;
            CurrentFolderList.ItemsSource = foldercollection;
        }
       
    }
}
