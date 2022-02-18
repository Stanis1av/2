namespace LeardScoolDem6.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Windows;

    [Table("ServicePhoto")]
    public partial class ServicePhoto
    {
        public int ID { get; set; }

        public int ServiceID { get; set; }

        [Required]
        [StringLength(1000)]
        public string PhotoPath { get; set; }
        // public byte[] Photo { get; set; }

        [NotMapped]
        public byte[] Photo
        {
            get
            {
                try
                {
                    return File.ReadAllBytes(PhotoPath);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    if (File.Exists(PhotoPath))
                    {
                        FileInfo info = new FileInfo(PhotoPath);

                        PhotoPath = "Услуги школы\\" + info.Name + "_" + Guid.NewGuid() + "." + info.Extension;
                    }
                    File.WriteAllBytes(PhotoPath, value);
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить изображение по пути " + PhotoPath + "!", "Не удалось сохранить изображение"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public virtual Service Service { get; set; }
    }
}
