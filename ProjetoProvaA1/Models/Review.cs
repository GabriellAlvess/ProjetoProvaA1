using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoProvaA1.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public DateTime ReviewDate { get; set; }
    
        public virtual ApplicationUser User { get; set; }
        public virtual Game Game { get; set; }
    }
}