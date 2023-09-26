using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Queries.GetAllCashed;
using Application.Features.Departments.Queries.GetAllPaged;
using Application.Features.Departments.Queries.GetById;
using AutoMapper;
using Domain.Entities.Catalog;

namespace Application.Mappings
{
    internal class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentCommand, Department>().ReverseMap();
            CreateMap<GetDepartmentByIdResponse, Department>().ReverseMap();
            CreateMap<GetAllDepartmentsCachedResponse, Department>().ReverseMap();
            CreateMap<GetAllDepartmentsResponse, Department>().ReverseMap();
        }
    }
}
