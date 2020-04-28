using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using DataBase.TempModel;
using LoginDemo.Help;
using LoginDemo.Help.HelpPageClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoginDemo.Pages.Manager
{
    public class rightModel : PageModel
    {
        private readonly jingshenContext _db;
        public UserLogTempMod UserLogTempMod { get; set; }
        public rightModel(jingshenContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            SetUserLogTempMod();
        }

        private void SetUserLogTempMod()
        {
            UserLogTempMod = _db.SqlQuery<UserLogTempMod>("getfourdate").FirstOrDefault();
        }

        public JsonResult OnGetData()
        {    
            var b = _db.SqlQuery<Da2>("getjson");
            ToJson toJson = new ToJson();
            DateTime time = DateTime.Now.AddDays(-29);

            while (time.Date != DateTime.Now.AddDays(1).Date)
            {
                toJson.time.Add(time.ToShortDateString().ToString());
                var s = b.ToList().Find(x => x.time.Date == time.Date);
                toJson.amount.Add((int)(s == null ? 0 : s.amountvisitor));
                var w = b.ToList().Find(x => x.time.Date == time.Date);
                toJson.order.Add((int)(w == null ? 0 : w.orderuser));
                time = time.AddDays(1);
            }

            return new JsonResult(toJson);
        }
      private  class Da2
        {
            public DateTime time { get; set; }
            public long amountvisitor { get; set; }
            public long orderuser { get; set; }
        }

    }
    /// <summary>
    /// 给前台统计json的类
    /// </summary>
    public class ToJson
    {
        public List<string> time { get; set; } = new List<string>();
        public List<int> amount { get; set; } = new List<int>();
        public List<int> order { get; set; } = new List<int>();

    }

}