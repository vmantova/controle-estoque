using System.ComponentModel.DataAnnotations.Schema;

namespace Controle.Estoque.Models
{
    [NotMapped]
    public class Erro
    {
        public string Mensagem { get; set; }
    }
}
