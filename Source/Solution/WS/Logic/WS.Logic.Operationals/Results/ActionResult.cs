namespace WS.Logic.Core.Results
{
    /// <summary>
    /// Define an action result from the logic layer that wraps the result 
    /// from  given operation.
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionResult<T>
    {
        public T Data { get; set; }

        /// <summary>
        /// Total set of data
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Indicate if the query was a success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Any message related to the query
        /// </summary>
        public string Message { get; set; }

        public static ActionResult<T> GetSuccess(T data, string message = "")
        {
            return new ActionResult<T>
            {
                Data = data,
                Message = message,
                Success = true
            };
        }

        public static ActionResult<T> GetFailed(string message = "")
        {
            return new ActionResult<T>
            {
                Data = default(T),
                Message = message,
                Success = false
            };
        }
    }
}
