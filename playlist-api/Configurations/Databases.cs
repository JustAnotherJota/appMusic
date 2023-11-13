using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace playlist_api.Configurations
{
    public class Databases
    {

        public static string getConnectionStringAppMusic() 
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["appmusic"].ConnectionString;
        }
    }
}