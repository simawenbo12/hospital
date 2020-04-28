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

namespace LoginDemo.Pages.Manager.RoleMod
{
    public class RoleOperatingModel : MyPageModelForIU<HmRbacRole,IHmRole>
    {
        private readonly jingshenContext _jsdao;
        private readonly Log _log;

        public RoleOperatingModel(IHmRole dao, LocalStr localStr,Log log) : base(dao, $"/{DefaultData.Manager}/RoleMod/Role", localStr)
        {
            _log = log;
            _jsdao = dao.GetJingshenContext();
            First = _jsdao.HmRbacPower.Where(x => x.ParentId == 0).OrderBy(x=>x.Sort).ToList();
            Secend = _jsdao.HmRbacPower.Where(x => x.ParentId != 0).ToList();
        }
        /// <summary>
        /// 用户选中的权利
        /// </summary>
        [BindProperty]
        public string[] Roles { get; set; }
        /// <summary>
        /// 一级目录
        /// </summary>
        public List<HmRbacPower> First { get; set; }
        /// <summary>
        /// 二级目录
        /// </summary>
        public List<HmRbacPower> Secend { get; set; }
        /// <summary>
        /// 编辑存在角色的当前权限
        /// </summary>
        public List<HmRbacPowerRole> SelectRole { get; set; }
        public  override IActionResult OnGet(int? id)
        {
            if(id!=null)//编辑
            {
                GetT = _dao.FindByID((int)id);
                SelectRole = _jsdao.HmRbacPowerRole.Where(x => x.RoleId == id).ToList();
            }
            return Page();
        }
        public async override Task<IActionResult> OnPost()
        {
            bool isCreate = GetT.Id == 0 ? true : false;
            await  _dao.CreateOrUpdate(GetT);
            if(isCreate) _log.SetLog(HttpContext, $"添加角色 ID :{GetT.Id}");
            else _log.SetLog(HttpContext, $"编辑角色 ID :{GetT.Id}");


            if (GetT.Id == 0)//新建
            {
                
                GetT.Id = _dao.FindAll().Where(x => x.Name.Equals(GetT.Name)).LastOrDefault().Id;
            }
            else//更新
            {                
                var deletelist = _jsdao.HmRbacPowerRole.Where(x => x.RoleId == GetT.Id).ToList();
                foreach (var x in deletelist)
                {
                    _jsdao.HmRbacPowerRole.Remove(x);
                }
                await _jsdao.SaveChangesAsync();             
            }
            //删除后执行插入操作
            foreach (var x in Roles)
            {
                _jsdao.HmRbacPowerRole.Add(new HmRbacPowerRole() { RoleId = GetT.Id, PowerId = int.Parse(x) });
            }
            await _jsdao.SaveChangesAsync();

            return RedirectToPage(_OriginUrl);
        }


    }
}