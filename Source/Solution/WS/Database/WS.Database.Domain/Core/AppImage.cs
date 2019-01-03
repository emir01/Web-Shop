using System.ComponentModel.DataAnnotations.Schema;
using WS.Database.Domain.Base;

namespace WS.Database.Domain.Core
{
    public class AppImage : Entity, IHaveEntityState
    {
        public AppImage()
        {
            State = WsEntityState.NoChanges;
        }
        
        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }

        [NotMapped]
        public WsEntityState State { get; set; }
    }
}
