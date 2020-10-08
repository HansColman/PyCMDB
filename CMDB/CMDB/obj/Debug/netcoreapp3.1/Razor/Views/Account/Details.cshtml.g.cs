#pragma checksum "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b136858d565519ada2cda06857986e52146388bb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Details), @"mvc.1.0.view", @"/Views/Account/Details.cshtml")]
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
#line 1 "C:\Software\PyCMDB\CMDB\CMDB\Views\_ViewImports.cshtml"
using CMDB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Software\PyCMDB\CMDB\CMDB\Views\_ViewImports.cshtml"
using CMDB.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b136858d565519ada2cda06857986e52146388bb", @"/Views/Account/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c0df55d47f24a4572e6ba6748a8f7caa8b0c8be", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CMDB.Models.Account>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AddNew", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
 if ((bool)ViewData["InfoAccess"])
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2>");
#nullable restore
#line 7 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
   Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h2>
    <table class=""table table-striped table-bordered"">
        <thead class=""thead-light"">
            <tr>
                <th>UserID</th>
                <th>Type</th>
                <th>Application</th>
                <th>Active</th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 18 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 21 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.UserID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 22 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.Type.Type));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 23 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.Application.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 24 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.Active));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 26 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b136858d565519ada2cda06857986e52146388bb5803", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 30 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
     foreach (var item in Model)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
         if ((bool)ViewData["IdentityOverview"])
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <H3>Identity overview</H3>\r\n");
#nullable restore
#line 35 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
             if (item.Identities.Count > 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <table class=""table table-striped table-bordered"">
                    <thead class=""thead-light"">
                        <tr>
                            <th>Name</th>
                            <th>UserID</th>
                            <th>E Mail</th>
                            <th>Language</th>
                            <th>Type</th>
");
#nullable restore
#line 45 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                             if ((bool)ViewData["ReleaseIdentity"] && item.AccID > 1)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <th>Action</th>\r\n");
#nullable restore
#line 48 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tr>\r\n                    </thead>\r\n                    <tbody>\r\n");
#nullable restore
#line 52 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                         foreach(IdenAccount idenAccount in item.Identities)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td class=\"small\">");
#nullable restore
#line 55 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                         Write(idenAccount.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"small\">");
#nullable restore
#line 56 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                         Write(idenAccount.Identity.UserID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"small\">");
#nullable restore
#line 57 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                         Write(idenAccount.Identity.EMail);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"small\">");
#nullable restore
#line 58 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                         Write(idenAccount.Identity.Language.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td class=\"small\">");
#nullable restore
#line 59 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                         Write(idenAccount.Identity.Type.Type);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 60 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                             if ((bool)ViewData["ReleaseIdentity"] && item.AccID > 1)
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                 if (idenAccount.ValidUntil >= new DateTime())
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td class=\"small\"><a class=\"btn btn-danger\"");
            BeginWriteAttribute("href", " href=\"", 2673, "\"", 2755, 4);
            WriteAttributeValue("", 2680, "/Account/ReleaseIdentity&id=", 2680, 28, true);
#nullable restore
#line 64 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
WriteAttributeValue("", 2708, item.AccID, 2708, 11, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2719, "&IdenId=", 2719, 8, true);
#nullable restore
#line 64 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
WriteAttributeValue("", 2727, idenAccount.Identity.IdenID, 2727, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" id=\"ReleaseIdentity\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Release Identity\"><span class=\"fa fa-user-minus\"></span></a></td>\r\n");
#nullable restore
#line 65 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <td class=\"small\"></td>\r\n");
#nullable restore
#line 69 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                                 
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tr>\r\n");
#nullable restore
#line 72 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n");
#nullable restore
#line 75 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>No Identities assigned to this Account</p>\r\n");
#nullable restore
#line 79 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 79 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
             
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <H3>Log overview</H3>\r\n");
#nullable restore
#line 82 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
         if (item.Logs.Count > 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <table class=""table table-striped table-bordered"">
                <thead class=""thead-light"">
                    <tr>
                        <th>Date</th>
                        <th>Text</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 92 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                     foreach (Log log in item.Logs)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 95 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                           Write(log.LogDate.ToString(ViewData["LogDateFormat"].ToString()));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 96 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                           Write(log.LogText);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 98 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n");
#nullable restore
#line 101 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p>No logs for to this Account</p>\r\n");
#nullable restore
#line 105 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 105 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
         
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 106 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
     
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>You do not access to this page</p>\r\n");
#nullable restore
#line 111 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Details.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CMDB.Models.Account>> Html { get; private set; }
    }
}
#pragma warning restore 1591