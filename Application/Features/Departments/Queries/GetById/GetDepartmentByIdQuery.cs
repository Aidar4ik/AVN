using Application.Interfaces.CacheRepositories;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;

namespace Application.Features.Departments.Queries.GetById
{
    public class GetDepartmentByIdQuery : IRequest<Result<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }

        public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<GetDepartmentByIdResponse>>
        {
            private readonly IDepartmentCacheRepository _fepartmentCache;
            private readonly IMapper _mapper;

            public GetDepartmentByIdQueryHandler(IDepartmentCacheRepository fepartmentCache, IMapper mapper)
            {
                _fepartmentCache = fepartmentCache;
                _mapper = mapper;
            }

            public async Task<Result<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _fepartmentCache.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetDepartmentByIdResponse>(product);
                return Result<GetDepartmentByIdResponse>.Success(mappedProduct);
            }
        }
    }
}
