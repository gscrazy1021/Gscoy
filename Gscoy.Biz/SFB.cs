﻿using Gscoy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gscoy.Biz
{
    public class SFB
    {
        public string GetHouse()
        {
            DataBaseHelper helper = DataBaseHelper.GetInstance(Common.Enums.DBUserType.User_R);
            var result = helper.ExecuteScalar("select getdate()");
            return result.ToString();
        }
    }
}
