using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BotAnzeigen.Core.service;
using BotAnzeigen.Core.model;
using Newtonsoft.Json;

namespace BotAnzeigen
{
    public partial class View : Form
    {
        private BotService bot;
        private Data data = new Data();
        private string saveDataFile = "saveData.json";

        public View()
        {
            InitializeComponent();

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerCompleted += worker_RunWorkerCompleted;

            try
            {
                if (System.IO.File.Exists(saveDataFile))
                {
                    string saveDataString = System.IO.File.ReadAllText(saveDataFile);
                    data = JsonConvert.DeserializeObject<Data>(saveDataString);
                    Console.WriteLine("Loaded Savedata");
                }
                txtMessageText.Text = data.messageText;
                txtPassword.Text = data.password;
                txtSearchUrl.Text = data.searchUrl;
                txtUsername.Text = data.username;
                listBoxAdList.DataSource = getAdItemTitles(data.adItems);

            }
            catch
            {

            }
        }

        private void btnStartBot_Click(object sender, EventArgs e)
        {
            if(txtMessageText.Text!="" && txtPassword.Text!="" && txtSearchUrl.Text!="" && txtUsername.Text!="")
            {

            
                disableInputs();
                data.messageText = txtMessageText.Text;
                data.password = txtPassword.Text;
                data.searchUrl = txtSearchUrl.Text;
                data.username = txtUsername.Text;
                saveDataToJson();

                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Alle Felder ausfüllen!", "Was machen Sachen?");
            }

        }

        private void btnStopBot_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Stopping Bot, please wait...");
            btnStopBot.Enabled = false;
            bot.stop();
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) 
        {
            bot = new BotService(data,30);
            bot.run();

            //bot = new Bot(txtUsername.Text, txtPassword.Text, txtSearchUrl.Text, txtMessageText.Text);
            //bot.login();
            //System.Threading.Thread.Sleep(2000);
            //bot.stopDriver();

            while (!backgroundWorker1.CancellationPending)
            {
                backgroundWorker1.ReportProgress(0);
                System.Threading.Thread.Sleep(2000);

            }
            if (backgroundWorker1.CancellationPending)
            {
                Console.WriteLine("Bot stopped");
                e.Cancel = true;
            }

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //listBoxAdList
            listBoxAdList.DataSource = null;
            listBoxAdList.DataSource = getAdItemTitles(bot.getAdItems());
            listBoxAdList.SelectedIndex = listBoxAdList.Items.Count - 1;
            data.adItems = bot.getAdItems();
            saveDataToJson();

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            enableInputs();
        }

        private void saveDataToJson()
        {
            string saveDataString = JsonConvert.SerializeObject(data, Formatting.Indented);

            System.IO.File.WriteAllText(saveDataFile, saveDataString);
            Console.WriteLine("Saved data");
        }

        private List<String> getAdItemTitles(List<AdItem> adItems)
        {
            List<String> adItemTitles = new List<String>();
            foreach(AdItem item in adItems)
            {
                adItemTitles.Add(item.title);
            }

            return adItemTitles;
         }

        private void disableInputs()
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtMessageText.Enabled = false;
            txtSearchUrl.Enabled = false;
            btnStartBot.Enabled = false;
            btnStopBot.Enabled = true;
        }

        private void enableInputs()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtMessageText.Enabled = true;
            txtSearchUrl.Enabled = true;
            btnStartBot.Enabled = true;
            btnStopBot.Enabled = false;
        }

        private void View_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("Stopping Bot, please wait...");
            bot.stop();
        }

        private void View_Load(object sender, EventArgs e)
        {

        }
    }
}
