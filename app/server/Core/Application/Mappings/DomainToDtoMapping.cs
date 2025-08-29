using Articly.Core.Application.DTOs;
using Articly.Core.Domain.Entities;
using AutoMapper;

namespace Articly.Core.Application.Mappings;

public class DomainToDtoMapping : Profile
{
    public DomainToDtoMapping()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryWithPathDTO>().ReverseMap();
        CreateMap<Article, ArticleDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
