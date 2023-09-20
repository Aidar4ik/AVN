using Application.Features.Faculties.Commands.Create;
using Application.Features.Faculties.Queries.GetAllCashed;
using Application.Features.Faculties.Queries.GetById;
using AutoMapper;
using Domain.Entities.Catalog;

namespace Application.Mappings
{
    internal class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<CreateFacultyCommand, Faculty>().ReverseMap();
            CreateMap<GetFacultyByIdResponse, Faculty>().ReverseMap();
            CreateMap<GetAllFacultiesCachedResponse, Faculty>().ReverseMap();
        }
    }
}
