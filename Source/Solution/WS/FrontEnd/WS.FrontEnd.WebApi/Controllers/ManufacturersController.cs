using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WS.Contracts.Contracts.Dtos.Manufacturers;
using WS.Database.Domain.Categorization;
using WS.FrontEnd.WebApi.Infrastucture;
using WS.FrontEnd.WebApi.Infrastucture.Extensions;
using WS.Logic.Products.Interface;

namespace WS.FrontEnd.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [Authorize]
    public class ManufacturersController : ApiController
    {
        private IManufacturerLogic _manufacturerLogic;

        public ManufacturersController(IManufacturerLogic manufacturerLogic)
        {
            _manufacturerLogic = manufacturerLogic;
        }

        /// <summary>
        /// Returns all the manufacturers in the system
        /// </summary>
        /// <returns></returns>\
        [AllowAnonymous]
        public IEnumerable<ManufacturerDto> Get()
        {
            var queryResult = _manufacturerLogic.Get();

            if (queryResult.Success)
            {
                var manufacturerDtos = queryResult.Data.Select(AutoMapper.Mapper.Map<Manufacturer, ManufacturerDto>);

                return manufacturerDtos;
            }

            throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(queryResult));
        }

        /// <summary>
        /// Create a new manufacturer in the system
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ManufacturerDto Post(ManufacturerDto data)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                Manufacturer manufacturer = AutoMapper.Mapper.Map<ManufacturerDto, Manufacturer>(data);

                var result = _manufacturerLogic.Create(manufacturer);

                if (result.Success)
                {
                    var createdManufacturer = result.Data;
                    var createdDto = AutoMapper.Mapper.Map<Manufacturer, ManufacturerDto>(createdManufacturer);
                    return createdDto;
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(result));
            });
        }

        public ManufacturerDto Put(ManufacturerDto data)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                Manufacturer manufacturer = AutoMapper.Mapper.Map<ManufacturerDto, Manufacturer>(data);

                var result = _manufacturerLogic.Update(manufacturer);

                if (result.Success)
                {
                    var createdManufacturer = result.Data;
                    var createdDto = AutoMapper.Mapper.Map<Manufacturer, ManufacturerDto>(createdManufacturer);
                    return createdDto;
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(result));
            });
        }

        public ManufacturerDto Delete(int id)
        {
            return ActionVerbConfigService.WrapAction(() =>
            {
                var result = _manufacturerLogic.Delete(id);

                if (result.Success)
                {
                    var deletedManufacturer = result.Data;
                    var deletedManufacturerDto =
                        AutoMapper.Mapper.Map<Manufacturer, ManufacturerDto>(deletedManufacturer);
                    return deletedManufacturerDto;
                }

                throw new HttpResponseException(ResponseMessageBuilder.BuildMessageFromActionResult(result));
            });
        }
    }
}
