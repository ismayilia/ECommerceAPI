using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
	public class CreateProductValidator : AbstractValidator<VM_Create_Product>
	{
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Zəhmət olmasa məhsul adını boş qoymayın!")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Zehmet olmasa 5 ile 150 arasi herfli soz daxil edin.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Zəhmət olmasa stock dəyərini boş qoymayın!")
                .Must(s => s >= 0)
                    .WithMessage("Stock dəyəri negatif ola bilməz!");

			RuleFor(p => p.Price)
				.NotEmpty()
				.NotNull()
					.WithMessage("Zəhmət olmasa stock dəyərini boş qoymayın!")
				.Must(s => s >= 0)
					.WithMessage("Price negatif ola bilməz!");
		}
    }
}
