using BotAnzeigen.Core.model;
using System.Collections.Generic;

namespace BotAnzeigen
{
    public class SaveData
    {
        public string password;
        public string username;
        public string searchUrl;
        public string messageText;
        public List<AdItem> ads;
        public SaveData()
        {
            ads = new List<AdItem>();
        }
    }
}
