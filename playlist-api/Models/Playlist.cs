using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace playlist_api.Models
{
    public class Playlist
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "É necessário informar o campo Nome para Playlist.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve conter até 100 caracteres")]
        public string Nome { get; set; }
        public int Duracao { get; set; }

        public Playlist ()
        {
            Id = 0;
            Nome = string.Empty;
            Duracao = 0;
        }
    }
}