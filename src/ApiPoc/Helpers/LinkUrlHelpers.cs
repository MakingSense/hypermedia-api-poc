using ApiPoc.Controllers;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ApiPoc.Helpers
{
    public static class LinkUrlHelpers
    {
        // Quick and dirty patch to have type safety
        private static Link ActionWithValues<T>(this IUrlHelper helper, Expression<Action<T>> expression)
            where T : Controller
        {
            var rel = Rel._None;
            var method = expression.Body as MethodCallExpression;
            var arguments = method.Arguments;
            var parameters = method.Method.GetParameters();
            var values = Enumerable.Range(0, arguments.Count).ToDictionary(
                x => parameters[x].Name,
                x =>
                {
                    var argument = arguments[x];
                    var unaryExpression = argument as UnaryExpression;

                    if (unaryExpression != null && unaryExpression.Operand.Type.GetTypeInfo().IsSubclassOf(typeof(TemplateParameter)))
                    {
                        var templateParameter = Expression.Lambda(unaryExpression.Operand).Compile().DynamicInvoke() as TemplateParameter;
                        rel |= Rel.Template;
                        return string.Format("{{{0}}}", templateParameter.CustomText ?? parameters[x].Name);
                    }
                    else
                    {
                        return Expression.Lambda(argument).Compile().DynamicInvoke();
                    }
                });
            var actionMember = method.Method;
            var controllerType = actionMember.DeclaringType.GetTypeInfo();
            var actionName = actionMember.Name;
            var controllerName = controllerType.Name.EndsWith("Controller") ? controllerType.Name.Substring(0, controllerType.Name.Length - "Controller".Length) : controllerType.Name;
            var href = helper.Action(actionName, controllerName, values).Replace("%7B", "{").Replace("%7D", "}");

            var linkDescriptionAttributes = actionMember.GetCustomAttributes(typeof(LinkDescriptionAttribute), true).Cast<LinkDescriptionAttribute>();
            foreach (var attr in linkDescriptionAttributes)
            {
                rel |= attr.Rel;
            }
            var descriptions = linkDescriptionAttributes.Select(x => x.Description).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var description = descriptions.Length > 0 ? string.Join("; ", descriptions) : null;


            return new Link()
            {
                Href = href,
                Rel = rel,
                Description = description
            };
        }

        public static string ToRelString(this Rel relation)
        {
            //UglyPatch
            return relation == Rel._None
                ? null
                : string.Join("-", Regex.Split(relation.ToString().Replace(",", ""), "(?<=[a-z])(?=[A-Z])")).ToLower();
        }

        public static Link Link<T>(this IUrlHelper helper, Expression<Action<T>> expression, Rel relation = Rel._None, string description = null)
            where T : Controller
        {
            var link = helper.ActionWithValues<T>(expression);
            if (description != null)
            {
                link.Description = description;
            }
            link.Rel |= relation;
            return link;
        }

        public static Link LinkSelf<T>(this IUrlHelper helper, Expression<Action<T>> expression, Rel relation = Rel._None, string description = null)
            where T : Controller
        {
            return Link(helper, expression, relation | Rel.Self, description);
        }

        public static Link LinkHome(this IUrlHelper helper, Rel relation = Rel._None, string description = null)
        {
            return helper.Link<HomeController>(x => x.Index(), relation);
        }
    }

    public class TemplateParameter
    {
        public string CustomText { get; private set; }
        public static TemplateParameter<T> Create<T>()
        {
            return new TemplateParameter<T>();
        }

        public static TemplateParameter<T> Create<T>(string customText)
        {
            return new TemplateParameter<T>() { CustomText = customText };
        }
    }

    public class TemplateParameter<T> : TemplateParameter
    {
        public static implicit operator T(TemplateParameter<T> parameter)
        {
            return default(T);
        }
    }
}
