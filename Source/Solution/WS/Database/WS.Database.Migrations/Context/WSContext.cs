using System.Data.Entity;
using System.Reflection;
using WS.Database.Configuration;
using WS.Database.Configuration.Conventions;
using WS.Database.Domain.Availability;
using WS.Database.Domain.Categorization;
using WS.Database.Domain.Company;
using WS.Database.Domain.Core;
using WS.Database.Domain.Products;
using WS.Database.Domain.Tagging;

namespace WS.Database.Bootstrap.Context
{
    /// <summary>
    /// The code first database context object containing all the domain objects Db Sets.
    /// </summary>
    public class WsContext : DbContext, IWsContext
    {
        #region Ctor


        public WsContext()
            : base(ConnectionStringNames.WorkingConnectionString)
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public WsContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        #endregion

        #region Core

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserProduct> UserProducts { get; set; }

        public virtual DbSet<AppUrl> Urls { get; set; }

        public virtual DbSet<AppImage> ImageDatas { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        #endregion

        #region Company

        public virtual DbSet<Tenant> Tenants { get; set; }

        public virtual DbSet<TenantCategory> TenantCategories { get; set; }

        public virtual DbSet<TenantManufacturer> TenantManufacturers { get; set; }

        public virtual DbSet<TenantTagType> TenantTagTypes { get; set; }

        public virtual DbSet<TenantMetricTemplate> TenantMetricTemplates { get; set; }

        public virtual DbSet<BusinessDomain> BusinessDomains { get; set; }

        public virtual DbSet<TenantUrlHistory> TenantUrlHistories { get; set; }

        #endregion

        #region Categorization

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        #endregion

        #region Availability

        public virtual DbSet<AvailabilityMetricTemplate> AvailabilityMetricTemplates { get; set; }

        public virtual DbSet<AvailabilityMetricUnit> AvailabilityMetricUnits { get; set; }

        #endregion

        #region Tagging

        public virtual DbSet<TagType> TagTypes { get; set; }

        #endregion

        #region Product

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<ProductMetricValue> ProductMetricValues { get; set; }

        public virtual DbSet<ProductTagValue> ProductTagValues { get; set; }

        #endregion

        #region Overides


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(BaseWsEntityTypeConfiguration<>)));
            modelBuilder.Conventions.Add(new ForeignKeyNamingConvention());
        }

        #endregion
    }
}
