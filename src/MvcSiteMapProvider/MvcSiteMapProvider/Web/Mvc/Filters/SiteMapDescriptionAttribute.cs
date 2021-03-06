﻿using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MvcSiteMapProvider.Web.Mvc.Filters
{
    /// <summary>
    /// SiteMapDescription attribute
    /// Can be used for overriding sitemap description on an action method on a per request basis.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SiteMapDescriptionAttribute
        : ActionFilterAttribute
    {
        /// <summary>
        /// Property name of ViewData to look in
        /// </summary>
        protected readonly string PropertyName;

        /// <summary>
        /// Cache key for the sitemap instance this attribute applies to.
        /// If not supplied, the default SiteMap instance for this request will be used.
        /// </summary>
        public string SiteMapCacheKey { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public AttributeTarget Target { get; set; }

        /// <summary>
        /// Creates a new SiteMapDescriptionAttribute instance.
        /// </summary>
        /// <param name="propertyName">Property in ViewData used as the SiteMap.CurrentNode.Description</param>
        public SiteMapDescriptionAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
            this.Target = AttributeTarget.CurrentNode;
        }

        /// <summary>
        /// Called by the MVC framework after the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult result = filterContext.Result as ViewResult;
            if (result != null)
            {
                var target = (ResolveTarget(result.ViewData.Model, PropertyName) ??
                              ResolveTarget(result.ViewData, PropertyName)) ??
                             result.ViewData[PropertyName];

                if (target != null)
                {
                    ISiteMap siteMap = SiteMaps.GetSiteMap(this.SiteMapCacheKey); 
                    if (siteMap != null && siteMap.CurrentNode != null)
                    {
                        if (Target == AttributeTarget.ParentNode && siteMap.CurrentNode.ParentNode != null)
                        {
                            siteMap.CurrentNode.ParentNode.Description = target.ToString();
                        }
                        else
                        {
                            siteMap.CurrentNode.Description = target.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Resolve target
        /// </summary>
        /// <param name="target">Target object</param>
        /// <param name="expression">Target expression</param>
        /// <returns>
        /// A target represented as a <see cref="object"/> instance 
        /// </returns>
        internal static object ResolveTarget(object target, string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return target;
            }

            try
            {
                var parameter = Expression.Parameter(target.GetType(), "target");
                var lambdaExpression = MvcSiteMapProvider.Linq.DynamicExpression.ParseLambda(new[] { parameter }, null, "target." + expression);
                return lambdaExpression.Compile().DynamicInvoke(target);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

}