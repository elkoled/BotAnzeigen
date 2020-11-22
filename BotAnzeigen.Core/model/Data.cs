using BotAnzeigen.Core.model;
using System.Collections.Generic;

namespace BotAnzeigen
{
    public class Data
    {
        public string password;
        public string username;
        public string searchUrl;
        public string messageText;
        public int updateInterval;
        public List<AdItem> adItems = new List<AdItem>();
    }
}
