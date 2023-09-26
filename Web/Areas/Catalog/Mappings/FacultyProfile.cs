using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Queries.GetAllCashed;
using Application.Features.Departments.Queries.GetById;
using AutoMapper;
using Web.Areas.Catalog.Models;

namespace Web.Areas.Catalog.Mappings
{
    internal class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<GetAllDepartmentsCachedResponse, DepartmentViewModel>().ReverseMap();
            CreateMap<GetDepartmentByIdResponse, DepartmentViewModel>().ReverseMap();
            CreateMap<CreateDepartmentCommand, DepartmentViewModel>().ReverseMap();
            CreateMap<UpdateDepartmentCommand, DepartmentViewModel>().ReverseMap();
        }
    }
}
