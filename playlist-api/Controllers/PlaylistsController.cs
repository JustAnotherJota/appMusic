using playlist_api.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace playlist_api.Controllers
{
    public class PlaylistsController : ApiController
    {
        private Repositories.Playlist playlist;
        public PlaylistsController() 
        {
            playlist = new Repositories.Playlist(Databases.getConnectionStringAppMusic());
        }

        [HttpGet]
        public IHttpActionResult getPlaylist() 
        {
            try
            { 
            return Ok(playlist.getPlaylists());
            }
            catch (Exception ex) 
            {
                return InternalServerError(ex);
            }
        }

    }
}
