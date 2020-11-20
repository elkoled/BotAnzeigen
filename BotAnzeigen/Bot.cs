using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BotAnzeigen
{
    class Bot
    {
        IWebDriver driver;
        String username;
        String password;
        String searchUrl;
        String message;

        public Bot(String username, String password, String searchUrl, String message)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-blink-features=AutomationControlled");
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            this.username = username;
            this.password = password;
            this.searchUrl = searchUrl;
            this.message = message;
        }

        public void login()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                
                driver.Url = "https://www.ebay-kleinanzeigen.de/m-einloggen.html";
                fakeWait(5000);

                //Accept cookies
                driver.FindElement(By.Id("gdpr-banner-accept")).Click();
                fakeWait(1000);
                
                //Paste login
                Console.WriteLine("Filling username");
                driver.FindElement(By.Id("login-email")).SendKeys(username);
                fakeWait(500);
                //Paste pw
                Console.WriteLine("Filling password");
                driver.FindElement(By.Id("login-password")).SendKeys(password);
                fakeWait(500);
                Console.WriteLine("Logging in");
                driver.FindElement(By.Id("login-submit")).Click();
                fakeWait(10000);
                
                Console.WriteLine("Navigating to search URL");
                driver.Url = searchUrl;
                fakeWait(10000);

                Console.WriteLine("Searching for results");
                var adItems = driver.FindElements(By.ClassName("aditem"));
                /*
                if (adItems.Count > 0)
                {
                    Console.WriteLine("Found " + adItems.Count + " results on this page\n");

                    foreach (IWebElement element in adItems)
                    {
                        Console.WriteLine(element.FindElement(By.XPath("//div[contains(@class, 'ellipsis')]")).Text);
                        
                        String output = "ID: ";
                        output += element.FindElement(By.Name("data-adid")).Text;
                        output += "\nDescription: ";
                        output += element.FindElement(By.ClassName("ellipsis")).Text;
                        Console.WriteLine(output);
                        
                    }
                }
                */

                //Click write message button
                //viewad - contact - button

            }
            catch
            {

            }
        }

        public String getAd()
        {
            Random rand = new Random();
            return "test";
        }

        public void stopDriver()
        {
            driver.Quit();
            Console.WriteLine("Quit Successfully");
        }

        //min. 500ms
        private void fakeWait(int ms)
        {
            try
            {
                Random rnd = new Random();
                if (ms < 500) ms = 500;
                int rand1 = rnd.Next(10, 1000);
                int rand2 = rnd.Next(ms, ms + rand1);
                Console.WriteLine("Waiting for " + rand2 + "ms");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(rand2);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
