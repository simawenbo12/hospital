using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help.HelpPageClass
{
    /// <summary>
    /// .net 2.2 获取wwwroot地址
    /// </summary>
    public class LocalStr
    {
        public LocalStr(string urlStr)
        {
            UrlStr = urlStr;
        }

        public string UrlStr { get; set; }
    }
}
