using Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;

namespace Application.Features.Faculties.Queries.GetById
{
    public class GetFacultyByIdQuery : IRequest<Result<GetFacultyByIdResponse>>
    {
        public int Id { get; set; }

        public class GetFacultyByIdQueryHandler : IRequestHandler<GetFacultyByIdQuery, Result<GetFacultyByIdResponse>>
        {
            private readonly IFacultyCacheRepository _facultyCache;
            private readonly IMapper _mapper;

            public GetFacultyByIdQueryHandler(IFacultyCacheRepository facultyCache, IMapper mapper)
            {
                _facultyCache = facultyCache;
                _mapper = mapper;
            }

            public async Task<Result<GetFacultyByIdResponse>> Handle(GetFacultyByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _facultyCache.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetFacultyByIdResponse>(product);
                return Result<GetFacultyByIdResponse>.Success(mappedProduct);
            }
        }
    }
}
