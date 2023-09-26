using Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;

namespace Application.Features.Departments.Queries.GetAllCashed
{
    public class GetAllDepartmentsCachedQuery : IRequest<Result<List<GetAllDepartmentsCachedResponse>>>
    {
        public GetAllDepartmentsCachedQuery() { }
    }

    public class GetAllDepartmentsCachedQueryHandler : IRequestHandler<GetAllDepartmentsCachedQuery, Result<List<GetAllDepartmentsCachedResponse>>>
    {
        private readonly IDepartmentCacheRepository _departmentCache;
        private readonly IMapper _mapper;

        public GetAllDepartmentsCachedQueryHandler(IDepartmentCacheRepository departmentCache, IMapper mapper)
        {
            _departmentCache = departmentCache;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllDepartmentsCachedResponse>>> Handle(GetAllDepartmentsCachedQuery request, CancellationToken cancellationToken)
        {
            var entityList = await _departmentCache.GetCachedListAsync();
            var mappedEntities = _mapper.Map<List<GetAllDepartmentsCachedResponse>>(entityList);
            return Result<List<GetAllDepartmentsCachedResponse>>.Success(mappedEntities);
        }
    }
}
