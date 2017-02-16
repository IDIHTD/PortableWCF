using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace AutomaticUpdatesWCF
{
    public static class FileProcessingHelper
    {
        public static string GetUpLoadFilePath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"UpLoadFile\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// 删除指定目录的文件夹及文件夹下的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteDir(string path)
        {
            if (Directory.Exists(path) == false)
            {
                return false;
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            try
            {
                foreach (var item in files)
                {
                    File.Delete(item.FullName);
                }
                if (dir.GetDirectories().Length != 0)
                {
                    foreach (var item in dir.GetDirectories())
                    {
                        if (!item.ToString().Contains("$") && (!item.ToString().Contains("Boot")))
                        {
                            DeleteDir(dir.ToString() + "\\" + item.ToString());
                        }
                    }
                }
                Directory.Delete(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取文件路径下的所有文件信息 实体
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static ApplicationEntity GetFiles(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                return null;
            ApplicationEntity appEntity = new ApplicationEntity();
            appEntity.MDir = new MDirs();
            appEntity.MDir.Name = path.Substring(path.LastIndexOf("\\") + 1);
            appEntity.MDir.FullName = path;
            GetFilesTree(path, appEntity.MDir);
            return appEntity;
        }

        private static void GetFilesTree(string targetDir, MDirs currentObject)
        {
            if (currentObject == null)
                currentObject = new MDirs();
            if (currentObject.Files == null)
                currentObject.Files = new List<MFiles>();
            if (currentObject.Dirs == null)
                currentObject.Dirs = new List<MDirs>();
            var length = targetDir.LastIndexOf("\\");
            var rootName = targetDir.Substring(length + 1);
            currentObject.Name = rootName;
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                var fileInfo = new FileInfo(fileName);
                FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(fileName);
                var version = string.Empty;
                if (myFileVersion != null && !string.IsNullOrEmpty(myFileVersion.FileVersion))
                    version = myFileVersion.FileVersion;
                currentObject.Files.Add(new MFiles
                {
                    Name = fileName.Substring(fileName.LastIndexOf("\\") + 1),
                    ParentName = targetDir,
                    FullName = fileName,
                    ExtendName = fileName.Substring(fileName.LastIndexOf(".") + 1),
                    Size = fileInfo.Length.ToString(),
                    Version = version
                });
            }
            foreach (string directory in Directory.GetDirectories(targetDir))
            {
                var currentEntity = new MDirs
                {
                    Name = directory.Substring(directory.LastIndexOf("\\") + 1),
                    ParentName = targetDir,
                    FullName = directory
                };
                currentObject.Dirs.Add(currentEntity);
                GetFilesTree(directory, currentEntity);
            }
        }

    }

}