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
    public static class LinkHelpers
    {
        // Quick and dirty pattch to have type safety
        private static string ActionWithValues<T>(this IUrlHelper helper, Expression<Action<T>> expression)
            where T : Controller
        {
            var method = expression.Body as MethodCallExpression;
            var arguments = method.Arguments;
            var parameters = method.Method.GetParameters();
            var values = Enumerable.Range(0, arguments.Count).ToDictionary(
                x => parameters[x].Name,
                x => Expression.Lambda(arguments[x]).Compile().DynamicInvoke());
            var actionMember = method.Method;
            var controllerType = actionMember.DeclaringType.GetTypeInfo();
            var actionName = actionMember.Name;
            var controllerName = controllerType.Name.EndsWith("Controller") ? controllerType.Name.Substring(0, controllerType.Name.Length - "Controller".Length) : controllerType.Name;
            return helper.Action(actionName, controllerName, values);
        }

        private static string ToRelString(this Rel relation)
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
                Description = description
            };
        }

        public static LinkRepresentation LinkSelf(this IUrlHelper helper, Rel relation = 0, string description = null)
        {
            relation = relation | Rel.Self;
            return new LinkRepresentation()
            {
                Href = helper.Action(),
                Rel = relation.ToRelString(),
                Description = description
            };
        }

        public static LinkRepresentation LinkHome(this IUrlHelper helper)
        {
            return helper.Link<HomeController>(x => x.GetRoot(), Rel.Home);
        }

    }

}