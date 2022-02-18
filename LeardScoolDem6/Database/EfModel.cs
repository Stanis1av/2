using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LeardScoolDem6.Database
{
    public partial class EfModel : DbContext
    {
        private static EfModel InstanceAsync;
        public static EfModel InitAsync()
        {
            if (InstanceAsync == null)
            {
                InstanceAsync = new EfModel();
            }
            return InstanceAsync;
        }

        private static EfModel Instance;
        public static EfModel Init() { 
            if(Instance == null)
            {
                Instance = new EfModel();
            }
            return Instance;
        }
        public EfModel()
            : base("name=EfModel1")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientService> ClientServices { get; set; }
        public virtual DbSet<DocumentByService> DocumentByServices { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPhoto> ProductPhotoes { get; set; }
        public virtual DbSet<ProductSale> ProductSales { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServicePhoto> ServicePhotoes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.RegistrationDate)
                .HasPrecision(6);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.GenderCode)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.PhotoPath)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ClientServices)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Clients)
                .Map(m => m.ToTable("TagOfClient").MapLeftKey("ClientID").MapRightKey("TagID"));

            modelBuilder.Entity<ClientService>()
                .Property(e => e.StartTime)
                .HasPrecision(6);

            modelBuilder.Entity<ClientService>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<ClientService>()
                .HasMany(e => e.DocumentByServices)
                .WithRequired(e => e.ClientService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentByService>()
                .Property(e => e.DocumentPath)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Gender>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.Gender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manufacturer>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MainImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductPhotoes)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductSales)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product1)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("AttachedProduct").MapLeftKey("MainProductID").MapRightKey("AttachedProductID"));

            modelBuilder.Entity<ProductPhoto>()
                .Property(e => e.PhotoPath)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSale>()
                .Property(e => e.SaleDate)
                .HasPrecision(6);

            modelBuilder.Entity<Service>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.MainImagePath)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ClientServices)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServicePhotoes)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServicePhoto>()
                .Property(e => e.PhotoPath)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Color)
                .IsUnicode(false);
        }
    }
}
