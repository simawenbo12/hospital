using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    public static class PageListHelp
    {
        /// <summary>
        /// 返回CurrentPage分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="CountPage"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<T> GetPageList<T>(List<T> list, PageData pageData, int CurrentPage, string url, int PageSize = DefaultData.PageSize) where T : class
        {
            try
            {
                int CountPage = list.Count;
                int min = (CurrentPage - 1) * PageSize;//下标
                int max = PageSize;//取的数量
                if (max + min >= CountPage) max = CountPage - min;
                pageData.CurrentPage = CurrentPage;
                pageData.Total = CountPage;
                pageData.RouteUrl = url;
                pageData.PageSize = PageSize;
                return list.GetRange(min, max);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static bool IsCurrentPageHasData<T>(List<T> list, int CurrentPage, int PageSize = DefaultData.PageSize)
        {
            try
            {
                int CountPage = list.Count;
                int min = (CurrentPage - 1) * PageSize;//下标
                int max = PageSize;//取的数量
                if (max + min >= CountPage) max = CountPage - min;
                var data = list.GetRange(min, max);
                return data.Count == 0 || data == null ? false : true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除后若当前页面无数据,返回上一页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">查找的数据list</param>
        /// <param name="pageData">分页类</param>
        /// <param name="url">跳转地址</param>
        /// <returns></returns>
        public static JsonResult GetSizeNum<T>(List<T> list, PageData pageData, string url) where T : class
        {
            string[] urlsplit = url.Split('=');
            if (urlsplit.Length == 2)
            {
                var data = PageListHelp.IsCurrentPageHasData(list, int.Parse(urlsplit[1]));
                if (!data)
                {
                    int temp = int.Parse(urlsplit[1]) - 1 > 0 ? int.Parse(urlsplit[1]) - 1 : 1;
                    return new JsonResult(temp);
                }
                else return new JsonResult(urlsplit[1]);
            }
            else
            {
                return new JsonResult(url);
            }


        }



    }
}
