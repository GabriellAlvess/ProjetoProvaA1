using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoProvaA1.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "A avaliação deve ser entre 1 e 5.")]
        public int Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        public string UserId { get; set; }

        public DateTime ReviewDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}