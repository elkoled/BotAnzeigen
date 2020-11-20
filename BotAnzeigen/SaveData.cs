using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BotAnzeigen
{
    class SaveData
    {
        public String password;
        public String username;
        public String searchUrl;
        public String messageText;
        public BindingList<String> ads;
        public SaveData()
        {
            ads = new BindingList<string>();
        }


    }
}
