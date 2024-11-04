using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoProvaA1.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Titulo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Titulo deve ter no máximo 100 caracteres.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "O modelo do avião é obrigatório.")]
        public int DeveloperId { get; set; }
       

        public virtual Developer Developer { get; set; }

       
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}