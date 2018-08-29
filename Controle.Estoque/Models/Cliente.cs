using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Controle.Estoque.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public string NomeCliente { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public string Documento{ get; set; }
        [StringLength(1)]
        public string Status { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public string Endereco{ get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public string Cidade{ get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        [StringLength(2)]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public string Pais{ get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Campo Obrigatorio!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public DateTime Data_inclusao { get; set; }
        public DateTime Data_alteracao{ get; set; }



    }
}
