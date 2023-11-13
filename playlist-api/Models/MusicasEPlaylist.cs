using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace playlist_api.Models
{
    public class MusicasEPlaylist
    {
        public int Id { get; set; }
        [Required]
        public string NomeMusica { get; set; }
        [Required]
        public string NomeBanda { get; set; }
        public string Album { get; set; }
        [Required]
        public int DuracaoEmSegundos { get; set; }
        [Required]
        public int IdPlaylist { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public int Duracao { get; set; }

        public MusicasEPlaylist() 
        {
            Album = string.Empty;
            IdPlaylist = 0;
        }
    }
}