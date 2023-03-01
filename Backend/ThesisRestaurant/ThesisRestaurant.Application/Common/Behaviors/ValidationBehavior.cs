using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisRestaurant.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse: IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;
        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid)
            {
                return await next();
            }

            var errors = validationResult.Errors
                .ConvertAll(validationFailure => Error.Validation(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage
                    ));
            //nincs implicit conversion
            //dynamic runtime nézi, hogy lehetséges-e a konvertálás
            //https://www.youtube.com/watch?v=FXP3PQ03fa0&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=8
            //ebben mutatja, hogy lehet szebben megoldani
            return (dynamic)errors;
        }
    }
}
