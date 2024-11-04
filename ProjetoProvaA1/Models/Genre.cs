using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoProvaA1.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Nome ter no máximo 100 caracteres.")]
        public string Name { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}