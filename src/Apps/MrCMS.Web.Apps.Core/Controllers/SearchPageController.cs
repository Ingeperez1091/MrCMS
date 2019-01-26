﻿using Microsoft.AspNetCore.Mvc;
using MrCMS.Attributes;
using MrCMS.Web.Apps.Core.ModelBinders;
using MrCMS.Web.Apps.Core.Models.Search;
using MrCMS.Web.Apps.Core.Pages;
using MrCMS.Web.Apps.Core.Services.Search;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Core.Controllers
{
    public class SearchPageController : MrCMSAppUIController<MrCMSCoreApp>
    {
        private readonly IWebpageSearchService _webpageSearchService;

        public SearchPageController(IWebpageSearchService webpageSearchService)
        {
            _webpageSearchService = webpageSearchService;
        }
        [CanonicalLinks]
        public ActionResult Show(SearchPage page, [ModelBinder(typeof(WebpageSearchQueryModelBinder))]WebpageSearchQuery model)
        {
            ViewData["webpageSearchQueryModel"] = model;

            ViewData["searchResults"] = _webpageSearchService.Search(model);
            return View(page);
        }
    }
}
