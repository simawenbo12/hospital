using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmArticleCategory
    {
        public int Id { get; set; }
        public uint ParentId { get; set; }
        public string Name { get; set; }
    }
}
