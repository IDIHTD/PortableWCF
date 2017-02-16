using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace AutomaticUpdatesWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AutomaticUpdateImplement : IAutomaticUpdateServer
    {
        private static string path;

        public AutomaticUpdateImplement()
        {
            path = FileProcessingHelper.GetUpLoadFilePath();
        }

       

        public string GetVersionByApplicationName(string applicationName)
        {
            if (AppList == null || !AppList.Any())
                return string.Empty;
            if (AppList.Exists(o => o.AppName == applicationName) && !string.IsNullOrEmpty(AppList.FirstOrDefault(o => o.AppName == applicationName).AppVersion))
                return AppList.FirstOrDefault(o => o.AppName == applicationName).AppVersion;
            return string.Empty;
        }

        /// <summary>
        /// 获取应用更新列表
        /// </summary>
        /// <param name="applicationName">应用名称</param>
        /// <returns></returns>
        public ApplicationEntity GetServerPublishFiles(string applicationName)
        {
            if (AppList != null && AppList.Any() && AppList.Any(o => o.AppName == applicationName))
            {
                return AppList.FirstOrDefault(o => o.AppName == applicationName);
            }
            else
                return new ApplicationEntity();
        }
        public static List<ApplicationEntity> AppList
        {
            get; set;
        }

        public bool SetApplicationInfo(ApplicationInfo appInfo)
        {
            var currentAppEntity = FileProcessingHelper.GetFiles(appInfo.AppPath);
            if (currentAppEntity != null && currentAppEntity.MDir != null)
            {
                currentAppEntity.AppName = appInfo.AppName;
                currentAppEntity.AppVersion = appInfo.AppVersion;
                if (AppList == null)
                    AppList = new List<ApplicationEntity>();
                AppList.Add(currentAppEntity);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="dlfile"></param>
        /// <returns></returns>
        public DlFileResult DownLoadFile(DlFile dlfile)
        {
            string path = FileProcessingHelper.GetUpLoadFilePath() + dlfile.ProjectName + "\\" + dlfile.FileName;
            if (!File.Exists(path))
            {
                var result = new DlFileResult();
                result.Size = 0;
                result.IsSuccess = false;
                result.Message = "";
                result.FileStream = new MemoryStream();
                return result;
            }
            DlFileResult file = new DlFileResult();
            file.FileStream = new MemoryStream();
            Stream ms = new MemoryStream();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.CopyTo(ms);
            ms.Position = 0;
            file.Size = ms.Length;
            file.FileStream = ms;
            file.IsSuccess = true;
            fs.Flush();
            fs.Close();
            return file;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public UpFileResult UpLoadFile(UpFile file)
        {
            file.FileName = file.FileName.Substring(2);
            byte[] buffer = new byte[file.Size];
            FileStream fs = new FileStream(path + file.ProjectName + file.FileName, FileMode.Create, FileAccess.Write);
            int count = 0;

            while ((count = file.FileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, count);
            }

            fs.Flush();
            fs.Close();
            return new UpFileResult(true, "");
        }

        /// <summary>
        /// 如果路径不存在则创建，存在返回true
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public bool DirIsExistOrCreate(string dirName, string projectName)
        {
            dirName = dirName.Substring(2);
            if (!Directory.Exists(path + projectName + "\\" + dirName))
            {
                try
                {
                    Directory.CreateDirectory(path + projectName + "\\" + dirName);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <returns></returns>
        public List<ApplicationEntity> GetAppList()
        {
            var appList = AppList;
            if (appList != null && appList.Any())
            {
                return appList;
            }
            return new List<ApplicationEntity>();
        }

        /// <summary>
        /// 删除指定目录下的所有文件和文件夹
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public bool DeleteFile(string projectName)
        {
            return FileProcessingHelper.DeleteDir(path + projectName + "\\");
        }

        public List<DifferentFile> GetDifferentList()
        {
            return new List<DifferentFile>();
        }
    }
}
