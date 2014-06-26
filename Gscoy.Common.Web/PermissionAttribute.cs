using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Common.Web
{
    public class PermissionAttribute:Attribute
    {
        public PermissionAttribute(int powerId)
        {
            PowerId = powerId;
        }
        public int PowerId { get; set; }
    }
}
