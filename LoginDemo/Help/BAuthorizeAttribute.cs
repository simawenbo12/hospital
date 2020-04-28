using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Help
{
    public class BAuthorizeAttribute:AuthorizeAttribute
    {
        public const string BAuthenticationScheme = nameof(BAuthenticationScheme);
        public BAuthorizeAttribute()
        {
            this.AuthenticationSchemes = BAuthenticationScheme;
        }
    }
}
