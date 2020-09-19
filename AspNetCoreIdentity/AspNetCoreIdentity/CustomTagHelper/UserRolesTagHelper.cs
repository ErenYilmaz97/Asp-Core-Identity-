using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreIdentity.CustomTagHelper
{
    [HtmlTargetElement("td",Attributes = "user-roles")]
    public class UserRolesTagHelper : TagHelper
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRolesTagHelper(UserManager<AppUser> UserManager)
        {
            _userManager = UserManager;
        }

        [HtmlAttributeName("user-roles")]
        public string UserID { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserID);

            var UserRoles = await _userManager.GetRolesAsync(user);

            string html = string.Empty;

            UserRoles.ToList().ForEach(x =>
            {
                html += $"<span class='badge'><p class='text-primary'>{x}</p></span>";
            });

            output.Content.SetHtmlContent(html);


        }
    }
}
