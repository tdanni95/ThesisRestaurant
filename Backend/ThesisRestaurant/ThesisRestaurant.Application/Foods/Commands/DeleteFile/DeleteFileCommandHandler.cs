using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;

namespace ThesisRestaurant.Application.Foods.Commands.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, ErrorOr<Deleted>>
    {
        private readonly IFoodRepository _foodRepository;
        public DeleteFileCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public async Task<ErrorOr<Deleted>> Handle(DeleteFileCommand request, CancellationToken cancellationToken) => await _foodRepository.DeleteFile(request.id);
    }
}
