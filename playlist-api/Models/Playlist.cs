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
        [Required]
        public string Nome { get; set; }
        public int Duracao { get; set; }
    }
}