using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace playlist_api.Repositories
{
    public class Playlist
    {

        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;

        public Playlist(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
            _cmd = new SqlCommand();
        }

        public List<Models.Playlist> getPlaylists() 
        {
            List<Models.Playlist> playlists = new List<Models.Playlist>();

            using (_conn) 
            {
                _conn.Open();
                using (_cmd) 
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandText = "select id, nome, duracao from playlist;";
                    SqlDataReader dr = _cmd.ExecuteReader();

                    while (dr.Read()) 
                    {
                        Models.Playlist playlist = new Models.Playlist();

                        playlist.Id = (int) dr["id"];
                        playlist.Nome = (string)dr["nome"];
                        playlist.Duracao = (int)dr["duracao"];

                        playlists.Add(playlist);
                    }
                }
            }
            return playlists;
        }

        // if put ou post == true and playlist > 0, update playlist set duracao += m.duracaoemsegundos where m.id = @id
    }
}