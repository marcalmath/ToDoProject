using Microsoft.Extensions.DependencyInjection;
using ToDoProject.DOMINIO.Interfaces;
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

        serviceProvider = services.BuildServiceProvider();
    }
}