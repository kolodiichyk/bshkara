using System;
using System.IO;
using System.Web.Hosting;

namespace Bshkara.Web.Helpers
{
    public static class FileHelper
    {
        public static void SaveFile(byte[] content, string path)
        {
            var filePath = GetFileFullPath(path);
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            //Save file
            using (var str = File.Create(filePath))
            {
                str.Write(content, 0, content.Length);
            }
        }

        public static string GetFileFullPath(string path)
        {
            var relName = path.StartsWith("~") ? path : path.StartsWith("/") ? string.Concat("~", path) : path;

            var filePath = relName.StartsWith("~") ? HostingEnvironment.MapPath(relName) : relName;

            return filePath;
        }

        public static bool CreateFolderIfNeeded(string path)
        {
            var result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }
    }
}