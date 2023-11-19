using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace playlist_api.Models
{
    public class Musica
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É necessário informar o campo NomeMusica")]
        [StringLength(100, ErrorMessage = "O campo NomeMusica deve conter até 100 caracteres")]
        public string NomeMusica { get; set; }

        [Required(ErrorMessage = "É necessário informar o campo NomeBanda")]
        [StringLength(100, ErrorMessage = "O campo NomeBanda deve conter até 100 caracteres")]
        public string NomeBanda { get; set;}
        
        [StringLength(100, ErrorMessage = "O campo NomeBanda deve conter até 100 caracteres")]
        public string Album { get; set; }

        [Required]
        public int DuracaoEmSegundos { get; set; }
        public int IdPlaylist { get; set; }

        public Musica() 
        {
            NomeBanda = string.Empty;
            NomeMusica = string.Empty;
            Album = string.Empty;
            DuracaoEmSegundos = 0;
            IdPlaylist = 0;
        }

    }
}