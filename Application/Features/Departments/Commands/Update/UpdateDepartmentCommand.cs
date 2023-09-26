using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;

namespace Application.Features.Departments.Commands.Update
{
    public class UpdateDepartmentCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }

        public int FacultyId { get; set; }

        public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<int>>
        {
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
            {
                _departmentRepository = departmentRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateDepartmentCommand command, CancellationToken cancellationToken)
            {
                var entity = await _departmentRepository.GetByIdAsync(command.Id);

                if (entity == null)
                {
                    return Result<int>.Fail($"Entity Not Found.");
                }
                else
                {
                    entity.DepartmentName = command.DepartmentName ?? entity.DepartmentName;
                    entity.DepartmentShortName = command.DepartmentShortName ?? entity.DepartmentShortName;
                    entity.FacultyId = command.FacultyId;
                    await _departmentRepository.UpdateAsync(entity);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(entity.Id);
                }
            }
        }
    }
}
