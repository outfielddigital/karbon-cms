using System;
using System.Linq;
using System.Web.Mvc;
using Karbon.Cms.Core;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web
{
    internal static class ContentExtensions
    {
        /// <summary>
        /// Gets the controller for the given content model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="defaultControllerName">Default name of the controller.</param>
        /// <returns></returns>
        public static Type GetController(this IContent model,
            string defaultControllerName = "")
        {
            var contentAttr = model.GetType().GetCustomAttribute<ContentAttribute>();
            var controllerName = (contentAttr != null && contentAttr.ControllerType != null)
                ? contentAttr.ControllerType.Name
                : string.Format("{0}Controller", model.TypeName);

            var controllers = TypeFinder.FindTypes<IController>().ToList();
            var controller = controllers.SingleOrDefault(x => x.Name == controllerName);

            if (controller != null)
                return controller;

            return !string.IsNullOrEmpty(defaultControllerName)
                ? controllers.SingleOrDefault(x => x.Name == defaultControllerName)
                : null;
        }
    }
}
