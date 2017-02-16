using MemexUpdateCommon.ServiceReference1;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;

namespace MemexUpdateCommon
{
   public class MemexSingleInstance
    {
        private static string _projectName;
        private static IAutomaticUpdateServer channel;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectName"></param>
        public MemexSingleInstance (string projectName)
        {
            _projectName = projectName;
            EndpointAddress address = new EndpointAddress("http://localhost:60124/AutomaticUpdateImplement.svc/IAutomaticUpdateServer");
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferSize = 2147483647;
            binding.Name = "BasicHttpBinding_IAutomaticUpdateServer";
            ChannelFactory<IAutomaticUpdateServer> factory = new ChannelFactory<IAutomaticUpdateServer>(binding, address);
            channel = factory.CreateChannel();
        }

        /// <summary>
        /// 删除服务端指定文件
        /// </summary>
        /// <param name="fileName"></param>
        public  void DeleteFile(string fileName)
        {
            channel.DeleteFile(fileName);
        }

        /// <summary>
        /// 设置服务端项目属性
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        public  bool SetApplicationInfo(ApplicationInfo appInfo)
        {
            return channel.SetApplicationInfo(appInfo);
        }

        /// <summary>
        /// 删除或创建指定文件夹
        /// </summary>
        /// <param name="dirName">文件夹路径</param>
        /// <param name="projectName">项目名称</param>
        /// <returns></returns>
        public  bool DirIsExistOrCreate(string dirName)
        {
            return channel.DirIsExistOrCreate(dirName,_projectName);
        }

        /// <summary>
        /// 获取项目信息列表
        /// </summary>
        /// <returns></returns>
        private  List<ApplicationEntity> GetAppList()
        {
            return channel.GetAppList();
        }

        /// <summary>
        /// 获取服务端文件列表实体
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public  ApplicationEntity GetServerFiles()
        {
            return channel.GetServerPublishFiles(_projectName);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="msg">返回消息</param>
        /// <param name="size">大小</param>
        /// <param name="sm">文件流</param>
        /// <returns></returns>
        public  DlFileResult DownLoadFile(string fileName, out string msg, out long size, out Stream sm)
        {
            DlFile dfile = new DlFile();
            dfile.FileName = fileName;
            dfile.ProjectName = _projectName;
            msg = "";
            var result = channel.DownLoadFile(dfile);
            size = result.Size;
            sm = result.FileStream;
            return result;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="length">长度</param>
        /// <param name="sm">文件流</param>
        /// <param name="message">消息</param>
        /// <returns></returns>     
        public  UpFileResult UpLoadFile(string fileName, long length, Stream sm, out string message)
        {
            UpFile upFile = new UpFile { FileName = fileName, ProjectName = _projectName, Size = length, FileStream = sm };
            message = string.Empty;
            return channel.UpLoadFile(upFile);
        }

        /// <summary>
        /// 获取指定路径下的所有文件集合
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public  ApplicationEntity GetFiles(string path)
        {
            return CommonAction.GetFiles(path);
        }

        /// <summary>
        /// 对比服务端和客户端文件
        /// </summary>
        /// <param name="server"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public  List<DifferentFile> GetDifferentFiles(MDirs server, MDirs client)
        {
            return CommonAction.GetDifferentFiles(server, client);
        }

        /// <summary>
        /// 删除客户端指定目录文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public  bool DeleteDir(string path)
        {
            return CommonAction.DeleteDir(path);
        }

        /// <summary>
        /// 删除客户端指定目录的文件
        /// </summary>
        /// <param name="path"></param>
        public  void DeleteFiles(string path)
        {
            CommonAction.DeleteFiles(path);
        }

        /// <summary>
        /// 是否需要更新
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public  bool IsNeedToUpdate(string version)
        {
            var appList = channel.GetAppList();
            if (appList != null && appList.Any())
            {
                var firstOrDefault = appList.FirstOrDefault(o => o.AppName == _projectName && o.AppVersion != version);
                if (firstOrDefault != null)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 更新项目文件信息到XML中
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public  bool UpdateAppInfo()
        {
            return channel.UpdateAppInfo(_projectName);
        }
    }
}
