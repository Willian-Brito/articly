﻿using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Model;
using AutoMapper;

namespace Application;

public class ModelToDtoMapping : Profile
{
    public ModelToDtoMapping()
    {
        CreateMap<Categories, CategoryDTO>().ReverseMap();
        CreateMap<Categories, CategoryWithPathDTO>().ReverseMap();
        CreateMap<Articles, ArticleByCategoryDTO>().ReverseMap();
        CreateMap<Articles, ArticleDTO>().ReverseMap();
        CreateMap<Users, UserDTO>().ReverseMap();
        CreateMap<Stats, StatDTO>().ReverseMap();
    }
}
