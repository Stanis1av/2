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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeardScoolDem6.Pages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void btAdminLogin(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text == "0000")
                NavigationService.Navigate(new AdminServiceList());
            else
                MessageBox.Show("Неверный код доутупа!");
        }

        private void btUserLogin(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceListPage());
        }
    }
}
