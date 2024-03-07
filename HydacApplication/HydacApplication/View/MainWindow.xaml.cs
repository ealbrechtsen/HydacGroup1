using HydacApplication.View;
using HydacApplication.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HydacApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CreateDepartmentDialog cdd;
        private CreateEmployeeDialog cmd;
        private MainViewModel mvm;
        public MainWindow()
        {
            InitializeComponent();
            mvm = new MainViewModel();
            DataContext = mvm;
        }


        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            cdd = new CreateDepartmentDialog(mvm);
            cdd.ShowDialog();
        }

        private void CreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            cmd = new CreateEmployeeDialog(mvm);
            cmd.ShowDialog();
        }
    }
}