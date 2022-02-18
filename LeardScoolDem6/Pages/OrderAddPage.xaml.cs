using LeardScoolDem6.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for OrderAddPage.xaml
    /// </summary>
    public partial class OrderAddPage : Page
    {
        ClientService Service;
        public OrderAddPage(Service service)
        {
            Service = new ClientService { ServiceID = service.ID, StartTime = DateTime.Now };
            InitializeComponent();
            DataContext = Service;
            CbClients.ItemsSource = EfModel.Init().Clients.ToList();
        }

        private void BtSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                EfModel.Init().ClientServices.Add(Service);
                EfModel.Init().SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(String.Join(", ", ex.EntityValidationErrors.Last().ValidationErrors.Select(ve => ve.ErrorMessage)));
            }
            
        }
    }
}
