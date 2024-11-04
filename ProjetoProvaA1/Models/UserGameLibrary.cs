using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjetoProvaA1.Models
{
    public class UserGameLibrary
    {
        [Key, Column(Order =0)]
        public string UserId { get; set; } // A chave estrangeira deve ser do mesmo tipo que a chave primária da entidade ApplicationUser
        
        [Key, Column(Order = 1)]
        public int GameId { get; set; } // A chave estrangeira deve ser do mesmo tipo que a chave primária da entidade Game

        // Propriedades de navegação
        public virtual ApplicationUser User { get; set; }
        public virtual Game Game { get; set; }
    }

}