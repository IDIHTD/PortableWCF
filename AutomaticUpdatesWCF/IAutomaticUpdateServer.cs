using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AutomaticUpdatesWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IAutomaticUpdateServer
    {
        /// <summary>
        /// 获取应用程序版本
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        [OperationContract]
        string GetVersionByApplicationName(string applicationName);
        /// <summary>
        /// 获取应用程序文件列表
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        [OperationContract]
        ApplicationEntity GetServerPublishFiles(string applicationName);
        /// <summary>
        /// 设置应用程序基本信息
        /// </summary>
        /// <param name="appInf"></param>
        [OperationContract]
        bool SetApplicationInfo(ApplicationInfo appInf);

        [OperationContract]
        DlFileResult DownLoadFile(DlFile file);

        [OperationContract]
        UpFileResult UpLoadFile(UpFile file);

        [OperationContract]
        bool DirIsExistOrCreate(string dirName, string projectName);

        [OperationContract]
        List<ApplicationEntity> GetAppList();

        [OperationContract]
        bool DeleteFile(string projectName);

        /// <summary>
        /// 传输数据接口暂时没有方法体
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<DifferentFile> GetDifferentList();

        // TODO: 在此添加您的服务操作
    }
}
