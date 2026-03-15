using ToDoProject.DOMINIO.Enums;

namespace ToDoProject.DOMINIO.Entidades
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime Prazo { get; set; }
        public Prioridade Prioridade { get; set; }
        public bool Concluido { get; set; }
    }
}
