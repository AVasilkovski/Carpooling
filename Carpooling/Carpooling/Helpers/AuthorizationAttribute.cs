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
            string RolesName = context.HttpContext.Session.GetString("CurrentRoles");

            if (RolesName != null)
            {
                var roleList = RolesName.Split(',');
                if (roleList.Length < 1)
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
