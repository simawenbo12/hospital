using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interface
{
     public interface IBasic<T> where T:class
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T t);
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> CreateAsync(T t);
        /// <summary>
        /// 删除数据库数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<int> DeleteTrueAsync(int id);
        /// <summary>
        /// 通过ID查找数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T FindByID(int ID);
        /// <summary>
        /// 返回所有数据
        /// </summary>
        /// <returns></returns>
        List<T> FindAll();

        Task<int> CreateOrUpdate(T t);

        Models.jingshenContext GetJingshenContext();

    }

}
