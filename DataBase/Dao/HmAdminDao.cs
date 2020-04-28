using DataBase.Interface;
using DataBase.Models;
using DataBase.TempModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Dao
{
    public class HmAdminDao : Baisc<HmAdmin>, IHmAdmin
    {
        public HmAdminDao(jingshenContext db):base(db)
        {

        }
        public async override Task<int> CreateOrUpdate(HmAdmin t)
        {
            if (t.Id == 0)
            {
                return await CreateAsync(t);
            }
            else
            {
                return await UpdateAsync(t);

            }
        }

        public List<HmAdminTempMod> FindAndRole()
        {
            var temp = _db.HmRbacRoleAdmin.OrderBy(x=>x.RoleId).ToList();
            List<HmAdminTempMod> list = new List<HmAdminTempMod>();
            foreach (var x in temp)
            {
                list.Add(new HmAdminTempMod()
                {
                    HmAdmin = FindByID(x.AdminId),
                    Role=_db.HmRbacRole.Find(x.RoleId)
                });
            }
            return list;
        }
        public override HmAdmin FindByID(int ID)
        {

            return _db.HmAdmin.Find(ID);
        }

        public async override Task<int> DeleteTrueAsync(int id)
        {
            _db.HmAdmin.Remove(_db.HmAdmin.Find(id));
            _db.HmRbacRoleAdmin.Remove(_db.HmRbacRoleAdmin.Where(x => x.AdminId == id).FirstOrDefault());
            return await _db.SaveChangesAsync();
        }

        public List<SelectListItem> GetSelectListItems()
        {

            var list = new List<SelectListItem>();
            foreach (var x in _db.HmRbacRole.ToList())
            {
                list.Add(new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            }
            return list;

        }
    }
}
