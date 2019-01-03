namespace WS.FrontEnd.WebApi.Infrastucture.Config
{
    public static class CorePathsConfig
    {
        public static string FilesCorePath = "~/Files";

        public static string GetBaseEntityPath(string type)
        {
            return $"{FilesCorePath}/{type}Images";
        }

        public static string GetEntityPathForEntityId(string type, int entityId)
        {
            if (entityId == 0)
            {
                return $"{GetBaseEntityPath(type)}/Temporary";
            }

            return $"{GetBaseEntityPath(type)}/{entityId}/";
        }

        /// <summary>
        /// Return the path for temporary uploaded images before they get moved
        /// to actual category upload paths
        /// </summary>
        /// <returns></returns>
        public static string GetTemporaryEntitiesPath()
        {
            return $"{FilesCorePath}/Tmp";
        }
    }
}