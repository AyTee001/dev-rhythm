using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using DevRhythm.App.DTOs;

namespace DevRhythm.Web.TagHelpers
{
    [HtmlTargetElement("user-short-info")]
    public class UserShortTagHelper : TagHelper
    {
        public UserShortDto? User { get; set; }
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (User is not null)
            {
                var html = new StringBuilder();
                html.Append("<div class=\"d-flex flex-column\">");
                html.Append($"<span>{User.UserName}</span>");
                html.Append($"<span>{User.FirstName} {User.LastName}</span>");
                html.Append("<span class=\"reputation\">");
                html.Append("<i class=\"fa-solid fa-trophy\"></i>");
                html.Append($"<span>{User.Reputation}</span>");
                html.Append("</span>");
                html.Append("</div>");

                output.Content.AppendHtml(html.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
