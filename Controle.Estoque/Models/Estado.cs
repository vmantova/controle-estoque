using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Controle.Estoque.Models
{
    [Table("Estados")]
    public class Estado
    {
        [Key]
        public int Id { get; set; }

        [StringLength(2)]
        public string Sigla { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }
    }
}
