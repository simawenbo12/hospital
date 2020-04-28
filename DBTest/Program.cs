using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new DataBase.Models.jingshenContext();
            GetTimeTest(data);
        }

        static string SetStr(jingshenContext db, string roleid)
        {
            var Power = db.HmRbacPower.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            var First = Power.Where(x => x.ParentId == 0).OrderBy(x => x.Sort).ToList();//获取全部一级目录
            var Secend = Power.Where(x => x.ParentId != 0).OrderBy(x => x.Sort).ToList();//获取全部二级目录
            if (roleid.Equals("1"))//特殊管理员权限
            {
                foreach (var f in First)
                {
                    stringBuilder.Append($"<li><a href=\"#\"><i class=\"fa fa-home\"></i><span class=\"nav-label\">{f.Name}</span><span class=\"fa arrow\"></span> </a> <ul class=\"nav nav-second-level\"><li>");
                    foreach (var s in Secend)
                    {
                        if (f.Id == s.ParentId)
                            stringBuilder.Append($"<a class=\"J_menuItem\" href=\"/{s.Controller + "/" + s.Action}\">{s.Name}</a>");
                    }
                    stringBuilder.Append("</li> </ul></li> ");
                }
            }
            else
            {
                var Role = db.HmRbacPowerRole.Where(x => x.RoleId == int.Parse(roleid)).OrderBy(x => x.PowerId).ToList();//升序排列powerrole
                var tempSecend = Secend.Join(Role, x => x.Id, y => y.PowerId,(x,y)=>new {x.Id,x.Name,x.ParentId,x.Controller,x.Action,x.Icon,x.Sort } ).OrderBy(x=>x.Sort).ToList();
                var tempFirst = First.Join(tempSecend, x => x.Id, y => y.ParentId, (x, y) => new { x.Id, x.Name, x.Icon, x.Sort }).OrderBy(x => x.Sort).Distinct().ToList();
                foreach (var f in tempFirst)
                {
                    stringBuilder.Append($"<li><a href=\"#\"><i class=\"fa fa-home\"></i><span class=\"nav-label\">{f.Name}</span><span class=\"fa arrow\"></span> </a> <ul class=\"nav nav-second-level\"><li>");
                    foreach (var s in tempSecend)
                    {
                        if (f.Id == s.ParentId)
                            stringBuilder.Append($"<a class=\"J_menuItem\" href=\"/{s.Controller + "/" + s.Action}\">{s.Name}</a>");
                    }
                    stringBuilder.Append("</li> </ul></li> ");
                }


            }
            return stringBuilder.ToString();


        }
        static void SetTest(jingshenContext db)
        {
            db.HmArticleCategory.ToList();
            var temp = new HmArticleCategory { ParentId = 0, Name = "测试" };
            db.HmArticleCategory.Add(temp);
            var a=db.SaveChanges();
            Console.WriteLine(a);
        }
        static void GetTimeTest(jingshenContext db)
        {
            var mouth =db.HmUserLog.Where(x => x.Time.Date <= DateTime.Now.Date && x.Time.Date >= DateTime.Now.AddDays(-30)).ToList();
            List<Res> list = new List<Res>();
            DateTime time = DateTime.Now.AddDays(-29);
            while (time.Date != DateTime.Now.AddDays(1).Date)
            {
                list.Add(new Res
                {
                    time = time,
                    amount = mouth.FindAll(x => x.Time.Date == time.Date).Count,
                    order= mouth.FindAll(x => x.Time.Date == time.Date).Select(x=>x.Ip).Distinct().ToList().Count
                });
                time=time.AddDays(1);
            }
           
            Console.WriteLine("s");
        }

    }
    public class Res
    {
        public DateTime time { get; set; }
        public int amount { get; set; } = 0;
        public int order { get; set; } = 0;
    }
}
