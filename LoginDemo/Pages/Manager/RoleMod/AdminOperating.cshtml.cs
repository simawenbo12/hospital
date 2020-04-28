using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Interface;
using DataBase.Models;
using LoginDemo.Help.HelpPageClass;
using LoginDemo.Help.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginDemo.Pages.Manager.RoleMod
{
    public class AdminOperatingModel : MyPageModelForIU<HmAdmin,IHmAdmin>
    {
        public List<SelectListItem> SLitem { get; set; }
        [BindProperty]
        public string SelectP { get; set; }//选择的角色id
        [BindProperty]
        public string Password { get; set; }//这个的name和asp-forGetT.Password是不一样的.
        private readonly Log _log;
        public AdminOperatingModel(IHmAdmin dao,  LocalStr localStr,Log log) : base(dao, $"/{DefaultData.Manager}/RoleMod/index", localStr)
        {
            _log = log;
        }

        public override IActionResult OnGet(int? id)
        {
            if (id != null)
            {
                GetT = _dao.FindByID((int)id);
                SelectP = _dao.GetJingshenContext().HmRbacRoleAdmin.Where(x => x.AdminId == id).FirstOrDefault().RoleId.ToString();
            }            
                SLitem = _dao.GetSelectListItems();            
                return Page();
        }
        public async override Task<IActionResult> OnPost()
        {
            var dao = _dao.GetJingshenContext();
            if (Password != null && Password.Length != 0) GetT.Password = HelpClass.GetMD5(Password);
            //ip和时间暂时设置
            GetT.Ip = _log.accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString(); ;
            GetT.Lasttime =(int) HelpClass.ConvertToTime(DateTime.Now) ;

            if (GetT.Id != 0)//更新
            {
                var temp = dao.HmRbacRoleAdmin.Where(x => x.AdminId == GetT.Id).FirstOrDefault();
                temp.RoleId = int.Parse(SelectP);
                dao.HmRbacRoleAdmin.Update(temp);
                _log.SetLog(HttpContext, $"编辑管理员 ID :{GetT.Id} 到角色 ID :{SelectP}");
                await dao.SaveChangesAsync();
                await _dao.CreateOrUpdate(GetT);
            }
            else//新建
            {
                await _dao.CreateOrUpdate(GetT);
                _log.SetLog(HttpContext, $"新建管理员 ID:{GetT.Id} 到角色 ID :{SelectP}");
                var temp = dao.HmAdmin.Where(x => x.Username.Equals(GetT.Username)).FirstOrDefault().Id.ToString();
                dao.HmRbacRoleAdmin.Add(new HmRbacRoleAdmin() { AdminId = int.Parse(temp), RoleId = int.Parse(SelectP) });
                await dao.SaveChangesAsync();
            }
            return RedirectToPage(_OriginUrl);
        }

    }
}