using WS.Database.Domain.Company;

namespace WS.Database.Domain.Base
{
    /// <summary>
    /// Describe the entities in the system that are linked to a Tenant.
    /// </summary>
    public interface ITenantEntity
    {
        Tenant Tenant { get; set; }
    }
}
