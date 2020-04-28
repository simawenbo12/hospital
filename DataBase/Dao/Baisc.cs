using DataBase.Interface;
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Dao
{
    public abstract class Baisc<T> : IBasic<T> where T : class
    {
        protected readonly Models.jingshenContext _db;

        public Baisc(jingshenContext db)
        {
            _db = db;
        }
        public async virtual Task<int> CreateAsync(T t)
        {
            _db.Set<T>().Add(t);
            return await _db.SaveChangesAsync();       
        }

        public async virtual  Task<int> DeleteTrueAsync(int id)
        {

            _db.Set<T>().Remove(_db.Set<T>().Find(id));
            return await _db.SaveChangesAsync();
        }

        public virtual List<T> FindAll()
        {
            return _db.Set<T>().ToList();
        }

        public virtual T FindByID(int ID)
        {
           return _db.Set<T>().Find(ID);
        }
    
        public virtual async Task<int> UpdateAsync(T t)
        {
            _db.Set<T>().Update(t);
            return await _db.SaveChangesAsync();
        }
        public jingshenContext GetJingshenContext()
        {
            return this._db;
        }

        public  abstract Task<int> CreateOrUpdate(T t);
      
    }
}
