using DataBase.Interface;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Dao
{
     public class HmAdPositionDao:Baisc<HmAdPosition>, IHmAdPosition
    {
        public HmAdPositionDao(Models.jingshenContext db) : base(db)
        {

        }

        public async override Task<int> CreateOrUpdate(HmAdPosition t)
        {
            if (t.Id == 0) return await CreateAsync(t);
            else return await UpdateAsync(t);


        }

        public override Task<int> DeleteTrueAsync(int id)
        {
            var dao = GetJingshenContext();
            var data = dao.HmAd.Where(x => x.PositionId == id).FirstOrDefault();
            if (data == null) return base.DeleteTrueAsync(id);
            else return Task.FromResult(0);
        }

        public List<SelectListItem> GetSelectListItems()
        {
            var list = new List<SelectListItem>();
            foreach (var x in FindAll())
            {
                list.Add(new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            }
            return list;
        }
    }
}
