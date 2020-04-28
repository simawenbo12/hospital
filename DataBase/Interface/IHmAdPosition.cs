using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Interface
{
  public  interface IHmAdPosition:IBasic<Models.HmAdPosition>
    {
        List<SelectListItem> GetSelectListItems();
    }
}
