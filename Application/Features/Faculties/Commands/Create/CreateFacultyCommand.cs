using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;

namespace Application.Features.Faculties.Commands.Create
{
    public partial class CreateFacultyCommand : IRequest<Result<int>>
    {
        public string FacultyName { get; set; }
        public string DeanName { get; set; }
        public string FacultyShortName { get; set; }
    }

    public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, Result<int>>
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }

        public CreateFacultyCommandHandler(IFacultyRepository facultyRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _facultyRepository = facultyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Faculty>(request);
                await _facultyRepository.InsertAsync(entity);
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
