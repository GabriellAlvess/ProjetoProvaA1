using ProjetoProvaA1.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;

namespace ProjetoProvaA1.Models
{
    public class Game : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Titulo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Titulo deve ter no máximo 100 caracteres.")]
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "O desenvolvedor é obrigatório.")]
        public int DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }
        [Required(ErrorMessage = "Pelo menos um gênero é obrigatório.")]
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        [Required(ErrorMessage = "Pelo menos uma avaliação é obrigatória.")]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        // Propriedade auxiliar para armazenar os IDs dos gêneros selecionados
        public List<int> SelectedGenreIds { get; set; } = new List<int>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Implementação da validação personalizada, se necessário
            return Enumerable.Empty<ValidationResult>();
        }
    } 
}
