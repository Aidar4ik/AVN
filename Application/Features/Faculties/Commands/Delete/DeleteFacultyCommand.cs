using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;

namespace Application.Features.Faculties.Commands.Delete
{
    public class DeleteFacultyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        public class DeleteFacultyCommandHandler : IRequestHandler<DeleteFacultyCommand, Result<int>>
        {
            private readonly IFacultyRepository _facultyRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteFacultyCommandHandler(IFacultyRepository facultyRepository, IUnitOfWork unitOfWork)
            {
                _facultyRepository = facultyRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(DeleteFacultyCommand command, CancellationToken cancellationToken)
            {
                var product = await _facultyRepository.GetByIdAsync(command.Id);
                await _facultyRepository.DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return Result<int>.Success(product.Id);
            }
        }
    }
}
