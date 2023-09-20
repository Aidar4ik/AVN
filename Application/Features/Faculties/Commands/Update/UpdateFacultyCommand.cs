using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Faculties.Commands.Update
{
    public class UpdateFacultyCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public string DeanName { get; set; }
        public string FacultyShortName { get; set; }

        public class UpdateFacultyCommandHandler : IRequestHandler<UpdateFacultyCommand, Result<int>>
        {
            private readonly IFacultyRepository _facultyRepository;
            private readonly IUnitOfWork _unitOfWork;

            public UpdateFacultyCommandHandler(IFacultyRepository facultyRepository, IUnitOfWork unitOfWork)
            {
                _facultyRepository = facultyRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Result<int>> Handle(UpdateFacultyCommand command, CancellationToken cancellationToken)
            {
                var entity = await _facultyRepository.GetByIdAsync(command.Id);

                if (entity == null)
                {
                    return Result<int>.Fail($"Entity Not Found.");
                }
                else
                {
                    entity.FacultyName = command.FacultyName ?? entity.FacultyName;
                    entity.DeanName = command.DeanName ?? entity.DeanName;
                    entity.FacultyShortName = command.FacultyShortName ?? entity.FacultyShortName;
                    await _facultyRepository.UpdateAsync(entity);
                    await _unitOfWork.Commit(cancellationToken);
                    return Result<int>.Success(entity.Id);
                }
            }
        }
    }
}
