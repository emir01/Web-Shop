using System;
using System.ComponentModel.DataAnnotations;

namespace WS.Database.Domain.Base
{
    public abstract class Entity
    {
        #region Props
        
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// All entity can have a string alias for code referential purposes.
        /// </summary>
        public string Alias { get; set; }

        public bool? Status { get; set; }

        public DateTime? DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Any object in the database can have additional XML related data
        /// </summary>
        public string XmlData { get; set; }
        
        /// <summary>
        /// All entities are going to have a IsSystem flag denoting special permissions to remove/manage
        /// the entity data.
        /// 
        /// Eg. Category data marked as IsSystem is never going to be able to be deleted by users/tenant users
        /// </summary>
        public bool? IsSystem { get; set; }
        
        #endregion
        
        #region Ctor
        
        protected Entity()
        {
            // set the default status to true. 
            Status = true;
        }
        
        #endregion
    }
}
