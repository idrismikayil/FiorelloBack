#pragma checksum "C:\Users\HP\OneDrive\Masaüstü\Backend\FiorelloBack\P512FiorelloBack\Areas\Dashboard\Views\Category\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "57e75df9994a9630d66706b0c013fd011cdec1f4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Dashboard_Views_Category_Detail), @"mvc.1.0.view", @"/Areas/Dashboard/Views/Category/Detail.cshtml")]
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
#line 1 "C:\Users\HP\OneDrive\Masaüstü\Backend\FiorelloBack\P512FiorelloBack\Areas\Dashboard\Views\_ViewImports.cshtml"
using P512FiorelloBack;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HP\OneDrive\Masaüstü\Backend\FiorelloBack\P512FiorelloBack\Areas\Dashboard\Views\_ViewImports.cshtml"
using P512FiorelloBack.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\HP\OneDrive\Masaüstü\Backend\FiorelloBack\P512FiorelloBack\Areas\Dashboard\Views\_ViewImports.cshtml"
using P512FiorelloBack.Areas.Dashboard.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57e75df9994a9630d66706b0c013fd011cdec1f4", @"/Areas/Dashboard/Views/Category/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b254af245082f8b89460f03b70af82d839a47e66", @"/Areas/Dashboard/Views/_ViewImports.cshtml")]
    public class Areas_Dashboard_Views_Category_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Category>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\HP\OneDrive\Masaüstü\Backend\FiorelloBack\P512FiorelloBack\Areas\Dashboard\Views\Category\Detail.cshtml"
  
    ViewData["Title"] = "Detail";

#line default
#line hidden
#nullable disable
            WriteLiteral(@" <div class=""main-panel"">        
        <div class=""content-wrapper"">
          <div class=""row"">
            <div class=""col-md-6 grid-margin stretch-card"">
              <div class=""card"">
                <div class=""card-body"">
                  <h4 class=""card-title"">");
#nullable restore
#line 11 "C:\Users\HP\OneDrive\Masaüstü\Backend\FiorelloBack\P512FiorelloBack\Areas\Dashboard\Views\Category\Detail.cshtml"
                                    Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                  \r\n                  <p>\r\n                   ");
#nullable restore
#line 14 "C:\Users\HP\OneDrive\Masaüstü\Backend\FiorelloBack\P512FiorelloBack\Areas\Dashboard\Views\Category\Detail.cshtml"
              Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                  </p>\r\n                </div>\r\n              </div>\r\n            </div>\r\n             </div>\r\n              </div>\r\n            </div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Category> Html { get; private set; }
    }
}
#pragma warning restore 1591
