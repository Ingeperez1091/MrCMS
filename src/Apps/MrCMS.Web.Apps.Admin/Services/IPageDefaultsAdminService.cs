using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Web.Apps.Admin.Models;

namespace MrCMS.Web.Apps.Admin.Services
{
    public interface IPageDefaultsAdminService
    {
        List<PageDefaultsInfo> GetAll();
        List<SelectListItem> GetUrlGeneratorOptions(Type type);
        List<SelectListItem> GetLayoutOptions();
        DefaultsInfo GetInfo(Type type);
        void SetDefaults(DefaultsInfo info);

        void EnableCache(string typeName);
        void DisableCache(string typeName);
    }
}