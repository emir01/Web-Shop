using WS.Logic.Core.QueryContracts;

namespace WS.Logic.Core.Validators
{
    public class QueryObjectValidator : IQueryObjectValidator
    {
        public BaseQueryContarct ValidateBaseQueryObject(BaseQueryContarct contract)
        {
            if (contract == null)
            {
                return new BaseQueryContarct();
            }
            else
            {
                return contract;
            }
        }
    }
}