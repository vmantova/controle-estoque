using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Controle.Estoque.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }
        public string Pedido { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        [DataType(DataType.Currency)]
        [Range(1, 1000000)]
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public int UnidadeId { get; set; }
        [NotMapped]
        public Unidade UM { get; set; }
        public DateTime DataVenda { get; set; }
        public string Observacao { get; set; }
        public string StatusVenda { get; set; } //A - Venda Ativa, C - Venda Cancelada
    }
}
