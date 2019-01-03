namespace WS.Database.Domain.Base
{
    public interface IHaveEntityState
    {
        WsEntityState State { get; set; }
    }
}
