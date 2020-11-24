using ControleFluxoAPI.Domain.Models;
using ControleFluxoAPI.Persistence.Contexts;
using ControleFluxoAPI.Persistence.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFluxoAPI.Validators
{
    public class AgendamentoValidator : AbstractValidator<Agendamento>
    {
        public AgendamentoValidator()
        {
            ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;

            RuleFor(a => a.VagaId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("É necessário inserir uma vaga válida.");

            RuleFor(a => a.FornecedorId)
                .NotNull()
                .NotEqual(0)
                .WithMessage("É necessário inserir um fornecedor válido.");

            RuleFor(a => a.PlacaVeiculo)
                .NotNull()
                .Length(7)
                .WithMessage("A placa do veículo deve ter 7 caracteres");

            RuleFor(a => a.DataInicio)
                .NotNull();

            RuleFor(a => a.DataFim)
                .NotNull();

            //Validar se a data inicio é posterior a atual
            RuleFor(a => a.DataInicio)
                .GreaterThan(DateTime.Now)
                .WithMessage("É necessário inserir um período onde a data/hora de início é maior que a data/hora atual.");

            //Validar se a data fim é posterior a atual
            RuleFor(a => a.DataFim)
                .GreaterThan(DateTime.Now)
                .WithMessage("É necessário inserir um período onde a data/hora de fim é maior que a data/hora atual.");

            //Validar máximo e mínimo de tempo de permanência na vaga
            RuleFor(a => a.DataFim.Subtract(a.DataInicio).TotalMinutes)
                .InclusiveBetween(30,60)
                .WithMessage("É necessário inserir um período entre 30 minutos e 1 hora.");

            RuleFor(a => a.DataInicio.Hour)
                .InclusiveBetween(8, 11).When(a => a.DataInicio.Hour <= 12)
                .InclusiveBetween(14,17).When(a => a.DataInicio.Hour > 12)
                .WithMessage("A data de início do agendamento deve estar entre 8h e 12h ou 14h e 18h.");

            RuleFor(a => a.DataFim.Hour)
                .InclusiveBetween(8, 12).When(a => a.DataInicio.Hour <= 12)
                .InclusiveBetween(14, 18).When(a => a.DataInicio.Hour > 12)
                .DependentRules(() => {
                    RuleFor(a => a.DataFim.Minute)
                        .Equal(00)
                        .When(a => a.DataFim.Hour == 18 || a.DataFim.Hour == 12);
                })
                .WithMessage("A data de fim do agendamento deve estar entre 8h e 12h ou 14h e 18h.");

           
        }
    }
}
