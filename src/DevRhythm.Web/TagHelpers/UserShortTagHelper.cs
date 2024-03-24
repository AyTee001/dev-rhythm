using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using DevRhythm.App.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevRhythm.Web.TagHelpers
{
    [HtmlTargetElement("user-short-info")]
    public class UserInfoTagHelper : TagHelper
    {
        public UserShortDto? User { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (User is not null)
            {
                var container = new TagBuilder("div");
                container.AddCssClass("user-info d-flex align-items-start position-relative");

                var imageTag = new TagBuilder("img");
                imageTag.AddCssClass("rounded-circle");
                imageTag.Attributes.Add("src", User.ImageUrl ?? "/images/account-icon-user-icon-vector-graphics_292645-552.jpg");

                container.InnerHtml.AppendHtml(imageTag);

                var userShortInfoTag = new TagBuilder("div");
                userShortInfoTag.AddCssClass("d-flex flex-column user-info");

                userShortInfoTag.InnerHtml.AppendHtml($"<span>{User.UserName}</span>");
                userShortInfoTag.InnerHtml.AppendHtml($"<span>{User.FirstName} {User.LastName}</span>");

                var reputation = new TagBuilder("span");
                reputation.AddCssClass("reputation");

                userShortInfoTag.InnerHtml.AppendHtml(reputation);

                container.InnerHtml.AppendHtml(userShortInfoTag);

                output.Content.AppendHtml(container);
            }
        }
    }
}
