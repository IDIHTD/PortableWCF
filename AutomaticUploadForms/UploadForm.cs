﻿using MemexUpdateCommon;
using MemexUpdateCommon.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AutomaticUploadForms
{ 
    public partial class UploadForm : Form
    {    
        public UploadForm()
        {
            InitializeComponent();
        }  
       
        /// <summary>
        /// 选择上传文件路径 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectePath_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                txtBoxLoadPath.Text = folderBrowserDialog1.SelectedPath;
        }

        /// <summary>
        /// 确定上传文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var uploadFiles = MemexUpateHelper.GetFiles(txtBoxLoadPath.Text);
            if (uploadFiles != null && uploadFiles.MDir != null)
            {
                MemexUpateHelper.DeleteFile(textBox3.Text);
                    StartUploadFile(uploadFiles.MDir);
            }
            var appParams = new ApplicationInfo { AppName = textBox3.Text, AppVersion= txtBoxProjectVersion.Text, AppPath=txtBoxLoadPath.Text};
            MemexUpateHelper.SetApplicationInfo(appParams);
            GetFileList();
            //更新项目文件信息到XML中
            MemexUpateHelper.UpdateAppInfo(textBox3.Text);
            MessageBox.Show("上传完成！");
        }

        #region upload
        //开始上传
        private  void StartUploadFile(MDirs mdirs)
        {
            if (mdirs != null && mdirs.Files != null && mdirs.Files.Any())
                mdirs.Files.ToList().ForEach(o =>
                {
                    var isTrue = false;
                    if (!string.IsNullOrEmpty(o.ParentName))
                        isTrue = MemexUpateHelper.DirIsExistOrCreate(o.ParentName, textBox3.Text);
                    if (isTrue)
                    {
                        Stream sm = new FileStream(o.FullName, FileMode.Open, FileAccess.Read);
                        string message = "";
                        MemexUpateHelper.UpLoadFile(o.FullName, textBox3.Text + "\\", sm.Length, sm, out message);
                    }
                });
            if (mdirs != null && mdirs.Dirs != null && mdirs.Dirs.Any())
                mdirs.Dirs.ToList().ForEach(o =>
                {
                    StartUploadFile(o);
                });
        }
        #endregion

        //获取服务器端文件列表
        private void GetFileList()
        {
            var appList = MemexUpateHelper.GetAppList();
            if (appList != null && appList.Any())
                dataGridView1.DataSource = appList;
        }

        private void btnGetFilesList_Click(object sender, EventArgs e)
        {
            GetFileList();
        }

        /// <summary>
        /// 选择文件对比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var clientApp = MemexUpateHelper.GetFiles(textBox2.Text);
            var serverApp = MemexUpateHelper.GetServerFiles(textBox1.Text);
         
            if (serverApp != null&&clientApp!=null)
            {
                var listDifferent = MemexUpateHelper.GetDifferentFiles(serverApp.MDir, clientApp.MDir);
                if (listDifferent != null && listDifferent.ToList().Any())
                    StartDownFiles(listDifferent);
                MessageBox.Show("完成！");
            }
           else
                MessageBox.Show("服务器端不存在要更新的文件！");
            
        }

        #region download
        /// <summary>
        /// 调用WCF下载文件
        /// </summary>
        /// <param name="fileName"></param>
        private  void DownFile(DifferentFile file)
        {
            Stream sm = new MemoryStream();
            var name = file.FullName.Substring(3);
            var msg = string.Empty;
            var size = 0l;
            var issuccess = MemexUpateHelper.DownLoadFile(name, textBox1.Text, out msg, out size, out sm);
            if (issuccess.IsSuccess)
            {
                string path = textBox2.Text;
                if (Directory.Exists(path))
                    Directory.CreateDirectory(path);
                byte[] buffer = new byte[size];
                FileStream fs = new FileStream(file.ClientFullPath+"\\" + file.FilName, FileMode.Create, FileAccess.Write);
                int count = 0;
                while ((count = sm.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, count);
                }
                fs.Flush();
                fs.Close();
            }
        }

        #region 客户端下载文件方法

        public  void StartDownFiles(List<DifferentFile> listDifferent)
        {
            if (listDifferent != null && listDifferent.Any())
            {
                //客户端需要下载的文件
                var desList = new List<DifDescription> { DifDescription.DirNotExistInClient, DifDescription.FileNotExistInClient, DifDescription.FileSizeInconsistency, DifDescription.FileVersionInconsistency, DifDescription.ForceUpdate,DifDescription.FileNotExistInServer,DifDescription.DirNotExistInServer};
                //需要下载的文件夹
                var needDownList = listDifferent.Where(o => desList.Contains(o.DiffentValue)).ToList();
                if (needDownList != null && needDownList.Any())
                {
                    var dirs = needDownList.Where(o => o.Type == FileType.Dir).ToList();
                    if (dirs != null && dirs.Any())
                        dirs.ForEach(o =>
                        {
                            //删除服务端不存在的文件夹
                            //if (o.DiffentValue == DifDescription.DirNotExistInServer)
                            //    MemexUpateHelper.DeleteDir(o.ClientFullPath);
                            if (o.DiffentValue == DifDescription.DirNotExistInClient)
                            {
                                o.FullName = o.FullName.Substring(2);
                                if (!Directory.Exists(o.ClientFullPath + "\\" + o.FilName))
                                    Directory.CreateDirectory(o.ClientFullPath + "\\" + o.FilName);
                            }
                        });
                }
                //需要下载的文件
                var needDownFiles = listDifferent.Where(o => o.Type == FileType.File).ToList();
                if (needDownFiles != null && needDownFiles.Any())
                    needDownFiles.ForEach(o =>
                    {
                        //删除服务端不存在的文件
                        //if (o.DiffentValue == DifDescription.FileNotExistInServer)
                        //    MemexUpateHelper.DeleteFiles(o.ClientFullPath);
                        if (o.DiffentValue == DifDescription.FileNotExistInClient)
                        {
                            //如果父目录不存在则创建
                            if (!Directory.Exists(o.ClientFullPath))
                                Directory.CreateDirectory(o.ClientFullPath);
                            //下载文件
                            DownFile(o);
                        }
                    });
            }
        }

        #endregion

        #endregion
        /// <summary>
        /// 选择客户端文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog2.ShowDialog();
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog2.SelectedPath))
                textBox2.Text = folderBrowserDialog2.SelectedPath;
        }
    }

    [Serializable]
    public class BandProjectInfo
    {
        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }
    }
}
