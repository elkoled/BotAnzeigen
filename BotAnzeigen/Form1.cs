using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace BotAnzeigen
{
    public partial class Form1 : Form
    {
        Bot bot;
        SaveData saveData;
        String saveDataPath = "saveData.json";
        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerCompleted += worker_RunWorkerCompleted;

            saveData = new SaveData();
            try
            {
                if (System.IO.File.Exists(saveDataPath))
                {
                    String saveDataString = System.IO.File.ReadAllText(saveDataPath);
                    saveData = JsonConvert.DeserializeObject<SaveData>(saveDataString);
                    Console.WriteLine("Loaded Savedata");
                }
                txtMessageText.Text = saveData.messageText;
                txtPassword.Text = saveData.password;
                txtSearchUrl.Text = saveData.searchUrl;
                txtUsername.Text = saveData.username;
                listBoxAdList.DataSource = saveData.ads;
            }
            catch
            {

            }
        }

        private void btnStartBot_Click(object sender, EventArgs e)
        {
            disableInputs();
            saveData.messageText = txtMessageText.Text;
            saveData.password = txtPassword.Text;
            saveData.searchUrl = txtSearchUrl.Text;
            saveData.username = txtUsername.Text;
            saveDataToJson();
            

            backgroundWorker1.RunWorkerAsync();

        }

        private void btnStopBot_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Stopping Bot, please wait...");
            backgroundWorker1.CancelAsync();
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) 
        {
            bot = new Bot(txtUsername.Text, txtPassword.Text, txtSearchUrl.Text);
            bot.login();
            System.Threading.Thread.Sleep(2000);
            bot.stopDriver();

            while (!backgroundWorker1.CancellationPending)
            {
                System.Threading.Thread.Sleep(2000);
                //saveData.ads.Add(bot.getAd());
                //backgroundWorker1.ReportProgress(0);
              
                //saveDataToJson();

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
            listBoxAdList.DataSource = saveData.ads;
            listBoxAdList.SelectedIndex = listBoxAdList.Items.Count - 1;

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            enableInputs();
        }

        private void saveDataToJson()
        {
            String saveDataString = JsonConvert.SerializeObject(saveData, Formatting.Indented);

            System.IO.File.WriteAllText(saveDataPath, saveDataString);
            Console.WriteLine("Saved data");
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
