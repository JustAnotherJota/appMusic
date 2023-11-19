using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using playlist_api.Models;

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
            _cmd.Connection = _conn;
        }

        public async Task<List<Models.Playlist>> GetAllPlaylists() 
        {
            List<Models.Playlist> playlists = new List<Models.Playlist>();

            using (_conn) 
            {
                await _conn.OpenAsync();

                using (_cmd) 
                {
                    _cmd.CommandText = "select id, nome, duracao from playlist;";
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync()) 
                    {
                        Models.Playlist playlist = new Models.Playlist();

                        playlist.Id = (int) dr["id"];
                        playlist.Nome = (string)dr["nome"];
                        if (!(dr["duracao"] is DBNull))
                            playlist.Duracao = (int) dr["duracao"];

                        playlists.Add(playlist);
                    }
                }
            }
            return playlists;
        }

        public async Task<Models.Playlist> GetPlaylistById(int id)
        {
            Models.Playlist playlist = new Models.Playlist();
            using(_conn)
            {
                await _conn.OpenAsync();

                using(_cmd)
                {
                    _cmd.CommandText = "select id, nome, duracao from playlist where id = @id;";
                    _cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    if (await dr.ReadAsync())
                    {
                        playlist.Id = (int)dr["id"];
                        playlist.Nome = (string)dr["nome"];
                        if (!(dr["duracao"] is DBNull))
                            playlist.Duracao = (int)dr["duracao"];
                    }
                }

            }
            return playlist;
        }

        public async Task Add (Models.Playlist playlist)
        {
            using(_conn)
            {
                await _conn.OpenAsync();
                using(_cmd)
                {
                    _cmd.CommandText = "insert into playlist (nome, duracao) values (@nome , @duracao); select convert (int, scope_identity())";
                    _cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = playlist.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@duracao", System.Data.SqlDbType.Int)).Value = 0;
                    playlist.Duracao = 0;
                    playlist.Id = (int)await _cmd.ExecuteScalarAsync();
                }
            }
        }

        public async Task<bool> Update(Models.Playlist playlist)
        {
            int linhasAfetadas = 0;
            using(_conn)
            {
                await _conn.OpenAsync();
                using(_cmd)
                {
                    _cmd.CommandText = "update playlist set nome = @nome where id = @id; select duracao from playlist where id = @id";
                    _cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = playlist.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = playlist.Id;
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();
                    if (await dr.ReadAsync()) 
                    {
                        if (!(dr["duracao"] is DBNull))
                            playlist.Duracao = (int)dr["duracao"];
                    }

                    linhasAfetadas = dr.RecordsAffected;
                }
            }
            return linhasAfetadas > 0;
        }

        public async Task<bool> Delete(int id)
        {
            int linhasAfetadas = 0;
            using(_conn)
            {
                await _conn.OpenAsync();
                using(_cmd)
                {
                    _cmd.CommandText = "Delete from playlist where id = @id";
                    _cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;
                    linhasAfetadas = (int) await _cmd.ExecuteNonQueryAsync();
                }
            }
            return linhasAfetadas > 0;
        } 

        // if put ou post == true and playlist > 0, update playlist set duracao += m.duracaoemsegundos where m.id = @id
    }
}