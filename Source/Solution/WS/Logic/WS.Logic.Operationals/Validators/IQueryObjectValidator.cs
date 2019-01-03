using WS.Logic.Core.QueryContracts;

namespace WS.Logic.Core.Validators
{
    public interface IQueryObjectValidator
    {
        /// <summary>
        /// Try and validate a query object.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        BaseQueryContarct ValidateBaseQueryObject(BaseQueryContarct contract);
    }
}
