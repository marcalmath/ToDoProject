using Microsoft.Extensions.DependencyInjection;
using ToDoProject.DOMINIO.Interfaces;
using ToDoProject.DOMINIO.Servicos;
using ToDoProject.DOMINIO.Validadores;
using ToDoProject.TESTE.repositorioMock;

namespace ToDoProject.TESTE.Configuração;

public class TesteBase
{
    protected ServiceProvider serviceProvider;

    public TesteBase()
    {
        var services = new ServiceCollection();

        services.AddSingleton<BancoMock>();
        services.AddScoped<ITarefaRepositorio, TarefaRepositorioMock>();
        services.AddScoped<TarefaServico>();
        services.AddScoped<TarefaValidador>();

        serviceProvider = services.BuildServiceProvider();
    }
}