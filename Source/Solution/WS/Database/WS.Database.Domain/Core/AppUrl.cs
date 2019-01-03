using WS.Database.Domain.Base;

namespace WS.Database.Domain.Core
{
    public class AppUrl:Entity
    {
        #region Props

        public string Name { get; set; }

        public string Value { get; set; }

        #endregion
    }
}
