using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Carpooling.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string roles = context.HttpContext.Session.GetString("CurrentRoles");

            if (roles != null)
            {
                var roleList = roles.Split(',');
                if (!roleList.Contains(this.Role))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new RedirectResult("/Authentication/Login");
            }
        }
    }
}
