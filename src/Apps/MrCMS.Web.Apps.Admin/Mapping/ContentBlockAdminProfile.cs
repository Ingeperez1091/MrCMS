﻿using AutoMapper;
using MrCMS.Entities.Documents;
using MrCMS.Web.Apps.Admin.Models.ContentBlocks;

namespace MrCMS.Web.Apps.Admin.Mapping
{
    public class ContentBlockAdminProfile : Profile
    {
        public ContentBlockAdminProfile()
        {
            CreateMap<ContentBlock, AddContentBlockViewModel>().ReverseMap()
                .MapEntityLookup(model => model.WebpageId, block => block.Webpage);
            CreateMap<ContentBlock, UpdateContentBlockViewModel>().ReverseMap();
        }
    }
}