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

namespace HydacApplication.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Fields for the different views and the current view.
        private CreateDepartmentDialog cdd;
        private CreateEmployeeDialog cmd;
        private CreateKeyChipDialog ckc;
        private MainViewModel mvm;
        public MainWindow()
        {
            mvm = new MainViewModel();
            DataContext = mvm;
            InitializeComponent();
            // Content is not necessarily Strings, so we explicitly tells them they are strings in the code behind. Necessary for checks in SwitchList method.
            SwitchList.Content = "Se Inaktive";
            SetStatus.Content = "Sæt Inaktiv";
            lvEmployees.ItemsSource = mvm.employeesVM;
        }


        private void CreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            // Instantiates Department View
            cdd = new CreateDepartmentDialog(mvm);
            // opens CreateDepartment View
            cdd.ShowDialog();
        }

        private void CreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            mvm.SelectedDepartment = null;
            cmd = new CreateEmployeeDialog(mvm);
            cmd.ShowDialog();
        }
        private void CreateKeyChip_Click(object sender, RoutedEventArgs e)
        {
            ckc = new CreateKeyChipDialog(mvm);
            ckc.ShowDialog();
        }
        private void SetStatus_Click(object sender, RoutedEventArgs e)
        {
            // Flips the status of the selected employee. Throwing them into the correct List and updates it in the repo and database.
            mvm.SetStatus(mvm.SelectedEmployee);
            mvm.ShiftsVM.Clear();
        }
        private void SwitchList_Click(object sender, RoutedEventArgs e)
        {
            // Switches between the lists depicting active and inactive employees. Updates several labels in the UI and rebinds the ItemsSource.
            lvEmployees.ItemsSource = SwitchList.Content == "Se Inaktive" ? mvm.unemployedEmployeesVM : mvm.employeesVM; 
            SwitchList.Content = SwitchList.Content == "Se Inaktive" ? "Se Aktive" : "Se Inaktive";
            SetStatus.Content = SetStatus.Content == "Sæt Inaktiv" ? "Sæt Aktiv" : "Sæt Inaktiv";
            txtTitle.Text = txtTitle.Text == "Aktive Medarbejdere" ? "Inaktive Medarbejdere" : "Aktive Medarbejdere";
        }

        private void CloseProgram_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}