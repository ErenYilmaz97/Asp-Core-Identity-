#pragma checksum "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "725b06dbe96e8a1df440a6bdc7296dc301fa1370"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Member_Index), @"mvc.1.0.view", @"/Views/Member/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 3 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\_ViewImports.cshtml"
using AspNetCoreIdentity.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"725b06dbe96e8a1df440a6bdc7296dc301fa1370", @"/Views/Member/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0588e77bafee6c9c55a62164376057f01f1eb84", @"/Views/_ViewImports.cshtml")]
    public class Views_Member_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserInfoModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Member/_MemberLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n    <div class=\"row\">\r\n        <div class=\"col-md-4 text-right\">\r\n");
#nullable restore
#line 12 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
              
                if (String.IsNullOrEmpty(Model.Picture))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <img src=\"Pictures/usericon.png\" />\r\n");
#nullable restore
#line 16 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <img");
            BeginWriteAttribute("src", " src=\"", 424, "\"", 444, 1);
#nullable restore
#line 19 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
WriteAttributeValue("", 430, Model.Picture, 430, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\r\n");
#nullable restore
#line 20 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


        </div>
            <div class=""col-md-8 mt-2"">
                <strong style=""font-size: 30px"">Kullanıcı Bilgileri</strong>
            </div>
        </div>

        <hr/>
        <table class=""table table-hover table-striped table-primary mt-3"">

            <tr>
                <th>UserName</th>
                <td>");
#nullable restore
#line 36 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
               Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Email</th>\r\n                <td>");
#nullable restore
#line 40 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
               Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>Phone</th>\r\n                <td>");
#nullable restore
#line 44 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                      
                        if (!String.IsNullOrEmpty(Model.PhoneNumber))
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                       Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                                              
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span>Belirtilmemiş</span>\r\n");
#nullable restore
#line 52 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                        }
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n            <tr>\r\n                <th>City</th>\r\n                <td>\r\n");
#nullable restore
#line 58 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                      
                        if (!String.IsNullOrEmpty(Model.City))
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                       Write(Model.City);

#line default
#line hidden
#nullable disable
#nullable restore
#line 61 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                                       
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span>Belirtilmemiş</span>\r\n");
#nullable restore
#line 66 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                        }

                    

#line default
#line hidden
#nullable disable
            WriteLiteral("</tr>\r\n            <tr>\r\n                <th>BirthDay</th>\r\n                <td>\r\n");
#nullable restore
#line 73 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                      
                        if (Model.BirthDay == null)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <p>Belirtilmemiş</p>\r\n");
#nullable restore
#line 77 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                        }
                        else
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 80 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                       Write(Model.BirthDay.Value.ToShortDateString());

#line default
#line hidden
#nullable disable
#nullable restore
#line 80 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
                                                                     ;
                        }

                                

#line default
#line hidden
#nullable disable
            WriteLiteral("</tr>\r\n            <tr>\r\n                <th>Gender</th>\r\n                <td>");
#nullable restore
#line 87 "C:\Users\eren_\Desktop\örnekc#\AspNetCoreIdentity\AspNetCoreIdentity\Views\Member\Index.cshtml"
               Write(Model.Gender);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            </tr>\r\n\r\n        </table>\r\n   \r\n    \r\n    \r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserInfoModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
