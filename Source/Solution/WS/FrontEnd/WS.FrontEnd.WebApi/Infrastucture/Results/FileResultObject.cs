using System;

namespace WS.FrontEnd.WebApi.Infrastucture.Results
{
    public class FileResultObject
    {
        public DateTime DateUploaded { get; set; }
        
        public string FileLocation { get; set; }

        public string VirtualLocation { get; set; }

        public object ServerLocationFull { get; set; }

        public string FileOriginalName { get; set; }
    }
}