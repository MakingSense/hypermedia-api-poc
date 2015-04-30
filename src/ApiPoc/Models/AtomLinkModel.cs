using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.AspNet.Mvc;
using System.Linq.Expressions;
using System.Reflection;

namespace ApiPoc.Controllers
{
    [XmlType("link", Namespace = "http://www.w3.org/2005/Atom")]
    public class AtomLinkModel
    {
        [XmlAttribute("href")]
        public string Href { get; set; }

        [XmlAttribute("rel")]
        public string Rel { get; set; }
        
        public static AtomLinkModel AccountHome(string href)
        {
            return new AtomLinkModel() { Href = href, Rel = "ClientHome" };
        }

    }

    public static class LinkHelpers
    {
        // TODO: Remove magic strings and make it type safe
        public static AtomLinkModel LinkSelf(this IUrlHelper helper)
        {
            return new AtomLinkModel() { Href = helper.Action(), Rel = "self" };
        }

        public static AtomLinkModel LinkSelf<T>(this IUrlHelper helper, Expression<Action<T>> expression, object values = null)
            where T : Controller
        {
            return new AtomLinkModel() { Href = helper.Action(expression, values), Rel = "self" };
        }

        public static AtomLinkModel LinkParent<T>(this IUrlHelper helper, Expression<Action<T>> expression, object values = null)
            where T : Controller
        {
            return new AtomLinkModel() { Href = helper.Action(expression, values), Rel = "parent" };
        }

        public static AtomLinkModel LinkAccountResource(this IUrlHelper helper, int accountId)
        {
            return new AtomLinkModel() { Href = helper.Action<AccountsController>(x => x.GetItem(0), new { accountId = accountId }), Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/account-resource" };
        }

        public static AtomLinkModel LinkAccountCollection(this IUrlHelper helper)
        {
            return new AtomLinkModel() {
                Href = helper.Action<AccountsController>(x => x.GetCollection()),
                Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/account-collection" };
        }

        public static AtomLinkModel LinkAccountDetailedCollection(this IUrlHelper helper)
        {
            return new AtomLinkModel()
            {
                Href = helper.Action<AccountsController>(x => x.GetDetailedCollection()),
                Rel = "http://andresmoschini.github.io/hypermedia-api-poc/rels/account-detailed-collection"
            };
        }
    }

    public static class TypedUrlHelper
    {
        // Quick and dirty pattch to have type
        public static string Action<T>(this IUrlHelper helper, Expression<Action<T>> expression, object values = null)
            where T : Controller
        {
            var method = expression.Body as MethodCallExpression;
            var actionMember = method.Method;
            var controllerType = actionMember.DeclaringType.GetTypeInfo();
            var actionName = actionMember.Name;
            var controllerName = controllerType.Name.EndsWith("Controller") ? controllerType.Name.Substring(0, controllerType.Name.Length - "Controller".Length) : controllerType.Name;
            return helper.Action(actionName, controllerName, values); 
        }

    }
}