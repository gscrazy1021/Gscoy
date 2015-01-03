using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Gscoy.Common
{
    public class StaticSouceHelper
    {
        /// <summary>
        /// 静态文件集合
        /// </summary>
        DataSet ds = new DataSet();
        /// <summary>
        /// 是否线上环境
        /// </summary>
        public bool IsOnline
        {
            get
            {
                return ConfigHelper.GetConfig("IsOnline", "1") == "1";
            }
        }
        /// <summary>
        /// 静态文件xml
        /// </summary>
        public string XmlPath
        {
            get
            {
                return ConfigHelper.GetConfig("XmlPath");
            }
        }

        public StaticSouceHelper()
        {
            if (!string.IsNullOrEmpty(XmlPath))
            {
                ds.ReadXml(XmlPath);
            }
        }

        public string GetSource(string key)
        {
            //ds.Tables[0].Select
            return string.Empty;
        }
    }
}
