using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.Api.ViewModels;
using ControleFinanceiro.Bll.Models;
using FluentValidation;

namespace ControleFinanceiro.Api.Validation
{
    public class FuncaoViewModelValidator:AbstractValidator<FuncaoViewModel>
    {
        public FuncaoViewModelValidator()
        {
            RuleFor(f => f.Name)
                .NotNull().WithMessage("Preencha a função")
                .NotEmpty().WithMessage("Preencha a função")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(30).WithMessage("Use mais caracteres");

            RuleFor(f => f.Descricao)
                .NotNull().WithMessage("Preencha a descrição")
                .NotEmpty().WithMessage("Preencha a descrição")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use mais caracteres");
        }
    }
}
