using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace playlist_api.Repositories
{
    public class Musica
    {

        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;
        public Musica(string connectionString)
        {
            _conn = new SqlConnection(connectionString);
            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
        }
        public async Task<List<Models.Musica>> GetAll()
        {

            List<Models.Musica> musicas = new List<Models.Musica>();

            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = "select id, nomemusica, nomebanda, album, duracaoemsegundos, idPlaylist from musica;";
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                    {

                        Models.Musica musica = new Models.Musica();

                        musica.Id = (int)dr["id"];
                        musica.NomeMusica = (string)dr["nomemusica"];
                        musica.NomeBanda = (string)dr["nomebanda"];
                        if (!(dr["album"] is DBNull))
                            musica.Album = (string)dr["album"];
                        musica.DuracaoEmSegundos = (int)dr["duracaoemsegundos"];
                        if (!(dr["idPlaylist"] is DBNull))
                            musica.IdPlaylist = (int)dr["idPlaylist"];

                        musicas.Add(musica);
                    }
                }
            }
            return musicas;
        }

        public async Task<Models.Musica> GetById(int id)
        {
            Models.Musica musica = new Models.Musica();
            using (_conn)
            {
                await _conn.OpenAsync();

                using (_cmd)
                {
                    _cmd.CommandText = $"select id, nomemusica, nomebanda, album, duracaoemsegundos, idPlaylist from musica where id = @id";

                    _cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();
                    if (await dr.ReadAsync())
                    {
                        musica.Id = (int)dr["id"];
                        musica.NomeMusica = (string)dr["nomemusica"];
                        musica.NomeBanda = (string)dr["nomebanda"];
                        if (!(dr["album"] is DBNull))
                            musica.Album = (string)dr["album"];
                        musica.DuracaoEmSegundos = (int)dr["duracaoemsegundos"];
                        if (!(dr["idPlaylist"] is DBNull))
                            musica.IdPlaylist = (int)dr["idPlaylist"];
                    }
                }
            }
            return musica;
        }

        public async Task<List<Models.MusicasEPlaylist>> GetMusicasDaPlaylist(int id)
        {
            List<Models.MusicasEPlaylist> musicasEPlaylist = new List<Models.MusicasEPlaylist>();
            using (_conn)
            {
                await _conn.OpenAsync();
                using (_cmd)
                {
                    _cmd.CommandText = $"select m.id, m.nomemusica, m.nomebanda, m.album, m.duracaoemsegundos, m.idPlaylist, p.id, p.nome, p.duracao from musica as m " +
                        $"inner join playlist as p on p.id = m.idPlaylist where m.idPlaylist = @id;";

                    _cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;
                    SqlDataReader dr = await _cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                    {
                        Models.MusicasEPlaylist musicaplaylist = new Models.MusicasEPlaylist();

                        musicaplaylist.Id = (int)dr["id"];
                        musicaplaylist.NomeMusica = (string)dr["nomemusica"];
                        musicaplaylist.NomeBanda = (string)dr["nomebanda"];
                        if (!(dr["album"] is DBNull))
                            musicaplaylist.Album = (string)dr["album"];

                        musicaplaylist.DuracaoEmSegundos = (int)dr["duracaoemsegundos"];

                        if (!(dr["idPlaylist"] is DBNull))
                            musicaplaylist.IdPlaylist = (int)dr["idPlaylist"];

                        musicaplaylist.Nome = (string)dr["nome"];
                        musicaplaylist.Duracao = (int)dr["duracao"];

                        musicasEPlaylist.Add(musicaplaylist);
                    }
                }
            }
            return musicasEPlaylist;
        }

        public async Task Add(Models.Musica musicaCriada)
        {
            using (_conn)
            {
                await _conn.OpenAsync();
                using (_cmd)
                {
                    _cmd.CommandText = $"insert into musica(nomemusica, nomebanda, album, duracaoemsegundos, idPlaylist) " +
                    $"values ( @nomemusica , @nomebanda , @album , @duracaoemsegundos , @idPlaylist);" +
                    "select convert(int,scope_identity())as id;";

                    _cmd.Parameters.Add(new SqlParameter("@duracaoemsegundos", SqlDbType.Int)).Value = musicaCriada.DuracaoEmSegundos;
                    _cmd.Parameters.Add(new SqlParameter("@nomemusica", SqlDbType.VarChar)).Value = musicaCriada.NomeMusica;
                    _cmd.Parameters.Add(new SqlParameter("@album", SqlDbType.VarChar)).Value = musicaCriada.Album;
                    _cmd.Parameters.Add(new SqlParameter("@nomebanda", SqlDbType.VarChar)).Value = musicaCriada.NomeBanda;
                    if (musicaCriada.IdPlaylist == 0)
                        _cmd.Parameters.Add(new SqlParameter("@idPlaylist", SqlDbType.Int)).Value = DBNull.Value;
                    else
                        _cmd.Parameters.Add(new SqlParameter("@idPlaylist", SqlDbType.Int)).Value = musicaCriada.IdPlaylist;

                    musicaCriada.Id = (int)await _cmd.ExecuteScalarAsync();
                }
            }
        }

        /*      public bool UpdateMusica(Models.Musica musica) ---- ATUALIZAR PLAYLIST
               {
                   int linhasafetadas;
                   using (_conn)
                   {
                       _conn.Open();
                       using (_cmd)
                       {
                           _cmd.CommandText = "update musica set nomemusica = @nomemusica , nomebanda = @nomebanda, album = @album , duracaoemsegundos = @duracaoemsegundos , idPlaylist = @idPlaylist where id = @id;";
                           _cmd.Parameters.Add(new SqlParameter("@duracaoemsegundos", SqlDbType.Int)).Value = musica.DuracaoEmSegundos;
                           _cmd.Parameters.Add(new SqlParameter("@nomemusica", SqlDbType.VarChar)).Value = musica.NomeMusica;
                           _cmd.Parameters.Add(new SqlParameter("@album", SqlDbType.VarChar)).Value = musica.Album;
                           _cmd.Parameters.Add(new SqlParameter("@nomebanda", SqlDbType.VarChar)).Value = musica.NomeBanda;
                           _cmd.Parameters.Add(new SqlParameter("@idPlaylist", SqlDbType.Int)).Value = musica.IdPlaylist;

                           _cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = musica.Id;

                           linhasafetadas = _cmd.ExecuteNonQuery();

                       }
                   }

                   return linhasafetadas > 0;
               } */

        public async Task<bool> Delete(int id)
        {
            {
                int linhasafetadas;
                using (_conn)
                {
                    await _conn.OpenAsync();
                    using (_cmd)
                    {
                        _cmd.CommandText = "Delete from musica where id = @id";

                        _cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;
                        linhasafetadas = await _cmd.ExecuteNonQueryAsync();
                    }
                }
                return linhasafetadas > 0;
            }
        }
    }
}