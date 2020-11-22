using BotAnzeigen.Core.model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace BotAnzeigen.Core.service
{ 
    public class BotService
    {
        IWebDriver driver;
        Data data;
        int checkInterval;
        bool isRunning = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="checkInterval">Delay between checking for new ads. Has to be >=30s for performance reasons</param>
        public BotService(Data data, int checkInterval)
        {
            this.data = data;
            if (checkInterval >= 30) this.checkInterval = checkInterval;
        }

        public void run()
        {
            isRunning = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-blink-features=AutomationControlled");
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(10);

#warning Maybe check each cycle if still logged in?
            login();
            while (isRunning == true)
            {
                searchItems();
                writeMessagesToSeller();
                Console.WriteLine("Waiting for " + checkInterval + "s until next check");
                System.Threading.Thread.Sleep(checkInterval*1000);
            }
            
        }


        private void login()
        {
            try
            {
                driver.Url = "https://www.ebay-kleinanzeigen.de/m-einloggen.html";
                fakeWait(10000);

                //Accept cookies
                driver.FindElement(By.Id("gdpr-banner-accept")).Click();
                fakeWait(1000);

                //Paste login
                Console.WriteLine("Filling username");
                driver.FindElement(By.Id("login-email")).SendKeys(data.username);
                fakeWait(500);
                //Paste pw
                Console.WriteLine("Filling password");
                driver.FindElement(By.Id("login-password")).SendKeys(data.password);
                fakeWait(500);
                Console.WriteLine("Logging in");
                driver.FindElement(By.Id("login-submit")).Click();
                fakeWait(10000);
            }
            catch
            {

            }
        }


        private void searchItems()
        {
            Console.WriteLine("Navigating to search URL");
            driver.Url = data.searchUrl;
            fakeWait(10000);
            //Add sleep time for all items to load (otherwise some titles will be empty)
            System.Threading.Thread.Sleep(2000);
           
            Console.WriteLine("Searching for results");
            ReadOnlyCollection<IWebElement> adItems = driver.FindElements(By.ClassName("aditem"));

            foreach (var element in adItems)
            {
                bool duplicate = false;
                AdItem foundItem = new AdItem();
                foundItem.id = element.GetAttribute("data-adid");
                foundItem.title = element.FindElement(By.ClassName("text-module-begin")).Text;
                foundItem.url = element.FindElement(By.ClassName("ellipsis")).GetAttribute("href");

                //Item already in list?
                foreach (AdItem tempItem in data.adItems)
                {
                    if (tempItem.id == foundItem.id)
                    {
                        duplicate = true;
                        break;
                    }
                }

                if(duplicate==false)
                { 
                    data.adItems.Add(foundItem);
                    Console.WriteLine("Found new item:");
                    Console.WriteLine(foundItem.id);
                    Console.WriteLine(foundItem.title);
                    Console.WriteLine(foundItem.url);
                    Console.WriteLine("\n");
                }
            }
        }

        private void writeMessagesToSeller()
        {
            foreach(AdItem adItem in data.adItems)
            {
                //Only sent message if not already messaged
                if(adItem.messageSent == false)
                {
                    Console.WriteLine("Sending message to item: " + adItem.title);
                    Console.WriteLine("Navigating to item URL");
                    driver.Url = adItem.url;
                    //ID only exists when logged in
                    driver.FindElement(By.Id("viewad-contact-button")).Click();
                    fakeWait(1000);
#warning Find element message-box and send-button, fill message, click send

                    //set sent message to true
                    adItem.messageSent = true;
                }
            }
        }

        public void reportAds(Action<List<AdItem>> callback)
        {
            callback(data.adItems);
            fakeWait(10000);
        }

        public List<AdItem> getAdItems()
        {
            return data.adItems;
        }

        public void stop()
        {
            isRunning = false;
            driver.Quit();
        }

        /// <summary>
        /// simulate waiting, min. 500ms.
        /// </summary>
        /// <param name="ms"></param>
        private void fakeWait(int ms)
        {
            Random rnd = new Random();
            if (ms < 500) ms = 500;
            int rand1 = rnd.Next(10, 1000);
            int rand2 = rnd.Next(ms, ms + rand1);
            Console.WriteLine("Waiting for max. " + rand2 + "ms");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(rand2);
        }
    }
}
