using System;
using System.Collections.Generic;
using System.Text;
using ToDoProject.DOMINIO.Entidades;

namespace ToDoProject.TESTE.repositorioMock
{
    public class BancoMock
    {
        public List<Tarefa> Tarefas { get; set; } = new();
    }
}
