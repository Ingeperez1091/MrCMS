﻿using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Batching.Entities;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Admin.Services.Batching;

namespace MrCMS.Web.Apps.Admin.Helpers
{
    public static class BatchExtensions
    {
        public static int GetBatchItemCount(this IHtmlHelper helper, Batch batch)
        {
            return helper.GetRequiredService<IGetBatchItemCount>().Get(batch);
        }

        public static BatchStatus GetBatchStatus(this IHtmlHelper helper, Batch batch)
        {
            return helper.GetRequiredService<IGetBatchStatus>().Get(batch);
        }
    }
}