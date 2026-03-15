using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ToDoProject.DOMINIO.Interfaces;
using ToDoProject.TESTE.Configuração;
using Microsoft.Extensions.DependencyInjection;
using ToDoProject.TESTE.repositorioMock;

namespace ToDoProject.TESTE.Servicos;

public class TarefaServicoTeste : TesteBase
{
    [Fact]
    public void Deve_retornar_lista_vazia_quando_nao_existir_tarefas()
    {
        //arrange
        var repositorio = serviceProvider.GetService<ITarefaRepositorio>();

        //act
        var resultado = repositorio.ObterTodos();

        //assert
        Assert.Empty(resultado);
    }
}
