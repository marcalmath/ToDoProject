using System;
using System.Collections.Generic;
using System.Text;
using ToDoProject.DOMINIO.Entidades;

namespace ToDoProject.DOMINIO.Interfaces;

public interface ITarefaRepositorio
{
    List<Tarefa> ObterTodos();
    Tarefa ObterPorId(int id);
    void Criar(Tarefa tarefa);
    void Atualizar(Tarefa tarefa);
    void Remover(int id);
}
