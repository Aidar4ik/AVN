using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;

namespace Application.Features.Departments.Commands.Delete
{
    public class DeleteDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result<int>>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            {
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteDepartmentCommand command, CancellationToken cancellationToken)
            {
                var entity = await _departmentRepository.GetByIdAsync(command.Id);
                await _departmentRepository.DeleteAsync(entity);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(entity.Id);
            }
        }
    }
}
