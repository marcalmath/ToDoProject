using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ToDoProject.DOMINIO.Interfaces;
using ToDoProject.DOMINIO.Entidades;
using Xunit.Sdk;
using ToDoProject.DOMINIO.Modelo;

namespace ToDoProject.TESTE.repositorioMock;

public class TarefaRepositorioMock(BancoMock banco) : ITarefaRepositorio
{
    public List<Tarefa> ObterTodos(FiltroTarefa? filtro)
    {
        return banco.Tarefas;
    }

    public Tarefa ObterPorId(int id)
    {
        var tarefa = banco.Tarefas.FirstOrDefault(p => p.Id == id) ??
            throw new KeyNotFoundException("Tarefa não encontrada");

        return tarefa;
    }
    public void Criar(Tarefa tarefa)
    {
        banco.Tarefas.Add(tarefa);
    }

    public void Atualizar(Tarefa tarefa)
    {
        var index = banco.Tarefas.FindIndex(t => t.Id == tarefa.Id);

        if (index == -1)
            throw new KeyNotFoundException("Tarefa não encontrada para atualizar.");

        banco.Tarefas[index] = tarefa;
    }

    public void Remover(int id)
    {
        var index = banco.Tarefas.FindIndex(t => t.Id == id);
        banco.Tarefas.RemoveAt(index);
    }
}