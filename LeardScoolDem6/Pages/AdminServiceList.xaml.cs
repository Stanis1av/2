using LeardScoolDem6.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for AdminServiceList.xaml
    /// </summary>
    public partial class AdminServiceList : Page
    {
        bool IsUpdate = false;
        public AdminServiceList()
        {
            InitializeComponent();


            CbSort.Items.Add("По возрастанию стоимости");
            CbSort.Items.Add("По убыванию стоимости");
            CbSort.SelectedIndex = 0;

            CbFilter.Items.Add("Все");
            CbFilter.Items.Add("Скидка от 0% до 5%");
            CbFilter.Items.Add("Скидка от 5% до 15%");
            CbFilter.Items.Add("Скидка от 15% до 30%");
            CbFilter.Items.Add("Скидка от 30% до 70%");
            CbFilter.Items.Add("Скидка от 70% до 100%");
            CbFilter.SelectedIndex = 0;

            IsUpdate = true;

            UpdateData();
        }

        public async void UpdateData()
        {
            tbNotFound.Visibility = Visibility.Hidden;
            tbUpdate.Visibility = Visibility.Visible;
            List<Service> services = await UpdateDataAsync(TbSearch.Text, CbSort.SelectedIndex, CbFilter.SelectedIndex);
            LvList.ItemsSource = services;
            TbCount.Text = LvList.Items.Count + " из " + EfModel.Init().Services.Count();
            tbUpdate.Visibility = Visibility.Hidden;
            if (LvList.Items.Count == 0)
                tbNotFound.Visibility = Visibility.Visible;
        }
        public async Task<List<Service>> UpdateDataAsync(string Search, int selectSort, int selectFilter)
        {
            EfModel model = new EfModel();
            IQueryable<Service> services = model.Services
                .Where(s => s.Title.Contains(Search) || s.Description.Contains(Search));
            switch (selectSort)
            {
                case 0:
                    services = services.OrderBy(s => s.Cost);
                    break;
                case 1:
                    services = services.OrderByDescending(s => s.Cost);
                    break;
            }

            switch (selectFilter)
            {
                case 1:
                    services = services.Where(s => s.Discount >= 0 && s.Discount < 5);
                    break;
                case 2:
                    services = services.Where(s => s.Discount >= 5 && s.Discount < 15);
                    break;
                case 3:
                    services = services.Where(s => s.Discount >= 15 && s.Discount < 30);
                    break;
                case 4:
                    services = services.Where(s => s.Discount >= 30 && s.Discount < 70);
                    break;
                case 5:
                    services = services.Where(s => s.Discount >= 70 && s.Discount < 100);
                    break;
            }

            return await services.ToListAsync();
        }

        private void cbSerchChange(object sender, SelectionChangedEventArgs e)
        {
            if (IsUpdate)
                UpdateData();
        }

        private void tbSearchChange(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtServiceDelete(object sender, RoutedEventArgs e)
        {
            Button btService = sender as Button;
            Service service = btService.DataContext as Service;
            if (service.ClientServices.Count > 0) {
                MessageBox.Show("Удаление невозможно! Услуга участвует в записях!");
                return;
            }
            if (service.ServicePhotoes.Count > 0)
            {
                foreach (ServicePhoto photo in service.ServicePhotoes.ToList()) {
                    EfModel.Init().ServicePhotoes.Remove(photo);
                }
            }
            EfModel.Init().Services.Remove(service);
            EfModel.Init().SaveChanges();
        }

        private void btAddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceAddPage(new Service()));
        }

        private void BtEditClick(object sender, RoutedEventArgs e)
        {
            Button btService = sender as Button;
            Service service = btService.DataContext as Service;
            NavigationService.Navigate(new ServiceAddPage(service));
        }

        private void PageChangeVisisble(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateData();
        }
    }
}
