using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace ProjetoProvaA1.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        public string Website { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}