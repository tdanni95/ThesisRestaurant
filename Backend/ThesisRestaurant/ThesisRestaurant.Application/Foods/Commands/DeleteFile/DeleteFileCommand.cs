using ErrorOr;
using MediatR;

namespace ThesisRestaurant.Application.Foods.Commands.DeleteFile
{
    public record DeleteFileCommand(int id):IRequest<ErrorOr<Deleted>>;
    
}
