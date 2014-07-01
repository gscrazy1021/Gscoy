using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.ResonseModel
{
    /// <summary>
    /// 接收事件基类
    /// </summary>
    public class BaseEventEntity : BaseMsgEntity
    {
        /// <summary>
        /// 接收用户发过来的事件
        /// </summary>
        public string Event { get; set; }
    }
}
