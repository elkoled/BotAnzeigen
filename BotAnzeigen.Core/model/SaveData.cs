using System.Collections.Generic;

namespace BotAnzeigen
{
    public class SaveData
    {
        public string password;
        public string username;
        public string searchUrl;
        public string messageText;
        public List<string> ads;
        public SaveData()
        {
            ads = new List<string>();
        }
    }
}
