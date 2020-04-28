using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help
{
    public class FAuthorizeAttribute:AuthorizeAttribute
    {
        public const string FAuthenticationSchem = nameof(FAuthenticationSchem);
        public FAuthorizeAttribute()
        {
            this.AuthenticationSchemes = FAuthenticationSchem;
        }
    }
}
