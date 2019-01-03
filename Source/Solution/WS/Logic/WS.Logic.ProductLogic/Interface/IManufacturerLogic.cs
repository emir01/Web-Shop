using System.Collections.Generic;
using WS.Database.Domain.Categorization;
using WS.Logic.Core.Results;

namespace WS.Logic.Products.Interface
{
    /// <summary>
    /// Describes the logic layer for working with manufacturers
    /// </summary>
    public interface IManufacturerLogic
    {
        /// <summary>
        /// Return a list of all manufacturers in the system
        /// </summary>
        /// <returns></returns>
        ActionResult<IEnumerable<Manufacturer>> Get();

        /// <summary>
        /// Save the given new manufacturer in persistance. The returned manufacturer contains 
        /// the id generated for the newly created manufacturer back to the calling code.
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        ActionResult<Manufacturer> Create(Manufacturer manufacturer);

        /// <summary>
        /// Update the manufacturer specified by the domain object.
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        ActionResult<Manufacturer> Update(Manufacturer manufacturer);

        /// <summary>
        /// Delete the manufacturer based on the given manfuacturer id
        /// </summary>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        ActionResult<Manufacturer> Delete(int manufacturerId);
    }
}
