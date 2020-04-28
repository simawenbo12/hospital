using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.TempModel
{
   public class HmArticleTempMod
    {
        public HmArticle HmArticle { get; set; }
        public HmArticleCategory HmArticleCategory { get; set; }
        public DateTime DateTime { get; set; }
        public HmReview HmReview { get; set; }
        public HmAdmin HmAdmin { get; set; }
    }
}
