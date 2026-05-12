using System;
using System.Collections.Generic;
using System.Text;
using ToDoProject.DOMINIO.Enums;

namespace ToDoProject.DOMINIO.Modelo;

public class FiltroTarefa
{
    public string? Titulo { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? Prazo { get; set; }
    public Prioridade? Prioridade { get; set; }
    public bool? Concluido { get; set; }
}
