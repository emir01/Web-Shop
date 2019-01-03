using System.Data.Entity.ModelConfiguration;

namespace WS.Database.Configuration
{
    public abstract class BaseWsEntityTypeConfiguration<T>:EntityTypeConfiguration<T> where T : class
    {
    }
}
