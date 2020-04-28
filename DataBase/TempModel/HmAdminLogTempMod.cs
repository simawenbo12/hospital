using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.TempModel
{
  public  class HmAdminLogTempMod
    {
        public HmAdmin HmAdmin { get; set; }
        public HmAdminLog HmAdminLog { get; set; }
        public DateTime DateTime { get; set; }
    }
}
