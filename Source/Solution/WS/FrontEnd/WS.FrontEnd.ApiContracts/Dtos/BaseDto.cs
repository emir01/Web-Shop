namespace WS.Contracts.Contracts.Dtos
{
    /// <summary>
    /// Base Dto object for the api contracts that contains the id of the entitiy the dto is
    /// supposed to represent
    /// </summary>
    public class BaseDto
    {
        public int Id { get; set; }

        public int State { get; set; }
    }
}