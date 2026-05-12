using System;
using System.Collections.Generic;
using System.Text;
using ToDoProject.DOMINIO.Entidades;
using ToDoProject.DOMINIO.Interfaces;
using ToDoProject.DOMINIO.Validadores;
using FluentValidation;
using System.ComponentModel;

namespace ToDoProject.DOMINIO.Servicos;

public class TarefaServico(ITarefaRepositorio repositorio, TarefaValidador validador)
{
    public void Criar(Tarefa tarefa)
    {
        var resultado = validador.Validate(tarefa);

        if (!resultado.IsValid)
            throw new ValidationException(resultado.Errors);

        repositorio.Criar(tarefa);
    }

    public void Atualizar(Tarefa tarefa)
    {
        var resultado = validador.Validate(tarefa);

        if (!resultado.IsValid)
            throw new ValidationException(resultado.Errors);

        repositorio.Atualizar(tarefa);
    }

    public void Remover(int id)
    {
        repositorio.ObterPorId(id);
        repositorio.Remover(id);
    }
}
