﻿using MrCMS.Apps;

namespace MrCMS.Web.Apps.Core
{
    public class MrCMSCoreApp : StandardMrCMSApp
    {
        public MrCMSCoreApp()
        {
            ContentPrefix = "/Apps/Core";
        }
        public override string Name => "Core";
        public override string Version => "1.0";
    }
}