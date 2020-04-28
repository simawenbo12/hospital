using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.TempModel
{
    /// <summary>
    /// 显示管理员账号可角色的中间类
    /// </summary>
   public class HmAdminTempMod
    {
        public HmAdmin HmAdmin { get; set; }
        public HmRbacRole  Role{ get; set; }

    }
}
