using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WS.FrontEnd.WebApi.Infrastucture.Results;

namespace WS.FrontEnd.WebApi.Infrastucture.FileManagement
{
    public interface IFileManager
    {
        Task<FileResultObject> Process(HttpRequestMessage request);

        void ProcessAndDeleteFilesInDirectoriesUnderRoot(string root, IList<string> activeFiles);

        void ProcessAndDeleteFilesInDirectory(string directory, List<string> activeFiles);
    }
}