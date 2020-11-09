using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace StudIPDownloader.WebApi.Controllers
{
    public class LoginActivityServiceWithSelenium : ILoginActivityService
    {
        private readonly ILogger<LoginActivityServiceWithSelenium> logger;

        public LoginActivityServiceWithSelenium(ILogger<LoginActivityServiceWithSelenium> logger)
        {
            this.logger = logger;
        }

        public async Task<string> Login(string username,  string password, string url)
        {
            Console.WriteLine($"Directory: {Directory.GetCurrentDirectory()}");
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("-headless");
            using IWebDriver driver = new FirefoxDriver(firefoxOptions);
            driver.Navigate().GoToUrl(url);

            driver.FindElement(By.ClassName("login_link")).FindElement(By.XPath(".//*")).Click();

            logger.LogInformation(driver.Url);

            var usernameField = driver.FindElement(By.Name("j_username"));
            usernameField.SendKeys(username);
            var passwordField = driver.FindElement(By.Id("password"));
            passwordField.SendKeys(password);
            var loginButton = driver.FindElement(By.Name("_eventId_proceed"));
            loginButton.Click();

            driver
                .WaitFor(TimeSpan.FromSeconds(5))
                .Until(x => x
                        .Manage()
                        .Cookies
                        .AllCookies
                        .Where(y => y
                            .Name.Equals("Seminar_Session"))
                        .ToList()
                        .Count != 0);



            var cookies = driver.Manage().Cookies.AllCookies.Where(x => x.Name.Equals("Seminar_Session")).ToList();
            driver.Quit();
            return cookies.Last().Value;
        }
    }

    public static class IWebdriverExtensions
    {
        public static IWaitingWebdriver WaitFor(this IWebDriver driver, TimeSpan timespan)
        {
            return new WaitingWebdriver(driver, timespan);
        }
    }

    public class WaitingWebdriver : IWaitingWebdriver
    {
        public WaitingWebdriver(IWebDriver driver, TimeSpan timespan)
        {
            Driver = driver;
            Timespan = timespan;
        }

        public IWebDriver Driver { get; }
        public TimeSpan Timespan { get; }

        public TResult Until<TResult>(Func<IWebDriver, TResult> condition)
        {
            var wait = new WebDriverWait(Driver, Timespan);
            return wait.Until(condition);
        }
    }

    public interface IWaitingWebdriver
    {
        public TResult Until<TResult>(Func<IWebDriver, TResult> condition);
    }
}