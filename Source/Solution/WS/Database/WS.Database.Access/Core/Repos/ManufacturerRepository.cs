using System.Data.Entity;
using System.Linq;
using WS.Database.Access.Interface.Repositories;
using WS.Database.Domain.Categorization;

namespace WS.Database.Access.Core.Repos
{
    public class ManufacturerRepository : GenericRepository<Manufacturer, DbContext>, IManufacturerRepository
    {
        public ManufacturerRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<Manufacturer> Query()
        {
            return Set;
        }

        public Manufacturer Create(Manufacturer manufacturer)
        {
            var created = CreateEntity(manufacturer);
            return created;
        }

        public Manufacturer Update(Manufacturer manufacturer)
        {
            AttachAndModify(manufacturer);
            return manufacturer;
        }

        public Manufacturer Delete(Manufacturer manufacturer)
        {
            return DeleteEntity(manufacturer);
        }
    }
}