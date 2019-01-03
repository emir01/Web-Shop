using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WS.Database.Access.Interface.Repositories;
using WS.Database.Domain.Tagging;

namespace WS.Database.Access.Core.Repos
{
    public class TagTypeRepository : GenericRepository<TagType, DbContext>, ITagTypeRepository
    {
        public TagTypeRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Return all the tags defined in the system
        /// </summary>
        /// <returns></returns>
        public IQueryable<TagType> Query()
        {
            return Set;
        }

        /// <summary>
        /// Should return the tag types define only for the given category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public IEnumerable<TagType> QueryByCategoryId(int categoryId)
        {
            var tags = (from tag in Set.Include(tt => tt.Categories)
                        where tag.Categories.Any(c => c.Id == categoryId)
                        select tag).AsNoTracking();

            return tags;
        }

        /// <summary>
        /// Return a given tag for the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TagType GetById(int id)
        {
            var tagType = Set.Include(t => t.Categories).FirstOrDefault(c => c.Id == id);
            return tagType;
        }

        /// <summary>
        /// Insert a new tag type into the set/database.
        /// </summary>
        /// <param name="tagType"></param>
        /// <returns></returns>
        public TagType Create(TagType tagType)
        {
            var created = CreateEntity(tagType);

            return created;
        }

        /// <summary>
        /// Attach and update the tag type.
        /// </summary>
        /// <param name="tagType"></param>
        /// <returns></returns>
        public TagType Update(TagType tagType)
        {
            AttachAndModify(tagType);

            SetStateUnchanged(tagType.Categories);

            return tagType;
        }

        /// <summary>
        /// Delete the specified tag type from the system.
        /// 
        /// Will delete any product information asosiated with the tag
        /// </summary>
        /// <param name="tagType"></param>
        /// <returns></returns>
        public TagType Delete(TagType tagType)
        {
            return DeleteEntity(tagType);
        }
    }
}