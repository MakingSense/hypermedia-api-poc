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
        private static string ActionWithValues<T>(this IUrlHelper helper, Expression<Action<T>> expression)
            where T : Controller
        {
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
            return helper.Action(actionName, controllerName, values).Replace("%7B", "{").Replace("%7D", "}");
        }

        public static string ToRelString(this Rel relation)
        {
            //UglyPatch
            return relation == 0
                ? null
                : string.Join("-", Regex.Split(relation.ToString().Replace(",", ""), "(?<=[a-z])(?=[A-Z])")).ToLower();
        }

        public static LinkRepresentation Link<T>(this IUrlHelper helper, Expression<Action<T>> expression, Rel relation, string description = null)
            where T : Controller
        {
            return new LinkRepresentation()
            {
                Href = helper.ActionWithValues<T>(expression),
                Rel = relation.ToRelString(),
                Description = description ?? relation.ToString(),
                Safe = (relation & Rel.Unsafe) == 0
            };
        }

        public static LinkRepresentation LinkSelf(this IUrlHelper helper, Rel relation = Rel.None, string description = null)
        {
            relation |= Rel.Self;
            return new LinkRepresentation()
            {
                Href = helper.Action(),
                Rel = relation.ToRelString(),
                Description = description ?? "Self",
                Safe = (relation & Rel.Unsafe) == 0
            };
        }

        public static LinkRepresentation LinkHome(this IUrlHelper helper, Rel relation = Rel.None, string description = null)
        {
            relation |= Rel.Home;
            return helper.Link<HomeController>(x => x.Index(), relation, description ?? "Home");
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
