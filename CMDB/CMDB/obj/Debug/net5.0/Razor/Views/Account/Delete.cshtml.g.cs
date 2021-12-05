#pragma checksum "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "47437dcbdd3adfcc80a23401cb90f43d46d81e88"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Delete), @"mvc.1.0.view", @"/Views/Account/Delete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"47437dcbdd3adfcc80a23401cb90f43d46d81e88", @"/Views/Account/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c0df55d47f24a4572e6ba6748a8f7caa8b0c8be", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CMDB.Domain.Entities.Account>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_deleteForm", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
 if ((bool)ViewData["DeleteAccess"])
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h2>");
#nullable restore
#line 7 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
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
#line 18 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 21 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.UserID));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 22 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.Type.Type));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 23 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.Application.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 24 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
                   Write(Html.DisplayTextFor(modelItem => item.Active));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 26 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "47437dcbdd3adfcc80a23401cb90f43d46d81e885804", async() => {
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
#line 30 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>You do not access to this page</p>\r\n");
#nullable restore
#line 34 "C:\Software\PyCMDB\CMDB\CMDB\Views\Account\Delete.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CMDB.Domain.Entities.Account>> Html { get; private set; }
    }
}
#pragma warning restore 1591
