#pragma checksum "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "69079a8acd3f27482907527ba6f553e317ad7fb8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Discord__wordStats), @"mvc.1.0.view", @"/Views/Discord/_wordStats.cshtml")]
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
#line 1 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\_ViewImports.cshtml"
using DiscordApp2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\_ViewImports.cshtml"
using DiscordApp2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"69079a8acd3f27482907527ba6f553e317ad7fb8", @"/Views/Discord/_wordStats.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c05dfffac33fd7b5f7c70e3d37ea2f66a1737a09", @"/Views/_ViewImports.cshtml")]
    public class Views_Discord__wordStats : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<StatsModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
 if (string.IsNullOrEmpty(Model.ChannelInfo.Word))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p class=\"text-danger\">\r\n        Please type the word!\r\n    </p>\r\n");
#nullable restore
#line 8 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p>");
#nullable restore
#line 11 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
  Write(Model.ResultsAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" results for:</p>\r\n    <p>Word: \"");
#nullable restore
#line 12 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
         Write(Model.ChannelInfo.Word);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" Channel ID: ");
#nullable restore
#line 12 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
                                              Write(Model.ChannelInfo.Channel);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Posts amount: ");
#nullable restore
#line 12 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
                                                                                       Write(Model.ChannelInfo.HistoryDeep);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    <table>\r\n        <thead>\r\n            <tr>\r\n                <th>User name</th>\r\n                <th>Posts amount</th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 21 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
             foreach (UserResult userResult in Model.UserResults)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 24 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
                   Write(userResult.User.username);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 25 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
                   Write(userResult.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                </tr>\r\n");
#nullable restore
#line 27 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
#nullable restore
#line 30 "E:\Uczelnia2021\SystemyRozproszone\lab5_rest\zadanie\DiscordApp2\Views\Discord\_wordStats.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<StatsModel> Html { get; private set; }
    }
}
#pragma warning restore 1591