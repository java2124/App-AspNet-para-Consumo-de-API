using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SiteCidadesUsuarios.ViewModels
{
    public class Cidade
    {
        [Key] public int cod_cidade { get; set; }
        [Required] [StringLength(100, ErrorMessage = "Tamanho Inválido", MinimumLength = 5)] [Column(TypeName = "VARCHAR")] public string nome_cidade { get; set; }
        [Required] [StringLength(2, ErrorMessage = "Tamanho Inválido", MinimumLength = 2)] [Column(TypeName = "CHAR")] public string uf_cidade { get; set; }
        [StringLength(8, ErrorMessage = "Tamanho Inválido", MinimumLength = 8)] [Column(TypeName = "VARCHAR")] public string cep_cidade { get; set; }

        //relacionando vários usuários à uma cidades
        public IEnumerable<Usuario> usuários { get; set; }
    }
}