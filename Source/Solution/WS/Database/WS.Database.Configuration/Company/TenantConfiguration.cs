using WS.Database.Domain.Company;

namespace WS.Database.Configuration.Company
{
    public class TenantConfiguration : BaseWsEntityTypeConfiguration<Tenant>
    {
        public TenantConfiguration()
        {
            HasMany(company => company.BusinessDomains)
                .WithMany(bd => bd.Tenants)
                .Map(mc =>
                {
                    mc.MapLeftKey("TenantId");
                    mc.MapRightKey("BusinessDomainId");
                    mc.ToTable("TenantBusinessDomains");
                });

            HasMany(t => t.Countries).WithMany()
                .Map(mc =>
                {
                    mc.MapLeftKey("TenantId");
                    mc.MapRightKey("CountryId");
                    mc.ToTable("TenantCountries");
                });
        }
    }
}
