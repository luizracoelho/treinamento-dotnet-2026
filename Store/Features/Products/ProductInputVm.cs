using FluentValidation;

namespace Store.Features.Products;

public record ProductInputVm(string Name, decimal Price);

public class ProductInputVmValidator : AbstractValidator<ProductInputVm>
{
    public ProductInputVmValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O campo Nome é obrigatório.")
            .MinimumLength(3).WithMessage("O campo Nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O campo Nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("O campo Preço deve ser maior que zero.");
    }
}