using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.TempModel
{
    public class UserLogTempMod
    {
        /// <summary>
        /// 今日浏览量
        /// </summary>
        public long TodayVisitor { get; set; }
        /// <summary>
        /// 今日用户量
        /// </summary>
        public long TodayUser { get; set; }
        /// <summary>
        /// 当月访问量
        /// </summary>
        public long MouthVisitor { get; set; }
        /// <summary>
        /// 当月用户量
        /// </summary>
        public long MouthUser { get; set; }

    }

}
