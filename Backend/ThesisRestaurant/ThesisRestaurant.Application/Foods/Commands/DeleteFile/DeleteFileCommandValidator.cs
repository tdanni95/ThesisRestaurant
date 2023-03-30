using FluentValidation;

namespace ThesisRestaurant.Application.Foods.Commands.DeleteFile
{
    public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
    {
        public DeleteFileCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().GreaterThan(0);
        }
    }
}
