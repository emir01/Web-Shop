using System.Security.Cryptography.X509Certificates;

namespace WS.Database.Domain.Base
{
    /// <summary>
    /// The interface used to define entities that can be flagged
    /// as client or not client, meaning there is a difference
    /// between client data and SaS data
    /// </summary>
    public interface IClientEntity
    {
        bool IsClient { get; set; }
    }
}
