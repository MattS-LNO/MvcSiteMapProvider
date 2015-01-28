using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// IXmlSiteMapResultFactory interface
    /// </summary>
    public interface IXmlSiteMapResultFactory
    {
        ActionResult Create(SiteMapType type, int page);
        ActionResult Create(SiteMapType type, int page, string siteMapUrlTemplate);
        ActionResult Create(SiteMapType type, int page, IEnumerable<string> siteMapCacheKeys);
        ActionResult Create(SiteMapType type, int page, IEnumerable<string> siteMapCacheKeys, string siteMapUrlTemplate);
        ActionResult Create(SiteMapType type, int page, IEnumerable<string> siteMapCacheKeys, string baseUrl, string siteMapUrlTemplate);
        ActionResult Create(SiteMapType type, int page, ISiteMapNode rootNode);
        ActionResult Create(SiteMapType type, int page, ISiteMapNode rootNode, string siteMapUrlTemplate);
        ActionResult Create(SiteMapType type, int page, ISiteMapNode rootNode, string baseUrl, string siteMapUrlTemplate);

        [Obsolete("Overload is invalid for sitemaps with over 35,000 links. Use Create(int page) instead. This overload will be removed in version 5.")]
        ActionResult Create();
        [Obsolete("Overload is invalid for sitemaps with over 35,000 links. Use Create(int page, IEnumerable<string> siteMapCacheKeys) instead. This overload will be removed in version 5.")]
        ActionResult Create(IEnumerable<string> siteMapCacheKeys);
        [Obsolete("Overload is invalid for sitemaps with over 35,000 links. Use Create(int page, IEnumerable<string> siteMapCacheKeys, string baseUrl, string siteMapUrlTemplate) instead. This overload will be removed in version 5.")]
        ActionResult Create(IEnumerable<string> siteMapCacheKeys, string baseUrl, string siteMapUrlTemplate);
        [Obsolete("Overload is invalid for sitemaps with over 35,000 links. Use Create(int page, ISiteMapNode rootNode) instead. This overload will be removed in version 5.")]
        ActionResult Create(ISiteMapNode rootNode);
        [Obsolete("Overload is invalid for sitemaps with over 35,000 links. Use Create(int page, IEnumerable<string> siteMapCacheKeys) instead. This overload will be removed in version 5.")]
        ActionResult Create(ISiteMapNode rootNode, string baseUrl, string siteMapUrlTemplate);
    }
}
