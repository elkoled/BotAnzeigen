﻿using System;
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
                txtUpdateInterval.Text = data.updateInterval.ToString();
                listBoxAdList.DataSource = getAdItemTitles(data.adItems);

            }
            catch
            {

            }
        }

        private void btnStartBot_Click(object sender, EventArgs e)
        {
            if(txtMessageText.Text!="" && 
               txtPassword.Text!=""    && 
               txtSearchUrl.Text!=""   && 
               txtUsername.Text!=""    && 
               txtUpdateInterval.Text!="")
            {

                data.messageText = txtMessageText.Text;
                data.password = txtPassword.Text;
                data.searchUrl = txtSearchUrl.Text;
                data.username = txtUsername.Text;
                try
                {
                    data.updateInterval = Int32.Parse(txtUpdateInterval.Text);
                }
                catch
                {
                    MessageBox.Show("Update Interval erlaubt nur Zahlen!", "Was machen Sachen?");
                    return;
                }
                disableInputs();
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
            
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) 
        {
            bot = new BotService(data);
            bot.start();

            while (!backgroundWorker1.CancellationPending)
            {
                bot.searchItems();
                backgroundWorker1.ReportProgress(0);
                Console.WriteLine("Waiting for " + data.updateInterval + "s until next check\n");
                System.Threading.Thread.Sleep(data.updateInterval*1000);
            }
            if (backgroundWorker1.CancellationPending)
            {
                bot.stop();
                Console.WriteLine("Bot stopped");
                e.Cancel = true;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Updating listBox...");
            List<AdItem> aditemsTemp = bot.getAdItems();
            data.adItems = aditemsTemp;

            listBoxAdList.DataSource = null;
            listBoxAdList.DataSource = getAdItemTitles(aditemsTemp);
            listBoxAdList.SelectedIndex = listBoxAdList.Items.Count - 1;
            
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
                adItemTitles.Add(item.title + " || ID:" + item.id);
            }

            return adItemTitles;
         }

        private void disableInputs()
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtMessageText.Enabled = false;
            txtSearchUrl.Enabled = false;
            txtUpdateInterval.Enabled = false;
            btnStartBot.Enabled = false;
            btnStopBot.Enabled = true;
        }

        private void enableInputs()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            txtMessageText.Enabled = true;
            txtSearchUrl.Enabled = true;
            txtUpdateInterval.Enabled = true;
            btnStartBot.Enabled = true;
            btnStopBot.Enabled = false;
        }

        private void View_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("Stopping Bot, please wait...");
            bot.stop();
        }

    }
}
