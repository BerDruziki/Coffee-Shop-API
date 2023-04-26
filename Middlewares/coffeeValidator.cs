using client.Model;
using FluentValidation;
using FluentValidation.Results;

namespace Validator;
public class coffeeValidator : AbstractValidator <Coffee> 
{
    public coffeeValidator()
    {
    RuleFor(Coffee => Coffee.name).NotEmpty()
    .WithMessage("O nome deve ser preenchido.")
    .Length(3, 25)
    .WithMessage("O nome deve ter entre 3 e 25 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Coffee => Coffee.category).NotEmpty()
    .WithMessage("A categoria deve ser preenchida.");
    //--------------------------------------------------------------------------
    RuleFor(Coffee => Coffee.description).NotEmpty()
    .WithMessage("A descrição deve ser preenchida.")
    .Length(10, 140)
    .WithMessage("A descrição ter entre 10 e 140 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Coffee => Coffee.price).NotEmpty()
    .WithMessage("O preço deve ser preenchido.");
    //--------------------------------------------------------------------------
    RuleFor(Coffee => Coffee.image).NotEmpty()
    .WithMessage("Uma foto do produto dever ser adicionada.");
  }
}
