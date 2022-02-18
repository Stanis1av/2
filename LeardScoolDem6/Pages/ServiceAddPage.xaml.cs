using LeardScoolDem6.Database;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
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
    /// Interaction logic for ServiceAddPage.xaml
    /// </summary>
    public partial class ServiceAddPage : Page
    {
        Service Service;
        public ServiceAddPage(Service service)
        {
            Service = EfModel.Init().Services.Find(service.ID);
            InitializeComponent();
            DataContext = Service;
        }

        /// <summary>
        /// Изменение картинки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageCnageClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog { Filter = "All Files|*.*" };
            if (openFile.ShowDialog() == true)
            {
                Service.MainImagePath = "Услуги школы\\"+new FileInfo(openFile.FileName).Name;
                Service.MainImage = File.ReadAllBytes(openFile.FileName);
                
            }
        }

        /// <summary>
        /// Добавление дополнительной картинки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtImageAdd(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog { Filter = "All Files|*.*" };
            if (openFile.ShowDialog() == true)
                Service.ServicePhotoes.Add(new ServicePhoto
                {
                    PhotoPath = "Услуги школы\\" + new FileInfo(openFile.FileName).Name,
                    Photo = File.ReadAllBytes(openFile.FileName)
                }
                );
            lvPhotos.Items.Refresh();
        }

        /// <summary>
        /// Удаление допнительной картинки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtImageRem(object sender, RoutedEventArgs e)
        {
            ServicePhoto servicePhoto = lvPhotos.SelectedItem as ServicePhoto;
            if (servicePhoto.ID == 0)
                Service.ServicePhotoes.Remove(servicePhoto);
            else
                EfModel.Init().ServicePhotoes.Remove(servicePhoto);
            lvPhotos.Items.Refresh();
        }

        /// <summary>
        /// Сохранение услуги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Service.ID == 0)
                    EfModel.Init().Services.Add(Service);
                EfModel.Init().SaveChanges();
            }catch(DbEntityValidationException ex)
            {
                MessageBox.Show(String.Join(", ", ex.EntityValidationErrors.Last().ValidationErrors.Select(ve => ve.ErrorMessage)));
            }
        }
    }
}
