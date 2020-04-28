using DataBase.Models;
using DataBase.TempModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Interface
{
    public interface IHmAdmin:IBasic<HmAdmin>
    {
        List<HmAdminTempMod> FindAndRole();
        List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetSelectListItems();
    }
}
