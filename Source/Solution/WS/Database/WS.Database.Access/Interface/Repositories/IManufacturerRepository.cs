using System.Linq;
using WS.Database.Domain.Categorization;

namespace WS.Database.Access.Interface.Repositories
{
    public interface IManufacturerRepository
    {
        IQueryable<Manufacturer> Query();
        
        Manufacturer Create(Manufacturer manufacturer);

        Manufacturer Update(Manufacturer manufacturer);
        
        Manufacturer Delete(Manufacturer manufacturer);
    }
}
