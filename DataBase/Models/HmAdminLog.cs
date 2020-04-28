using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmAdminLog
    {
        public uint Id { get; set; }
        public int AdminId { get; set; }
        public string LogInfo { get; set; }
        public string LogIp { get; set; }
        public uint LogTime { get; set; }
    }
}
