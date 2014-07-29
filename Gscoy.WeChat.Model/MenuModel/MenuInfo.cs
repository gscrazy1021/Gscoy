using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.WeChat.Model.MenuModel
{
    public class MenuInfo
    {
        public MenuBase button { get; set; }
    }

    public class MenuInfoList
    {
        public MenuInfo menu { get; set; }
    }
}
