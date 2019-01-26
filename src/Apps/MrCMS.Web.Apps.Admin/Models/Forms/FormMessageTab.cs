﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Admin.Infrastructure.Models.Tabs;

namespace MrCMS.Web.Apps.Admin.Models.Forms
{
    public class FormMessageTab : AdminTab<Form>
    {
        public override int Order
        {
            get { return 300; }
        }

        public override Type ParentType
        {
            get { return null; }
        }

        public override Type ModelType => typeof(FormMessageTabViewModel);

        public override string TabHtmlId
        {
            get { return "form-message-tab"; }
        }

        public override Task RenderTabPane(IHtmlHelper html, IMapper mapper, Form form)
        {
            return html.RenderPartialAsync("FormMessage", mapper.Map<FormMessageTabViewModel>(form));
        }

        public override string Name(IServiceProvider serviceProvider, Form entity)
        {
            return "Settings";
        }

        public override bool ShouldShow(IServiceProvider serviceProvider, Form entity)
        {
            return true;
        }
    }
}