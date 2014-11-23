using Gscoy.Common;
using Gscoy.DataModel.Sina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Biz.Sina
{
    public class DreamBiz : BaseBiz
    {
        /// <summary>
        /// 返回解梦列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public List<DreamEntity> GetDreamList(int start, int length)
        {
            //初始值为0的取不到列表
            if (start <= 0 || length <= 0) return null;
            string url = string.Format("http://gscoy.sinaapp.com/dream.php?action=getlist&start={0}&length={1}", start, length);
            DreamListEntity list = GetSinaEntity<DreamListEntity>(url, true);
            if (list != null)
                return list.result.ToList();
            return null;
        }
        /// <summary>
        /// 返回单个解梦实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DreamEntity GetEntityByID(int dreamId)
        {
            if (dreamId <= 0) return null;
            string url = string.Format("http://gscoy.sinaapp.com/dream.php?action=getbyid&id={0}", dreamId);
            return GetSinaEntity<DreamEntity>(url);
        }


        public DreamEntity GetEntityByTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) return null;
            string url = string.Format("http://gscoy.sinaapp.com/dream.php?action=getbytitle&title={0}", title.UrlEncode(Encoding.UTF8));
            return GetSinaEntity<DreamEntity>(url);
        }
    }
}
