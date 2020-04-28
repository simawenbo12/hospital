using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    public static class HelpClass
    {
        /// <summary>
        /// 获得MD5加密后的密码
        /// </summary>
        /// <param name="str">用户输入密码</param>
        /// <returns></returns>
       public static string GetMD5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x");
            }
            return pwd;

        }

        /// <summary>
        /// 判断角色的权利是否选中
        /// </summary>
        /// <param name="list">当前角色的权利</param>
        /// <param name="id">权利id</param>
        /// <returns></returns>
        public static bool isCheckByRole(List<HmRbacPowerRole> list,int id)
        {
            foreach (var x in list)
            {
                if (x.PowerId == id) return true;
            }

            return false;
        }
        public async static void SetLog(IActionContextAccessor accessor,jingshenContext db,HttpContext httpContext,string dowhat,int loginid=-1)
        {
            //adminid
            httpContext.Request.Cookies.TryGetValue("adminid", out string adminnum);
            int adminid = -1;
            if (adminnum == null) adminid = loginid;
            else adminid = (int.Parse(adminnum));
            //ip
            string  ip = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            //时间戳
            UInt32 time = ConvertToTime(DateTime.Now);
            var log = new HmAdminLog()
            {
                AdminId=adminid,LogInfo=dowhat,LogIp=ip,LogTime=time
            };
            db.HmAdminLog.Add(log);
            await db.SaveChangesAsync();
        }
        public async static void SetLog(IActionContextAccessor accessor, jingshenContext db, HttpContext httpContext, string[] dowhats, int loginid = -1)
        {
            //adminid
            httpContext.Request.Cookies.TryGetValue("adminid", out string adminnum);
            int adminid = -1;
            if (adminnum == null) adminid = loginid;
            else adminid = (int.Parse(adminnum));
            //ip
            string ip = accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString();
            //时间戳
            UInt32 time = ConvertToTime(DateTime.Now);
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
            await db.SaveChangesAsync();
        }
        /// <summary>
        /// 日期换成时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static uint ConvertToTime(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var a = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return (uint)a;
        }
        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToTime(uint timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(timeStamp).AddHours(8);
        }
        /// <summary>
        /// 获取文章分类的正确顺序
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<HmArticleCategory> GetHmArticleCategoryOrder(List<HmArticleCategory> data)
        {
            var dataclone = new List<HmArticleCategory>();
            foreach (var x in data)
            {
                dataclone.Add(new HmArticleCategory { Id = x.Id, ParentId = x.ParentId, Name = x.Name });
            }
            string k = "　　";
            string zong = k + " ∟";
            List<HmArticleCategory> list = new List<HmArticleCategory>();
            list.Add(dataclone.Find(x => x.Id == 0));//顶级分类置顶
            var temp = dataclone.FindAll(x => x.Id == 0);//找到顶级分类
            dataclone.Remove(dataclone.Find(x => x.Id == 0));//移除特殊的顶级分类
            List<HmArticleCategory> ceng = GetCeng(dataclone, temp);//初设化第一层数
            while (ceng.Count != 0)
            {
                foreach (var x in ceng)
                {
                    x.Name = zong + x.Name;
                    int num = 1 + list.IndexOf(list.Find(y => y.Id == x.ParentId));
                    list.Insert(num, x);
                }
                ceng = GetCeng(dataclone, ceng);
                zong = k + zong;
            }
            return list;

        }
        /// <summary>
        /// 获取下一层文章顺序
        /// </summary>
        /// <param name="origindata"></param>
        /// <param name="data"></param>
        /// <returns></returns>
       private static List<HmArticleCategory> GetCeng(List<HmArticleCategory> origindata, List<HmArticleCategory> data)
        {
            List<HmArticleCategory> list = new List<HmArticleCategory>();
            foreach (var x in origindata)
            {
                foreach (var y in data)
                {
                    if (x.ParentId == y.Id)
                    {
                        list.Add(x);
                        break;
                    }
                }
            }
            return list;
        }

    }
}
