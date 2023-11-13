using playlist_api.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace playlist_api.Controllers
{
    public class MusicasController : ApiController
    {

        Repositories.Musica _Repositorio_musica;
        public MusicasController()
        {
            _Repositorio_musica = new Repositories.Musica(Databases.getConnectionStringAppMusic());
        }

        [HttpGet]
        [ActionName("musica")]
        public IHttpActionResult Get()
        {
            try 
            { 
                return Ok(_Repositorio_musica.getMusica());
            } 
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [ActionName("musica")]
        public IHttpActionResult Get(int id)
        {
            try { 
            Models.Musica musica = _Repositorio_musica.getMusica(id);
            if (musica.Id == 0)
                return NotFound();
            
            return Ok(musica);
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [ActionName ("musicasdaplaylist")]
        public IHttpActionResult GetMusicaDaPlaylist(int id)
        {
            try {
                List<Models.MusicasEPlaylist> musicasplaylist = _Repositorio_musica.getMusicasDaPlaylist(id);

                if(musicasplaylist.Count == 0)
                    return NotFound();
                
                return Ok(musicasplaylist);
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ActionName("musica")]
        public IHttpActionResult Post([FromBody] Models.Musica nomemusica)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _Repositorio_musica.addMusica(nomemusica);

                if (nomemusica.Id == 0)
                    return BadRequest();

                return Ok(nomemusica);

            }
            catch (Exception ex)
            { 
                return InternalServerError(ex);
            }
        }

        //[HttpPut]
        //[ActionName("musica")]
        //public IHttpActionResult Put(int id,[FromBody] Models.Musica musica) 
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        if (id != musica.Id)
        //            return BadRequest("Objeto não relacionado com a URL invocada. Ids diferentes.");
               
        //        bool update = _Repositorio_musica.UpdateMusica(musica);

        //        if (!update)
        //            return NotFound();

        //        return Ok(musica);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        [HttpDelete]
        [ActionName("musica")]
        public IHttpActionResult Delete(int id) 
        {
            try
            {
                bool deletar = _Repositorio_musica.Delete(id);

                if (!deletar)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
