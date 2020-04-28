using DataBase.Interface;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    /// <summary>
    /// 管理员的日志记录类
    /// </summary>
   public class Log
    {
        public jingshenContext db { get; set; }
        public IActionContextAccessor accessor { get; set; }
        public Log(jingshenContext db, IActionContextAccessor accessor)
        {
            this.db = db;
            this.accessor = accessor;
     
        }
        /// <summary>
        /// 单个操作记录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="dowhat"></param>
        public   void SetLog(HttpContext httpContext,string dowhat)
        {
            //adminid
            httpContext.Request.Cookies.TryGetValue("adminid", out string adminnum);
            int adminid = -1;
            if(adminnum!=null)
             adminid = (int.Parse(adminnum));
            //ip
            string ip = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            //时间戳
            UInt32 time = HelpClass.ConvertToTime(DateTime.Now);
            var log = new HmAdminLog()
            {
                AdminId = adminid,
                LogInfo = dowhat,
                LogIp = ip,
                LogTime = time
            };
            db.HmAdminLog.Add(log);
             db.SaveChanges();
        }
        /// <summary>
        /// 只为登录记录数据
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="dowhat"></param>
        /// <param name="loginid"></param>
        public void SetLog(HttpContext httpContext, string dowhat,int loginid)
        {
            //adminid          
            int adminid = loginid;           
            //ip
            string ip = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            //时间戳
            UInt32 time = HelpClass.ConvertToTime(DateTime.Now);
            var log = new HmAdminLog()
            {
                AdminId = adminid,
                LogInfo = dowhat,
                LogIp = ip,
                LogTime = time
            };
            db.HmAdminLog.Add(log);
            db.SaveChanges();
        }
        /// <summary>
        /// 多个操作记录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="dowhats"></param>
        public   void SetLog(HttpContext httpContext, string[] dowhats)
        {
            //adminid
            httpContext.Request.Cookies.TryGetValue("adminid", out string adminnum);
            int adminid = -1;
            if (adminnum != null)
                adminid = (int.Parse(adminnum));
            //ip
            string ip = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            //时间戳
            UInt32 time = HelpClass.ConvertToTime(DateTime.Now);
            foreach (var dowhat in dowhats)
            {
                var log = new HmAdminLog()
                {
                    AdminId = adminid,
                    LogInfo = dowhat,
                    LogIp = ip,
                    LogTime = time
                };
                db.HmAdminLog.Add(log);
            }
            db.SaveChanges();
        }


    }


    /// <summary>
    /// 用户的日志记录类
    /// </summary>
    public class UserLog
    {
        private readonly IActionContextAccessor _accessor;
        private readonly jingshenContext _db;

        public UserLog(IActionContextAccessor accessor, jingshenContext db)
        {
            this._accessor = accessor;
            _db = db;
        }

        public async Task SetLog( HttpContext httpContext)
        {
            //ip
            string ip = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();                        
            var log = new HmUserLog()
            {
               Ip=ip,
               Time=DateTime.Now
            };
            _db.HmUserLog.Add(log);
            _db.SaveChanges();
        }

    }


}
