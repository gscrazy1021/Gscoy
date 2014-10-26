﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

namespace Gscoy.Common.Wechat
{
    #region FileIo
    public class FileIO
    {
        
        private FileStream fsw;
        private StreamWriter sw;
        private string Charset = "UTF-8";

        public FileIO()
        {
        }

        public FileIO(string charset)
        {
            Charset = charset;
        }

        private void CreateDir(string filePath)
        {
            string dirPath = System.IO.Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        public void OpenWriteFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    CreateDir(filePath);
                    File.Create(filePath).Close();
                    fsw = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    sw = new StreamWriter(fsw, Encoding.GetEncoding(Charset));
                }
                else
                {
                    fsw = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    sw = new StreamWriter(fsw, Encoding.GetEncoding(Charset));
                }
            }
            catch
            {
            }
        }

        public void CloseWriteFile()
        {
            if (fsw != null)
            {
                fsw.Close();
            }
        }

        public void WriteLine(string s)
        {
            if (sw != null)
            {
                sw.WriteLine(s);
                sw.Flush();
            }
        }

        private FileStream fsr;
        private StreamReader sr;

        public void OpenReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateDir(filePath);
                File.Create(filePath).Close();
            }
            fsr = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read,
            FileShare.ReadWrite);
            sr = new StreamReader(fsr, Encoding.GetEncoding(Charset));
        }

        public void CloseReadFile()
        {
            if (fsr != null)
                fsr.Close();
        }

        public string ReadLine()
        {
            if (sr.EndOfStream)
                return null;
            return sr.ReadLine();
        }

        public string ReadToEnd()
        {
            if (sr.EndOfStream) { return null; }
            return sr.ReadToEnd();
        }

        public bool IsEof()
        {
            return sr.EndOfStream;
        }
    }//ClassEnd
    #endregion
}