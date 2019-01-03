namespace WS.Database.Access.Results
{
    public class SaveChangesResult
    {
        #region Props

        public bool IsSuccess { get; set; }

        #endregion

        #region Factory Methods

        public static SaveChangesResult Failed()
        {
            return new SaveChangesResult()
            {
                IsSuccess = false
            };
        }

        public static SaveChangesResult Success()
        {
            return new SaveChangesResult()
            {
                IsSuccess = true
            };
        }

        #endregion
    }
}
