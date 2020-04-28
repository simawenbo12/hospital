using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace LoginDemo.Pages.Manager.ArticleMod
{
    public class ArticleReadModel : PageModel
    {
        private readonly jingshenContext _db;
        private IWebHostEnvironment _hostingenvironment;
        public HmArticle HmArticle { get; set; }

        public ArticleReadModel(jingshenContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingenvironment = hostingEnvironment;
        }

        public void OnGet(int id)
        {
            HmArticle = _db.HmArticle.Find(id);
        }
        public IActionResult OnGetExport(int id)
        {
            string sWebRootFolder = _hostingenvironment.WebRootPath+"/excel/";
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                //添加头
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "描述";
                worksheet.Cells[1, 3].Value = "内容";
                //添加值
                var temp = _db.HmArticle.Find(id);
                worksheet.Cells["A2"].Value = temp.Id;
                worksheet.Cells["B2"].Value = temp.Description;
                worksheet.Cells["C2"].Value = temp.Content;
                package.Save();
            }
            return File("/excel/"+sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

    }
}