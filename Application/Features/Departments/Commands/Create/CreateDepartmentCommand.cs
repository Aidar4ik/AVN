using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;

namespace Application.Features.Departments.Commands.Create
{
    public partial class CreateDepartmentCommand : IRequest<Result<int>>
    {
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }

        public int FacultyId { get; set; }
    }

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Result<int>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Department>(request);
                await _departmentRepository.InsertAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(entity.Id);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
