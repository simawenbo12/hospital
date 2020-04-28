using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmRbacRoleAdmin
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public int RoleId { get; set; }
    }
}
