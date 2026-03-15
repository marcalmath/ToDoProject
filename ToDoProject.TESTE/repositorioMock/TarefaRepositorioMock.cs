using System;
using System.Collections.Generic;
using System.Text;
using ToDoProject.DOMINIO.Interfaces;
using ToDoProject.DOMINIO.Entidades;

namespace ToDoProject.TESTE.repositorioMock
{
    public class TarefaRepositorioMock : ITarefaRepositorio
    {
        private readonly BancoMock _banco;

        public TarefaRepositorioMock(BancoMock banco)
        {
            _banco = banco;
        }

        public List<Tarefa> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public Tarefa ObterPorId(int id)
        {
            throw new NotImplementedException();
        }
        public void Criar(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Tarefa tarefa)
        {
            throw new NotImplementedException();
        }

        public void Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}