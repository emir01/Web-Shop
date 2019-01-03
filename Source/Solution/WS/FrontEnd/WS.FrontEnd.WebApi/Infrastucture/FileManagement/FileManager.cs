using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WS.FrontEnd.WebApi.Infrastucture.Config;
using WS.FrontEnd.WebApi.Infrastucture.Extensions;
using WS.FrontEnd.WebApi.Infrastucture.Results;
using System.Collections.Specialized;

namespace WS.FrontEnd.WebApi.Infrastucture.FileManagement
{
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Process the image from the given request and store them as entity images based on the File Upload Type.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FileResultObject> Process(HttpRequestMessage request)
        {
            var provider = GetMultipartProvider();

            var result = await request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            var storePath = GetStorePath(result.FormData);

            var composedFileName = Guid.NewGuid() + "_" + originalFileName;

            var newPath = storePath + composedFileName;

            uploadedFileInfo.CopyTo(newPath);

            var serverLocation = newPath.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");

            CleaupTemporary(uploadedFileInfo);

            return new FileResultObject
            {
                DateUploaded = DateTime.Now,
                FileOriginalName = originalFileName,
                FileLocation = newPath,
                VirtualLocation = serverLocation.Substring(1, serverLocation.Length - 1)
            };
        }

        private object GetStorePath(NameValueCollection formData)
        {
            int entityId = formData.EntityId();
            var type = formData.FileType();
            
            var entityPath = HttpContext.Current.Server.MapPath(CorePathsConfig.GetEntityPathForEntityId(type, entityId));
            
            Directory.CreateDirectory(entityPath);

            return entityPath;
        }

        public void ProcessAndDeleteFilesInDirectoriesUnderRoot(string root, IList<string> activeFiles)
        {
            var server = HttpContext.Current.Server;

            var directoryPath = Path.Combine(server.MapPath(CorePathsConfig.FilesCorePath), root);

            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            var allDirectoriesUnderSubRoot = Directory.GetDirectories(directoryPath);

            foreach (var subDirectoryPath in allDirectoriesUnderSubRoot)
            {
                ProcessAndDeleteFilesInDirectory(subDirectoryPath, activeFiles.ToList());
            }
        }

        public void ProcessAndDeleteFilesInDirectory(string directory, List<string> activeFiles)
        {
            if (!Directory.Exists(directory))
            {
                directory = Path.Combine(HttpContext.Current.Server.MapPath(CorePathsConfig.FilesCorePath), directory);

                if (!Directory.Exists(directory))
                {
                    return;
                }
            }
            var serverMappedFiles = activeFiles.GetServerMappedFiles();

            if (Directory.Exists(directory))
            {
                var filesInDirectory = Directory.GetFiles(directory);

                foreach (var file in filesInDirectory)
                {
                    if (File.Exists(file))
                    {
                        if (!FileIsInActivePaths(serverMappedFiles, file))
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
        }

        private bool FileIsInActivePaths(IList<string> activeFilePaths, string file)
        {
            return activeFilePaths.Any(f => f.Contains(file));
        }

        /// <summary>
        /// Delete the file that as uploaded from the Temporary location
        /// </summary>
        /// <param name="uploadedFileInfo"></param>
        private void CleaupTemporary(FileInfo uploadedFileInfo)
        {
            uploadedFileInfo.Delete();
        }

        #region Provided Utilities

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var temporaryUploadDirectory = HttpContext.Current.Server.MapPath(CorePathsConfig.GetTemporaryEntitiesPath());
            
            Directory.CreateDirectory(temporaryUploadDirectory);

            return new MultipartFormDataStreamProvider(temporaryUploadDirectory);
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        #endregion
    }
}