using client.Model;
using FluentValidation;
using FluentValidation.Results;

namespace Validator;
public class clientValidator : AbstractValidator <Client> 
{
public clientValidator()
  {
    RuleFor(Client => Client.name).NotEmpty()
    .WithMessage("O nome do cliente deve ser preenchido.")
    .Length(3, 100)
    .WithMessage("O nome do cliente deve ter entre 3 e 100 caracteres.");
    //---------------------------------------------------------------------------
    RuleFor(Client => Client.phone).NotEmpty()
    .WithMessage("O número de telefone deve ser preenchido.")
    .Length(11)
    .WithMessage("O número de telefone deve ter 11 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Client => Client.email).NotEmpty()
    .WithMessage("O email deve ser preenchido.")
    .EmailAddress()//Obriga que o Email tenha um "@" e um ".com".
    .WithMessage("O email deve ser válido.")
    .Length(10, 70)
    .WithMessage("O email deve ter entre 10 e 70 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Client => Client.password).NotEmpty()
    .WithMessage("A senha deve ser preenchida.")
    .Length(8, 60)
    .WithMessage("A senha deve ter entre 8 e 60 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Client => Client.cep).NotNull()
    .WithMessage("O cep deve ser preenchido.")
    .Length(8)
    .WithMessage("O cep deve ter 8 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Client => Client.state).NotEmpty()
    .WithMessage("O estado deve ser preenchido.")
    .Length(2)
    .WithMessage("O estado deve ter entre 2 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Client => Client.city).NotEmpty()
    .WithMessage("A cidade deve ser preenchida.")
    .Length(5, 50)
    .WithMessage("A cidade deve ter entre 5 e 50 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Client => Client.district).NotEmpty()
    .WithMessage("O bairro deve ser preenchido.")
    .Length(5, 60)
    .WithMessage("O Bairro deve ter entre 5 e 60 caracteres.");
    //--------------------------------------------------------------------------
    RuleFor(Client => Client.street).NotEmpty()
    .WithMessage("A rua deve ser preenchida.")
    .Length(5, 70)
    .WithMessage("A rua deve ter entre 5 e 70 caracteres.");
  }
}