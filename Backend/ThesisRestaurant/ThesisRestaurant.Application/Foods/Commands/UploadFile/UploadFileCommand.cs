using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ThesisRestaurant.Application.Foods.Commands.UploadFile
{
    public record UploadFileCommand(int id, IFormFile file) : IRequest<ErrorOr<Created>>;
}
