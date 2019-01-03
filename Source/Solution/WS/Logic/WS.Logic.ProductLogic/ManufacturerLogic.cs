using System;
using System.Collections.Generic;
using WS.Database.Access.Interface;
using WS.Database.Domain.Categorization;
using WS.Logic.Core.Results;
using WS.Logic.Products.Interface;

namespace WS.Logic.Products
{
    public class ManufacturerLogic : IManufacturerLogic
    {
        private readonly IWsUnitOfWork _unitOfWork;

        public ManufacturerLogic(IWsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Return a list of all manufacturers in the system
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<Manufacturer>> Get()
        {
            try
            {
                var manufacturers = _unitOfWork.ManufacturerRepository.Query();

                return ActionResult<IEnumerable<Manufacturer>>.GetSuccess(manufacturers);
            }
            catch (Exception ex)
            {
                return ActionResult<IEnumerable<Manufacturer>>.GetFailed($"Exception when retrieving manufacturers. Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Save the given new manufacturer in persistance. The returned manufacturer contains 
        /// the id generated for the newly created manufacturer back to the calling code.
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        public ActionResult<Manufacturer> Create(Manufacturer manufacturer)
        {
            try
            {
                var createdManufacturer = _unitOfWork.ManufacturerRepository.Create(manufacturer);
                _unitOfWork.Commit();

                return ActionResult<Manufacturer>.GetSuccess(createdManufacturer, "Successfully Created a new manufacturer");
            }
            catch (Exception ex)
            {
                return ActionResult<Manufacturer>.GetFailed($"Exception when creating manufacturer: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the manufacturer specified by the domain object.
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        public ActionResult<Manufacturer> Update(Manufacturer manufacturer)
        {
            try
            {
                var updatedManufacturer = _unitOfWork.ManufacturerRepository.Update(manufacturer);
                _unitOfWork.Commit();

                return ActionResult<Manufacturer>.GetSuccess(updatedManufacturer);
            }
            catch (Exception ex)
            {
                return ActionResult<Manufacturer>.GetFailed($"Exception when updating manufacturer: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete the manufacturer based on the given manfuacturer id
        /// </summary>
        /// <param name="manufacturerId"></param>
        /// <returns></returns>
        public ActionResult<Manufacturer> Delete(int manufacturerId)
        {
            try
            {
                var manufacturer = _unitOfWork.ManufacturerRepository.GetEntityById(manufacturerId);

                if (manufacturer == null)
                {
                    return ActionResult<Manufacturer>.GetFailed($"There was no manufacturer with id {manufacturerId}");
                }

                _unitOfWork.ManufacturerRepository.Delete(manufacturer);
                _unitOfWork.Commit();

                return ActionResult<Manufacturer>.GetSuccess(manufacturer, $"Manufacturer with id {manufacturerId} deleted");
            }
            catch (Exception ex)
            {
                return ActionResult<Manufacturer>.GetFailed($"Exception when deleting manufacturer: {ex.Message}");
            }
        }
    }
}