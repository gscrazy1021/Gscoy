using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common.Enums
{
    /// <summary>
    /// 数据库操作角色
    /// </summary>
    public enum DBUserType
    {
        /// <summary>
        /// 只读账号(有select权限)
        /// </summary>
        User_R=1,
        /// <summary>
        /// 读写账号(有select,insert,update,delete权限)
        /// </summary>
        User_W=2
    }
}
