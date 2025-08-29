using Articly.Core.Domain.Entities;
using Model = Articly.Core.Domain.Model;
using AutoMapper;
using Articly.Core.Domain.Account;

namespace Articly.Core.Application.Mappings;

public class ModelToDomainMapping : Profile
{
    public ModelToDomainMapping()
    {
        CreateMap<Category, Model.Categories>().ReverseMap();
        CreateMap<Article, Model.Articles>().ReverseMap();
        CreateMap<User, Model.Users>().ReverseMap();
        CreateMap<UserToken, Model.UserTokens>().ReverseMap();
    }
}
