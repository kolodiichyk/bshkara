using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bshkara.Web.Extentions
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            var cssClass = "active";
            var currentAction = (string) html.ViewContext.RouteData.Values["action"];
            var currentController = (string) html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            return controller.ToLower().Contains(currentController.ToLower()) &&
                   action.ToLower() == currentAction.ToLower()
                ? cssClass
                : string.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            var currentAction = (string) html.ViewContext.RouteData.Values["action"];
            return currentAction.ToLower();
        }

        public static HtmlString ValidationSummaryBootstrap(this HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            if (htmlHelper.ViewData.ModelState.IsValid)
            {
                return new HtmlString(string.Empty);
            }

            return
                new HtmlString(
                    $@"<div class='alert alert-danger' >
                                       <button class='close' data-close='alert'></button>
                                       <span> {
                        htmlHelper.ValidationSummary(false)
                        }</span>
                                    </div>");
        }

        public static MvcHtmlString HiddenForDateTime(this HtmlHelper html, string target, DateTime? dateTime)
        {
            return
                new MvcHtmlString(
                    $"<input data-val=\"true\" data-val-date=\"The field CreatedOn must be a date.\" data-val-required=\"The CreatedOn field is required.\" id=\"CreatedOn\" name=\"{target}\" type=\"hidden\" value=\"{dateTime?.ToString(CultureInfo.CurrentCulture)}\">");
        }

        /*
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var values = Enum.GetValues(typeof (TEnum)).Cast<TEnum>();

            var items =
                values.Select(
                    value =>
                        new SelectListItem
                        {
                            Text = GetEnumDescription(value),
                            Value = value.ToString(),
                            Selected = value.Equals(metadata.Model)
                        });
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            return htmlHelper.DropDownListFor(expression, items, attributes);
        }

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }*/
    }
}