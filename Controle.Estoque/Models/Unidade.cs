using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controle.Estoque.Models
{
    public class Unidade
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string Status { get; set; }
    }
}
