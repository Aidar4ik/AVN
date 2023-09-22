using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
        }
    }
}
