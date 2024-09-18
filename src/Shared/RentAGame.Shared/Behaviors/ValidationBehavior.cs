using FluentValidation;
using MediatR;
using RentAGame.Shared.Mediator;

namespace RentAGame.Shared.Behaviors;

/*
    MediatR hattında araya girip, kendisine doğrulama kuralı yüklenmiş tüm nesneleri aşağıdaki
    davranış türevi ile ele alabiliriz.

    DI'a yüklenen tüm validator'lar bu davranışa constructor üzerinden enjekte edilebilirler.

    Handle fonksiyonu bir sonraki operasyona geçmeden önce ICommand<TResponse> türevli talepleri ele alır.
    Tahmin edileceği üzere bir Command(komut) talebi geldiğinde bu davranış TReqeust'a ait validator'ları çalıştırır.
 */
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> // Bu sayede MediatR'a bir davranış olarak ekleyebileceğiz
    where TRequest : ICommand<TResponse> // Sadece ICommand türünden olan nesnelerle çalışır
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validations = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var errors = validations.Where(v => v.Errors.Count != 0).SelectMany(v => v.Errors).ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
