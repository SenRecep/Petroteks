using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Petroteks.MvcUi.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHalper : TagHelper
    {
        [HtmlAttributeName("Page-Size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("Page-Count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("Current-Category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("Currnt-Page")]
        public int CurrntPage { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='pagination'>");
            for (int i = 1; i <= PageCount; i++)
            {
                stringBuilder.AppendFormat("<li class='{0}'>", i == CurrntPage ? "active" : "");
                stringBuilder.AppendFormat("<a href='/Detail/CategoryDetail?page={0}&category={1}'>{2}</a></li>", i, CurrentCategory, i);
            }
            stringBuilder.Append("</ul>");
            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }
    }
}
