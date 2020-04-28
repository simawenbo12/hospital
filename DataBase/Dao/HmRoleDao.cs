using DataBase.Interface;
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Dao
{
    public class HmRoleDao : Baisc<HmRbacRole>, IHmRole
    {

        public HmRoleDao(jingshenContext db):base(db)
        {

        }

        public async override Task<int> CreateOrUpdate(HmRbacRole t)
        {
            if (t.Id == 0)//新建
            {
              return  await CreateAsync(t);                
            }
            else//更新
            {
              return   await UpdateAsync(t);
            }


        }
        public async override Task<int> DeleteTrueAsync(int id)
        {
            var temp = _db.HmRbacRoleAdmin.Where(x => x.RoleId == id).ToList();
            if (temp == null || temp.Count == 0)//判断该角色是否某个用户
            {
                var data = _db.HmRbacPowerRole.Where(x => x.RoleId == id).ToList();
                foreach (var x in data)
                {
                    _db.HmRbacPowerRole.Remove(x);
                }
                _db.HmRbacRole.Remove(_db.HmRbacRole.Find(id));
                return await _db.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(0);
            }
        }
    }
}
