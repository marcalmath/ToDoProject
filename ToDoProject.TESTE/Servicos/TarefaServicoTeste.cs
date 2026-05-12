using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ToDoProject.DOMINIO.Interfaces;
using ToDoProject.TESTE.Configuração;
using Microsoft.Extensions.DependencyInjection;
using ToDoProject.TESTE.repositorioMock;
using ToDoProject.DOMINIO.Enums;
using ToDoProject.DOMINIO.Entidades;
using ToDoProject.DOMINIO.Servicos;
using Xunit.Sdk;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using System.Net.WebSockets;

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

    [Fact]
    public void Deve_retornar_uma_tarefa_quando_existir_uma_tarefa()
    {
        //arrange
        var banco = serviceProvider.GetService<BancoMock>();
        banco.Tarefas.Clear();
        var repositorio = serviceProvider.GetService<ITarefaRepositorio>();
        
        banco.Tarefas.Add(new Tarefa
        {
            Id = 1,
            Titulo = "Titulo Teste",
            Descricao = "Descrição Teste",
            DataCriacao = DateTime.Now,
            Prazo = new DateTime(2026,04,17),
            Prioridade = Prioridade.Alta,
            Concluido = false
        });

        //act
        var resultado = repositorio.ObterTodos();

        //assert
        Assert.Single(resultado);
        Assert.Equal("Titulo Teste", resultado[0].Titulo);
    }

    [Fact]
    public void Deve_retornar_multiplas_tarefas_quando_existirem()
    {
        //Arrange
        var banco = serviceProvider.GetService<BancoMock>();
        banco.Tarefas.Clear();
        var repositorio = serviceProvider.GetService<ITarefaRepositorio>();

        banco.Tarefas.Add(new Tarefa
        {
            Id = 1,
            Titulo = "Titulo Teste 1",
            Descricao = "Descrição Teste 1",
            DataCriacao = DateTime.Now,
            Prazo = new DateTime(2026,04,18),
            Prioridade = Prioridade.Baixa,
            Concluido = false
        });

        banco.Tarefas.Add(new Tarefa
        {
            Id = 2,
            Titulo = "Titulo Teste 2",
            Descricao = "Descrição Teste 2",
            DataCriacao = DateTime.Now,
            Prazo = new DateTime(2026,05,01),
            Prioridade = Prioridade.Media,
            Concluido = false
        });

        banco.Tarefas.Add(new Tarefa
        {
            Id = 3,
            Titulo = "Titulo Teste 3",
            Descricao = "Descrição Teste 3",
            DataCriacao = DateTime.Now,
            Prazo = new DateTime(2026,06,01),
            Prioridade = Prioridade.Alta,
            Concluido = true
        });

        //Act
        var resultado = repositorio.ObterTodos();

        //Assert
        Assert.Equal(3, resultado.Count);
    }
    
    [Fact]
    public void Deve_retornar_tarefa_quando_id_existir()
    {
        //Arrange
        var banco = serviceProvider.GetService<BancoMock>();
        var repositorio = serviceProvider.GetService<ITarefaRepositorio>();

        banco.Tarefas.Clear();

        banco.Tarefas.Add(new Tarefa
        {
            Id = 1,
            Titulo = "Titulo Teste",
            Descricao = "Descrição Teste",
            DataCriacao = DateTime.Now,
            Prazo = new DateTime(2026, 04, 21),
            Prioridade = Prioridade.Baixa,
            Concluido = false
        });

        //Act
        var resultado = repositorio.ObterPorId(1);

        //Assert
        Assert.NotNull(resultado);
        Assert.Equal(1, resultado.Id);
    }

    [Fact]
    public void Deve_lancar_excecao_quando_id_nao_existir()
    {
        //Arrange
        var banco = serviceProvider.GetService<BancoMock>();
        var repositorio = serviceProvider.GetService<ITarefaRepositorio>();

        banco.Tarefas.Clear();

        //Act & Assert
        Assert.Throws<KeyNotFoundException>(() => repositorio.ObterPorId(999));
    }

    [Fact]
    public void Deve_criar_tarefa_quando_dados_validos()
    {
        //Arrange
        var banco = serviceProvider.GetService<BancoMock>();
        var servico = serviceProvider.GetService<TarefaServico>();

        banco.Tarefas.Clear();

        var tarefa = new Tarefa
        {
            Id = 1,
            Titulo = "Nova tarefa",
            Descricao = "Descrição Teste",
            DataCriacao = DateTime.Now,
            Prazo = DateTime.Now.AddDays(5),
            Prioridade = Prioridade.Baixa,
            Concluido = false
        };

        //Act
        servico.Criar(tarefa);

        //Assert
        Assert.Single(banco.Tarefas);
    }

    [Fact]
    public void Deve_lancar_excecao_quando_titulo_for_vazio()
    {
        //Arrange
        var servico = serviceProvider.GetService<TarefaServico>();

        var tarefa = new Tarefa
        {
            Id = 1,
            Titulo = "", //titulo vazio deve falhar
            Descricao = "Descrição Teste",
            DataCriacao = DateTime.Now,
            Prazo = DateTime.Now.AddDays(5),
            Prioridade = Prioridade.Baixa,
            Concluido = false
        };
        //Act & Assert
        Assert.Throws<ValidationException>(() => servico.Criar(tarefa));
    }

    [Fact]
    public void Deve_lancar_excecao_quando_descricao_for_vazia()
    {
        //Arrange
        var servico = serviceProvider.GetService<TarefaServico>();

        var tarefa = new Tarefa
        {
            Id = 1,
            Titulo = "Titulo Teste",
            Descricao = "", //descrição vazia deve falhar
            DataCriacao = DateTime.Now,
            Prazo = DateTime.Now.AddDays(5),
            Prioridade = Prioridade.Baixa,
            Concluido = false
        };

        //Act & Assert
        Assert.Throws<ValidationException>(() => servico.Criar(tarefa));
    }

    [Fact]
    public void deve_atualizar_tarefa_quando_dados_forem_validos()
    {
        //arrange
        var banco = serviceProvider.GetService<BancoMock>();
        var servico = serviceProvider.GetService<TarefaServico>();
        var repositorio = serviceProvider.GetService<ITarefaRepositorio>();

        banco.Tarefas.Clear();

        var tarefa = new Tarefa
        {
            Id = 1,
            Titulo = "Titulo Teste",
            Descricao = "Descrição Teste",
            DataCriacao = DateTime.Now,
            Prazo = DateTime.Now.AddDays(5),
            Prioridade = Prioridade.Baixa,
            Concluido = false
        };

        servico.Criar(tarefa);

        var tarefaAtualizada = new Tarefa
        {
            Id = 1,
            Titulo = "Novo Titulo",
            Descricao = "Nova Descrição",
            DataCriacao = DateTime.Now,
            Prazo = DateTime.Now.AddDays(5),
            Prioridade = Prioridade.Alta,
            Concluido = false
        };

        //Act
        servico.Atualizar(tarefaAtualizada);
        var resultado = repositorio.ObterPorId(1);

        //Assert
        Assert.Equal("Novo Titulo", resultado.Titulo);

    }

    [Fact]
    public void Deve_remover_tarefa_quando_id_existir()
    {
        //arrange
        var banco = serviceProvider.GetService<BancoMock>();
        var servico = serviceProvider.GetService<TarefaServico>();

        banco.Tarefas.Clear();

        var tarefa = new Tarefa
        {
            Id = 1,
            Titulo = "Titulo Teste",
            Descricao = "Descrição Teste",
            DataCriacao = DateTime.Now,
            Prazo = DateTime.Now.AddDays(5),
            Prioridade = Prioridade.Baixa,
            Concluido = false
        };

        servico.Criar(tarefa);

        //Act
        servico.Remover(1);

        //Assert
        Assert.Empty(banco.Tarefas);
    }

    [Fact]
    public void deve_lancar_excecao_ao_remover_quando_id_nao_existir()
    {
        //Arrange
        var servico = serviceProvider.GetService<TarefaServico>();

        //Act & Assert
        Assert.Throws<KeyNotFoundException>(() => servico.Remover(999));
    }
}
