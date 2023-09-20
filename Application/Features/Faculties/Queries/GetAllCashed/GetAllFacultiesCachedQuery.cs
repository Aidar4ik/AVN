using Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;

namespace Application.Features.Faculties.Queries.GetAllCashed
{
    public class GetAllFacultiesCachedQuery : IRequest<Result<List<GetAllFacultiesCachedResponse>>>
    {
        GetAllFacultiesCachedQuery() { }
    }

    public class GetAllFacultiesCachedQueryHandler : IRequestHandler<GetAllFacultiesCachedQuery, Result<List<GetAllFacultiesCachedResponse>>>
    {
        private readonly IFacultyCacheRepository _facultyCache;
        private readonly IMapper _mapper;

        public GetAllFacultiesCachedQueryHandler(IFacultyCacheRepository facultyCache, IMapper mapper)
        {
            _facultyCache = facultyCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllFacultiesCachedResponse>>> Handle(GetAllFacultiesCachedQuery request, CancellationToken cancellationToken)
        {
            var entityList = await _facultyCache.GetCachedListAsync();
            var mappedEntities = _mapper.Map<List<GetAllFacultiesCachedResponse>>(entityList);
            return Result<List<GetAllFacultiesCachedResponse>>.Success(mappedEntities);
        }
    }
}
