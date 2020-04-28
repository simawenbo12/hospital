using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class HmReview
    {
        public int Id { get; set; }
        public int Adminid { get; set; }
        public int Review { get; set; }
        public int? Reviewtime { get; set; }
    }
}
