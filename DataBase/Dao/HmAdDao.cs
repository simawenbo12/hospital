using DataBase.Interface;
using DataBase.Models;
using DataBase.TempModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Dao
{
   public class HmAdDao:Baisc<Models.HmAd>,IHmAd
    {
        public HmAdDao(Models.jingshenContext db) : base(db)
        {

        }
        public async override Task<int> CreateOrUpdate(HmAd t)
        {
            if (t.Id == 0) return await CreateAsync(t);
            else return await UpdateAsync(t);
        }

        public List<HmAdTempMod> GetHmAdTempModels()
        {
            var ad = FindAll();
            var adp = _db.HmAdPosition.ToList();
            var list = new List<TempModel.HmAdTempMod>();
            foreach (var x in ad)
            {
                foreach (var y in adp)
                {
                    if (x.PositionId == y.Id)
                    {
                        list.Add(new HmAdTempMod() { HmAd = x, HmAdPosition = y });
                        break;
                    }
                }
            }
            return list;
        }
    }
}
