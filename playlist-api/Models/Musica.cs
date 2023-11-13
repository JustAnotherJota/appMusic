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
        [Required]
        public string NomeMusica { get; set; }
        [Required]
        public string NomeBanda { get; set; }
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