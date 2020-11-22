﻿using BotAnzeigen.Core.model;
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
        bool isRunning = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="checkInterval">Delay between checking for new ads. Has to be >=30s for performance reasons</param>
        public BotService(Data data)
        {
            this.data = data;

            if (data.updateInterval < 30) this.data.updateInterval = 30;
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
                Console.WriteLine("Waiting for " + data.updateInterval + "s until next check\n");
                System.Threading.Thread.Sleep(data.updateInterval*1000);
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
                fakeWait(1000);
                //Paste pw
                Console.WriteLine("Filling password");
                driver.FindElement(By.Id("login-password")).SendKeys(data.password);
                fakeWait(1000);
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

            bool anyNewItem = false;
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
                    anyNewItem = true;
                    data.adItems.Add(foundItem);
                    Console.WriteLine("Found new item:");
                    //Console.WriteLine(foundItem.id);
                    Console.WriteLine(foundItem.title);
                    //Console.WriteLine(foundItem.url);
                    Console.WriteLine("\n");
                }
            }
            if (anyNewItem == false) Console.WriteLine("-> No new items");
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
                    fakeWait(3000);
                    //Wait for popup to maybe appear
                    System.Threading.Thread.Sleep(1000);

                    try
                    {
                        //Search for popup close button and click it
                        driver.FindElement(By.XPath("//*[@type='button' and @class='mfp-close']")).Click();
                        Console.WriteLine("Found warning popup, closing popup");
                        System.Threading.Thread.Sleep(1000);
                    }
                    catch(NoSuchElementException)
                    {
                        Console.WriteLine("No warning popup found, that is good");
                    }
                    fakeWait(1000);
                    //Element only exists when logged in. Fill message window
                    try
                    {
                        driver.FindElement(By.XPath("//textarea[@name='message' and @class='viewad-contact-message']")).SendKeys(data.messageText);
                    }
                    catch(NoSuchElementException)
                    {
                        Console.WriteLine("Message box not found");
                    }
                    fakeWait(1000);
                    try
                    {
#if DEBUG
                        driver.FindElement(By.XPath("//button[@class='button viewad-contact-submit taller' and @type='submit']"));
#else
                        IWebElement button = driver.FindElement(By.XPath("//button[@class='button viewad-contact-submit taller' and @type='submit']")).Click();
#endif
                    }
                    catch(NoSuchElementException)
                    {
                        Console.WriteLine("Button not found");
                    }
                    fakeWait(2000);
                    Console.WriteLine("Successfully sent message!\n");   
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
        /// simulate waiting, min. 1000ms.
        /// </summary>
        /// <param name="ms"></param>
        private void fakeWait(int ms)
        {
            Random rnd = new Random();
            if (ms < 1000) ms = 1000;
            int randDelay = ms + rnd.Next(-500, 1000);
            Console.WriteLine("<delay> for max. " + randDelay + "ms");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(randDelay);
        }
    }
}
