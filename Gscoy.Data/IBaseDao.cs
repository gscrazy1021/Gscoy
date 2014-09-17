using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gscoy.DataModel;

namespace Gscoy.Data
{
    public interface IBaseDao<T> where T : BaseEntity
    {
        /// <summary>
        /// 根据页号和大小获取指定页的数据列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="allCount"></param>
        /// <returns></returns>
        List<T> GetList(int pageSize, int pageIndex, out int allCount);
        /// <summary>
        /// 获得单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetEntity(int id);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Insert(T entity);
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Update(T entity);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Delete(T entity);
    }
}
