namespace ShopingList.Web.Helpers
{
    using System.Text;
    using System.Web.Mvc;

    public static class HtmlHelperExtension
    {
        public static MvcHtmlString AlertBox(this HtmlHelper helper, string id)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine($"<div id='{id}' class='alert alert-dismissible' style='display: none'  role='alert'>");
            html.AppendLine("<span id='message'><span>");
            html.AppendLine("</div>");
            return new MvcHtmlString(html.ToString());
        }
    }
}