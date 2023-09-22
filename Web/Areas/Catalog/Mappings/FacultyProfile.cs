using Application.Features.Faculties.Commands.Create;
using Application.Features.Faculties.Commands.Update;
using Application.Features.Faculties.Queries.GetAllCashed;
using Application.Features.Faculties.Queries.GetById;
using AutoMapper;
using Web.Areas.Catalog.Models;

namespace Web.Areas.Catalog.Mappings
{
    internal class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<GetAllFacultiesCachedResponse, FacultyViewModel>().ReverseMap();
            CreateMap<GetFacultyByIdResponse, FacultyViewModel>().ReverseMap();
            CreateMap<CreateFacultyCommand, FacultyViewModel>().ReverseMap();
            CreateMap<UpdateFacultyCommand, FacultyViewModel>().ReverseMap();
        }
    }
}
