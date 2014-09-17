using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.DataModel.Blog;

namespace Gscoy.Data.Blog
{
    public class ArticleDao:IBaseDao<ArticleEntity>
    {
        #region IBaseDao<ArticleEntity> 成员

        public List<ArticleEntity> GetList(int pageSize, int pageIndex, out int allCount)
        {
            throw new NotImplementedException();
        }

        public ArticleEntity GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ArticleEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(ArticleEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ArticleEntity entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
