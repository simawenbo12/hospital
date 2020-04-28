using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Interface
{
    public interface IHmAd:IBasic<Models.HmAd>
    {
        List<TempModel.HmAdTempMod> GetHmAdTempModels();
    }
}
