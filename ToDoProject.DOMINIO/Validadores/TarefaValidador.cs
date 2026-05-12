using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ToDoProject.DOMINIO.Entidades;

namespace ToDoProject.DOMINIO.Validadores
{
    public class TarefaValidador : AbstractValidator<Tarefa>
    {
        public TarefaValidador()
        {
            RuleFor(t => t.Titulo)
                .NotEmpty()
                .WithMessage("O Titulo é obrigatório.");

            RuleFor(t => t.Descricao)
                .NotEmpty()
                .WithMessage("A Descrição é obrigatória.");

            RuleFor(t => t.Prazo)
                .GreaterThan(DateTime.Now)
                .WithMessage("O Prazo deve ser maior que a data atual.");
        }
    }
}
