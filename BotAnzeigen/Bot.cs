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
        String test_url;

        public Bot(String username, String password, String test_url)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-blink-features=AutomationControlled");
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            this.username = username;
            this.password = password;
            this.test_url = test_url;
        }

        public void login()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.Url = "https://www.ebay-kleinanzeigen.de/m-einloggen.html";
                fakeWaiter(5000);
                driver.FindElement(By.Id("gdpr-banner-accept")).Click();
                //fakeWaiter(2000);
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
        private void fakeWaiter(int ms)
        {
            Random rnd = new Random();
            if (ms < 500) ms = 500;
            int rand1 = rnd.Next(-500, 500);
            int rand2 = rnd.Next(ms, ms + rand1);
            Console.WriteLine("Waiting for " + rand2 + "ms");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(rand2);
        }
    }
}
