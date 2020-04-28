using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help
{
    public static class JingshenContextHelp
    {
        /// <summary>
        /// 执行sql存储过程
        /// </summary>
        /// <typeparam name="TElement">存储过程返回的类,采用反射</typeparam>
        /// <param name="sql">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public static IEnumerable<TElement> SqlQuery<TElement>(this jingshenContext context,string sql, params object[] parameters) where TElement : new()
        {
            var connection = context.Database.GetDbConnection();
            using (var cmd = connection.CreateCommand())
            {
                context.Database.OpenConnection();
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters);
                var dr = cmd.ExecuteReader();
                var columnSchema = dr.GetColumnSchema();
                var data = new List<TElement>(); ;
                while (dr.Read())
                {
                    TElement item = new TElement();
                    Type type = item.GetType();
                    foreach (var kv in columnSchema)
                    {
                        var propertyInfo = type.GetProperty(kv.ColumnName);
                        if (kv.ColumnOrdinal.HasValue && propertyInfo != null)
                        {
                            //注意需要转换数据库中的DBNull类型
                            var value = dr.IsDBNull(kv.ColumnOrdinal.Value) ? null : dr.GetValue(kv.ColumnOrdinal.Value);
                            propertyInfo.SetValue(item, value);
                        }
                    }
                    data.Add(item);
                }
                dr.Dispose();
                return data;
            }
        }
        /// <summary>
        /// 根据id返回文章数据,若一级分类则返回1级的全部,2级则返回2级全部
        /// </summary>
        /// <param name="hmArticles"></param>
        /// <param name="id">分类id</param>
        /// <returns></returns>
        public static List<HmArticle> GetArticles(this jingshenContext context, int id)
        {
            var list = new List<HmArticle>();
            if (context.HmArticleCategory.Find(id).ParentId == 0)//一级分类
            {
                var HmArticleCategories = context.HmArticleCategory.AsNoTracking().Where(x => x.ParentId == id).ToList();
                foreach (var x in HmArticleCategories)
                {
                    list.AddRange(context.HmArticle.AsNoTracking().ToList().FindAll(y => y.CateId == x.Id));
                }
            }
            else//二级分类
            {
                list.AddRange(context.HmArticle.ToList().FindAll(x => x.CateId == id));
            }
            return list;


        }
        /// <summary>
        /// 筛选文章ishow是否是显示
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<HmArticle> NeedShow(this List<HmArticle> list)
        {
            return list.Where(x => x.IsShow == 1).ToList();
        }
        /// <summary>
        /// 筛选文章是否过审
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<HmArticle> NeedReview(this List<HmArticle> list,jingshenContext db)
        {
            var review = db.HmReview.ToList();
            var finalllist = new List<HmArticle>();
            foreach (var x in list)
            {
                if (review.Find(y=>y.Id==x.Id).Review==2)
                {
                    finalllist.Add(x);
                }
            }
            return finalllist;

        }




    }
}
