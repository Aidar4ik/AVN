using Application.Extensions;
using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using Domain.Entities.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Departments.Queries.GetAllPaged
{
    public class GetAllDepartmentsQuery : IRequest<PaginatedResult<GetAllDepartmentsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetAllDepartmentsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    public class GGetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, PaginatedResult<GetAllDepartmentsResponse>>
    {
        private readonly IDepartmentRepository _repository;

        public GGetAllDepartmentsQueryHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<GetAllDepartmentsResponse>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Department, GetAllDepartmentsResponse>> expression = e => new GetAllDepartmentsResponse
            {
                Id = e.Id,
                DepartmentName = e.DepartmentName,
                DepartmentShortName = e.DepartmentShortName,
            };
            var paginatedList = await _repository.Departments
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }
}
