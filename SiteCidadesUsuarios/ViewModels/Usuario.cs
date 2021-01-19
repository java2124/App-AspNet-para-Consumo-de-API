using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SiteCidadesUsuarios.ViewModels
{
    public class Usuario
    {
        [Key] public int cod_cliente { get; set; }
        [Required] [StringLength(100, ErrorMessage = "Tamanho Inválido", MinimumLength = 3)] [Column(TypeName = "VARCHAR")] public string nome_cliente { get; set; }
        [StringLength(100, ErrorMessage = "Tamanho Inválido", MinimumLength = 5)] [Column(TypeName = "VARCHAR")] public string sobrenome_cliente { get; set; }
        [Required] [StringLength(11, ErrorMessage = "Tamanho Inválido", MinimumLength = 11)] [Column(TypeName = "VARCHAR")] public string cpf_cliente { get; set; }
        [StringLength(11, ErrorMessage = "Tamanho Inválido", MinimumLength = 11)] [Column(TypeName = "VARCHAR")] public string celular_cliente { get; set; }
        [StringLength(10, ErrorMessage = "Tamanho Inválido", MinimumLength = 10)] [Column(TypeName = "VARCHAR")] public string tel_cliente { get; set; }
        [Required] [StringLength(9, ErrorMessage = "Tamanho Inválido", MinimumLength = 9)] [Column(TypeName = "VARCHAR")] public string rg_cliente { get; set; }
        [Required] [StringLength(100, ErrorMessage = "Tamanho Inválido", MinimumLength = 5)] [Column(TypeName = "VARCHAR")] public string email_cliente { get; set; }

        //FK cidade
        public Cidade cidade { get; set; }
        public int cod_cidade { get; set; }
    }
}