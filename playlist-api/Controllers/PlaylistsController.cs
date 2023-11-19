using playlist_api.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace playlist_api.Controllers
{
    public class PlaylistsController : ApiController
    {
        private Repositories.Playlist _repositoriePlaylist;
        public PlaylistsController() 
        {
            _repositoriePlaylist = new Repositories.Playlist(Databases.getConnectionStringAppMusic());
        }

        [HttpGet]
        [ActionName("playlist")]
        public async Task<IHttpActionResult> Get() 
        {
            try
            { 
            return Ok(await _repositoriePlaylist.GetAllPlaylists());
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [ActionName("playlist")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Playlist playlist = await _repositoriePlaylist.GetPlaylistById(id);

                if(playlist.Id == 0)
                    return NotFound();

                return Ok(playlist);
            } 
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        [HttpPost]
        [ActionName("playlist")]
        public async Task<IHttpActionResult> Post([FromBody]Models.Playlist playlist)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _repositoriePlaylist.Add(playlist);
                
                if(playlist.Id == 0)
                    return BadRequest();

                return Ok(playlist);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [ActionName("playlist")]
        public async Task<IHttpActionResult> Put(int id,[FromBody]Models.Playlist playlist)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                if(playlist.Id != id)
                    return BadRequest("Objeto não relacionado com a URL invocada. Ids diferentes.");
                
                bool atualizar = await _repositoriePlaylist.Update(playlist);

                if(!atualizar)
                    return NotFound();
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [ActionName("playlist")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                bool deletar = await _repositoriePlaylist.Delete(id);
                
                if(!deletar)
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
