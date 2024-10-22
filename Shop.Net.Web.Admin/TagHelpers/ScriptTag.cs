using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Shop.Net.Web.Admin.TagHelpers
{
    [HtmlTargetElement("script", Attributes = "asp-location")]
    public class ScriptTagHelper : TagHelper
    {
        private static readonly List<string> HeadScripts = new();
        private static readonly List<string> FooterScripts = new();

        [HtmlAttributeName("asp-location")] public AspLocation? Location { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Location.HasValue) return;

            // Get the full script tag with its content
            var scriptContent = await output.GetChildContentAsync();
            var scriptTag = scriptContent.GetContent();
            switch (Location)
            {
                case AspLocation.Head:
                    HeadScripts.Add(scriptTag);
                    break;
                case AspLocation.Footer:
                    FooterScripts.Add(scriptTag);
                    break;
            }

            // Suppress the output so it doesn't render where it is defined
            output.SuppressOutput();
        }

        // Render each script as its own <script> tag
        public static string RenderHeadScripts()
        {
            if (!HeadScripts.Any())
                return string.Empty;

            var scripts = string.Join("\n", HeadScripts.Select(script => $"<script>{script}</script>"));
            HeadScripts.Clear();
            return scripts;
        }

        public static string RenderFooterScripts()
        {
            if (!FooterScripts.Any())
                return string.Empty;

            var scripts = string.Join("\n", FooterScripts.Select(script => $"<script>{script}</script>"));
            FooterScripts.Clear();
            return scripts;
        }
    }
}
