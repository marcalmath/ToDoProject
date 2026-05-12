using System;
using System.Collections.Generic;
using System.Text;
using ToDoProject.DOMINIO.Entidades;
using ToDoProject.DOMINIO.Modelo;

namespace ToDoProject.DOMINIO.Interfaces;

public interface ITarefaRepositorio
{
    List<Tarefa> ObterTodos(FiltroTarefa? filtro);
    Tarefa ObterPorId(int id);
    void Criar(Tarefa tarefa);
    void Atualizar(Tarefa tarefa);
    void Remover(int id);
}
