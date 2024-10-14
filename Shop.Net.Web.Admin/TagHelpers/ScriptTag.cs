using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.Net.Web.Admin.TagHelpers;

[HtmlTargetElement("script", Attributes = "asp-location")]
public class ScriptTagHelper : TagHelper
{
    private static readonly List<string> HeadScripts = [];
    private static readonly List<string> FooterScripts = [];

    [HtmlAttributeName("asp-location")] public AspLocation? Location { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!Location.HasValue) return;

        var scriptContent = output.GetChildContentAsync().Result.GetContent();
        switch (Location)
        {
            case AspLocation.Head:
                HeadScripts.Add(scriptContent);
                break;
            case AspLocation.Footer:
                FooterScripts.Add(scriptContent);
                break;
        }

        output.SuppressOutput();
    }

    public static string RenderHeadScripts()
    {
        if (!HeadScripts.Any())
            return string.Empty;

        var scripts = string.Join("\n", HeadScripts);
        HeadScripts.Clear();
        return $"<script>\n{scripts}\n</script>";
    }

    public static string RenderFooterScripts()
    {
        if (!FooterScripts.Any())
            return string.Empty;

        var scripts = string.Join("\n", FooterScripts);
        FooterScripts.Clear();
        return $"<script>\n{scripts}\n</script>";
    }
}