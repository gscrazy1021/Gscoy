using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.Data.Blog;
using Gscoy.DataModel.Blog;

namespace Gscoy.Biz.Blog
{
    public class ActicleBiz : IBaseBiz<ArticleEntity>
    {
        ArticleDao dao = new ArticleDao();
        #region IBaseBiz<ArticleEntity> 成员

        public List<ArticleEntity> GetList(int pageSize, int pageIndex, out int allCount)
        {
            return dao.GetList(pageSize, pageIndex, out allCount);
        }

        public ArticleEntity GetEntity(int id)
        {
            return dao.GetEntity(id);
        }

        public bool Insert(ArticleEntity entity)
        {
            return dao.Insert(entity);
        }

        public bool Update(ArticleEntity entity)
        {
            return dao.Update(entity);
        }

        public bool Delete(ArticleEntity entity)
        {
            return dao.Delete(entity);
        }

        #endregion
    }
}
