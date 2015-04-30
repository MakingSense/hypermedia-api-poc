using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiPoc.Controllers
{
    // TODO: move all of this logic to a better place
    public static class LinkHelpers
    {
        public static AtomLinkModel LinkSelf(this IUrlHelper helper)
        {
            return new AtomLinkModel() { Href = helper.Action(), Rel = "self" };
        }

        public static AtomLinkModel LinkSelf<T>(this IUrlHelper helper, Expression<Action<T>> expression)
            where T : Controller
        {
            return new AtomLinkModel() { Href = helper.ActionWithValues(expression), Rel = "self" };
        }

        public static AtomLinkModel LinkParent<T>(this IUrlHelper helper, Expression<Action<T>> expression)
            where T : Controller
        {
            return new AtomLinkModel() { Href = helper.ActionWithValues(expression), Rel = "parent" };
        }

        public static AtomLinkModel LinkAccountResource(this IUrlHelper helper, int accountId)
        {
            return new AtomLinkModel() { Href = helper.ActionWithValues<AccountsController>(x => x.GetItem(accountId)), Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/account-resource" };
        }

        public static AtomLinkModel LinkAccountCollection(this IUrlHelper helper)
        {
            return new AtomLinkModel()
            {
                Href = helper.ActionWithValues<AccountsController>(x => x.GetCollection()),
                Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/account-collection"
            };
        }

        public static AtomLinkModel LinkAccountDetailedCollection(this IUrlHelper helper)
        {
            return new AtomLinkModel()
            {
                Href = helper.ActionWithValues<AccountsController>(x => x.GetDetailedCollection()),
                Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/account-detailed-collection"
            };
        }

        public static AtomLinkModel LinkSubscriptorsCollection(this IUrlHelper helper, int accountId)
        {
            return new AtomLinkModel()
            {
                Href = helper.ActionWithValues<SubscriptorsController>(x => x.GetCollection(accountId)),
                Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/subscriptors-collection"
            };
        }

        public static AtomLinkModel LinkSubscriptorsDetailedCollection(this IUrlHelper helper, int accountId)
        {
            return new AtomLinkModel()
            {
                Href = helper.ActionWithValues<SubscriptorsController>(x => x.GetDetailedCollection(accountId)),
                Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/account-detailed-collection"
            };
        }

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

    }

}