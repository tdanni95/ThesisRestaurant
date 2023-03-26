using ErrorOr;
using MediatR;
using ThesisRestaurant.Application.Common.Interfaces.Persistence;
using ThesisRestaurant.Domain.Foods.FoodPictures;

namespace ThesisRestaurant.Application.Foods.Commands.UploadFile
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, ErrorOr<Created>>
    {
        private readonly IFoodRepository _foodRepository;
        public UploadFileCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public async Task<ErrorOr<Created>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var file = request.file;

            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", Path.GetRandomFileName());
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", request.id.ToString());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var newFileName = Path.GetRandomFileName() + file.FileName;
            var filePath = Path.Combine(directoryPath, newFileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream); ;
            }
            var dbSrc = $"uploads/{request.id}/{newFileName}";
            var fp = new FoodPicture();
            fp.Src = dbSrc;
            var result = await _foodRepository.UploadFile(request.id, fp);
            return result;
        }
    }
}
