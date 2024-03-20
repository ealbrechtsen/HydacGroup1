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
using HydacApplication.View;
using HydacApplication.ViewModel;

namespace HydacApplication.View
{
    /// <summary>
    /// Interaction logic for CreateKeyChipDialog.xaml
    /// </summary>
    public partial class CreateKeyChipDialog : Window
    {
        private MainViewModel mvm;
        public CreateKeyChipDialog(MainViewModel mvm)
        {
            this.mvm = mvm;
            InitializeComponent();
            DataContext = mvm;
        }

        private void CreateKeyChip_Click(object sender, RoutedEventArgs e)
        {
            mvm.CreateKeyChip(long.Parse(txtNøglebrikId.Text));
            DialogResult = true;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
