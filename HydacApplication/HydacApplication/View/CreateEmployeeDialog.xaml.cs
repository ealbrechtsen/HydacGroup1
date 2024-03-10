using HydacApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HydacApplication.View
{
    /// <summary>
    /// Interaction logic for CreateEmployeeDialog.xaml
    /// </summary>
    public partial class CreateEmployeeDialog : Window
    {
        MainViewModel mvm;
        public CreateEmployeeDialog(MainViewModel mvm)
        {
            InitializeComponent();
            this.mvm = mvm;
            DataContext = mvm;
        }
        private void CreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            mvm.CreateEmployee(txtFirstName.Text, txtLastName.Text, txtCPRNum.Text, txtPhoneNum.Text, txtEmail.Text, txtAddress.Text, mvm.SelectedDepartment);
            this.DialogResult = true;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
