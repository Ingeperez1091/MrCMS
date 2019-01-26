﻿using MrCMS.Web.Apps.Admin.Infrastructure.Breadcrumbs;

namespace MrCMS.Web.Apps.Admin.Breadcrumbs.System
{
    public class ImportExportWebpagesBreadcrumb : Breadcrumb<SystemBreadcrumb>
    {
        public override int Order => 2;
        public override string Name => "Import/Export Webpages";
        public override string Controller => "ImportExport";
        public override string Action => "Documents";
        public override bool IsNav => true;
    }
}