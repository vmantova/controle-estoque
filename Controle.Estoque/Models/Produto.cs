using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle.Estoque.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Descrição do produto é obrigatório")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Obrigatório selecionar a categoria do produto")]
        public int CategoriaId { get; set; }

        [NotMapped]
        public Categoria Categoria { get; set; }
        //[Required(ErrorMessage = "Preço unitário deve ser informado")]7
        public decimal Preco { get; set; }
       // [Required(ErrorMessage = "Estoque mínimo deve ser informado")]
        public int EstoqueMinimo { get; set; }
       //[Required(ErrorMessage = "Quantidade atual deve ser informada")]
        public int EstoqueAtual { get; set; }
        public int UnidadeId { get; set; }

        [NotMapped]
        public Unidade UnidadeMedida { get; set; }
        
        public string Cor { get; set; }
        [StringLength(1)]
        public string Status { get; set; } //D - Disponível, A - Alugado, V - Vendido
        public DateTime DataInclusao { get; set; }

    }
}
