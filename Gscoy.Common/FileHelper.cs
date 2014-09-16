﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gscoy.Common
{
    /// <summary>
    ///     文件辅助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        ///     编码方式
        /// </summary>
        private static readonly Encoding Encoding = Encoding.GetEncoding(ConfigHelper.GetConfig("FileEncoding", "UTF-8"));

        /// <summary>
        ///     递归取得文件夹下文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="list"></param>
        public static void GetFiles(string dir, List<string> list)
        {
            GetFiles(dir, list, new List<string>());
        }

        /// <summary>
        ///     递归取得文件夹下文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="list"></param>
        /// <param name="fileExtsions"></param>
        public static void GetFiles(string dir, List<string> list, List<string> fileExtsions)
        {
            //添加文件 
            string[] files = Directory.GetFiles(dir);
            if (fileExtsions.Count > 0)
            {
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);
                    if (extension != null && fileExtsions.Contains(extension))
                    {
                        list.Add(file);
                    }
                }
            }
            else
            {
                list.AddRange(files);
            }
            //如果是目录，则递归
            DirectoryInfo[] directories = new DirectoryInfo(dir).GetDirectories();
            foreach (DirectoryInfo item in directories)
            {
                GetFiles(item.FullName, list, fileExtsions);
            }
        }

        /// <summary>
        ///     写入文件
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="content">文件内容</param>
        public static void WriteFile(string filePath, string content, bool isAppend = false)
        {
            try
            {
                CreateFile(filePath);
                var fs = new FileStream(filePath, isAppend ? FileMode.Append : FileMode.Create);
                Encoding encode = Encoding;
                //获得字节数组
                byte[] data = encode.GetBytes(content);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        ///     读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            return ReadFile(filePath, Encoding);
        }

        /// <summary>
        ///     读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath, Encoding encoding)
        {
            if (IsExist(filePath))
            {
                using (var sr = new StreamReader(filePath, encoding))
                {
                    return sr.ReadToEnd();
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     读取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> ReadFileLines(string filePath)
        {
            var str = new List<string>();
            if (IsExist(filePath))
            {
                using (var sr = new StreamReader(filePath, Encoding))
                {
                    String input;
                    while ((input = sr.ReadLine()) != null)
                    {
                        str.Add(input);
                    }
                }
            }
            return str;
        }

        /// <summary>
        ///     复制文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="sourcePath">待复制的文件夹路径</param>
        /// <param name="destinationPath">目标路径</param>
        public static void CopyDirectory(String sourcePath, String destinationPath)
        {
            var info = new DirectoryInfo(sourcePath);
            Directory.CreateDirectory(destinationPath);
            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                String destName = Path.Combine(destinationPath, fsi.Name);

                if (fsi is FileInfo) //如果是文件，复制文件
                    File.Copy(fsi.FullName, destName);
                else //如果是文件夹，新建文件夹，递归
                {
                    Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }

        /// <summary>
        ///     删除文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void DeleteFolder(string directoryPath)
        {
            foreach (string d in Directory.GetFileSystemEntries(directoryPath))
            {
                if (File.Exists(d))
                {
                    var fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d); //删除文件   
                }
                else
                    DeleteFolder(d); //删除文件夹
            }
            Directory.Delete(directoryPath); //删除空文件夹
        }

        /// <summary>
        ///     清空文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void ClearFolder(string directoryPath)
        {
            foreach (string d in Directory.GetFileSystemEntries(directoryPath))
            {
                if (File.Exists(d))
                {
                    var fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly", StringComparison.Ordinal) != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d); //删除文件   
                }
                else
                    DeleteFolder(d); //删除文件夹
            }
        }

        /// <summary>
        ///     取得文件大小，按适当单位转换
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetFileSize(string filepath)
        {
            string result = "0KB";
            if (File.Exists(filepath))
            {
                var size = new FileInfo(filepath).Length;
                int filelength = size.ToString().Length;
                if (filelength < 4)
                    result = size + "byte";
                else if (filelength < 7)
                    result = Math.Round(Convert.ToDouble(size / 1024d), 2) + "KB";
                else if (filelength < 10)
                    result = Math.Round(Convert.ToDouble(size / 1024d / 1024), 2) + "MB";
                else if (filelength < 13)
                    result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024), 2) + "GB";
                else
                    result = Math.Round(Convert.ToDouble(size / 1024d / 1024 / 1024 / 1024), 2) + "TB";
                return result;
            }
            return result;
        }
        /// <summary>
        /// 判断文件路径是否存在
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool IsExist(string filepath)
        {
            bool isExist = File.Exists(filepath);
            return isExist;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filepath"></param>
        public static void CreateFile(string filepath)
        {
            if (!IsExist(filepath))
            {
                string dir = Path.GetDirectoryName(filepath);
                DirectoryInfo di = Directory.CreateDirectory(dir);
                FileStream fs = File.Create(filepath);
                fs.Flush();
                fs.Dispose();
            }
        }
    }
}