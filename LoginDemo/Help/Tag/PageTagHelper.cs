using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help.Tag
{
    /// <summary>
    /// 分页标签
    /// </summary>
    public class PagerTagHelper : TagHelper
    {
        public PageData PageData { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            if (PageData.PageSize <= 0) { PageData.PageSize = 15; }
            if (PageData.CurrentPage <= 0) { PageData.CurrentPage = 1; }
            if (PageData.Total <= 0) { return; }

            //总页数
            var totalPage = PageData.Total / PageData.PageSize + (PageData.Total % PageData.PageSize > 0 ? 1 : 0);
            if (totalPage <= 0) { return; }
            //当前路由地址
            if (string.IsNullOrEmpty(PageData.RouteUrl))
            {

                //PagerOption.RouteUrl = helper.ViewContext.HttpContext.Request.RawUrl;
                if (!string.IsNullOrEmpty(PageData.RouteUrl))
                {

                    var lastIndex = PageData.RouteUrl.LastIndexOf("/");
                    PageData.RouteUrl = PageData.RouteUrl.Substring(0, lastIndex);
                }
            }
            PageData.RouteUrl = PageData.RouteUrl.TrimEnd('/');

            //构造分页样式
            var sbPage = new System.Text.StringBuilder(string.Empty);
            sbPage.Append("<nav>");
            sbPage.Append("  <ul class=\"pagination\">");
            //第一页标签
            sbPage.Append($" <li><a href=\"{PageData.RouteUrl}1\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>");
            //上一页标签
            sbPage.AppendFormat("       <li><a href=\"{0}{1}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&lt;</span></a></li>",
                                    PageData.RouteUrl,
                                    PageData.CurrentPage - 1 <= 0 ? 1 : PageData.CurrentPage - 1);

            //for (int i = 1; i <= totalPage; i++)
            //{

            //    sbPage.AppendFormat("       <li {1}><a href=\"{2}{0}\">{0}</a></li>",
            //        i,
            //        i == PageData.CurrentPage ? "class=\"active\"" : "",
            //        PageData.RouteUrl);
            //}
            //默认显示7页数据
            for (int i = PageData.CurrentPage - 3 > 0 ? PageData.CurrentPage - 3 : 1; i <= totalPage && i <= PageData.CurrentPage + 3; i++)
            {
                sbPage.AppendFormat("<li class=\"{0}\"><a href=\"{1}{2}\">{2}</a></li>", i == PageData.CurrentPage ? "active" : "", PageData.RouteUrl, i);

            }





            sbPage.Append("       <li>");
            //下一页标签
            sbPage.AppendFormat("         <a href=\"{0}{1}\" aria-label=\"Next\">",
                                PageData.RouteUrl,
                                PageData.CurrentPage + 1 > totalPage ? PageData.CurrentPage : PageData.CurrentPage + 1);
            sbPage.Append("               <span aria-hidden=\"true\">&gt;</span>");
            sbPage.Append("         </a>");
            sbPage.Append("       </li>");
            //最后一页标签
            sbPage.Append($" <li><a href=\"{PageData.RouteUrl}{totalPage}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&raquo;</span></a></li>");
            sbPage.Append($"<li><a>总计{PageData.Total}条数据</a></li>");
            sbPage.Append("   </ul>");
            sbPage.Append("</nav>");


            output.Content.SetHtmlContent(sbPage.ToString());


        }
    }


    public class PageData
    {
        public PageData()
        {

        }
        public PageData(int currentPage, int total, string routeUrl, int pageSize = DefaultData.PageSize)
        {
            CurrentPage = currentPage;
            Total = total;
            PageSize = pageSize;
            RouteUrl = routeUrl;
        }
        /// <summary>
        /// 当前页  必传
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 总条数  必传
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 分页记录数（每页条数 默认每页15条）
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 路由地址(格式如：/Controller/Action) 默认自动获取
        /// </summary>
        public string RouteUrl { get; set; }
        /// <summary>
        /// 样式 默认 bootstrap样式 1
        /// </summary>
        public int StyleNum { get; set; }

    }
    public struct DefaultData
    {
        public const int PageSize = 20;//每一页显示的数据量
        public const string Manager = "Manager";
    }
}
