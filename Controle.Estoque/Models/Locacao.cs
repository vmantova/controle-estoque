using System;   
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Controle.Estoque.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public DateTime DataLocacao { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]

        public DateTime DataDevolucao { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]

        public decimal ValorTotal { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]

        public int ClienteId { get;set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]

        public int Quantidade { get; set; }
        [NotMapped]
        public virtual Cliente Cliente { get; set; }
        [NotMapped]
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public int ProdutoId { get; set; }
        public string Status { get; set; }
    }
}
