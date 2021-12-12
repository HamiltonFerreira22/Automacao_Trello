using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace SecondTrello
{
    class Elements_Extensions
    {
        public IWebDriver BrowserConfig()
        {
            // Configurações do Chrome
            string projectPath = @System.Environment.CurrentDirectory.ToString();
            string chromeDriverPath;
            var options = new ChromeOptions();
            var os = Environment.OSVersion;
            Global.osLinux = os.Platform == PlatformID.Unix;
            ChromeDriverService service;
            if (Global.osLinux)
            {
                chromeDriverPath = "/usr/bin/";
                service = ChromeDriverService.CreateDefaultService(chromeDriverPath, "chromedriver");
            }
            else
            {
                chromeDriverPath = projectPath;
                service = ChromeDriverService.CreateDefaultService(chromeDriverPath, "chromedriver.exe");
            }
            //options.AddArgument("--headless");
            options.AddArgument("--incognito");
            //options.AddArgument("--window-size=1366,768");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--ignore-certificate-errors-spki-list");
            options.AddArgument("use-fake-ui-for-media-stream");
            //options.AddArgument("--touch-events=enabled");
            options.AddArgument("--start-maximized");
            options.AcceptInsecureCertificates = true;




            // Instância do driver
            IWebDriver driver = new ChromeDriver(service, options);
            return driver;
        }

        public void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
        public void Navigate(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            WaitForPageLoad(driver);
        }
        public void WaitForPageLoad(IWebDriver driver)
        {
            OpenQA.Selenium.Support.UI.WebDriverWait Wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            try
            {
                Wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (Exception)
            {




            }
        }

        public void SwitchTab(IWebDriver driver, int tabIndex)
        {
            driver.SwitchTo().Window(driver.WindowHandles[tabIndex]);
        }

        public void WaitVisible(IWebDriver driver, By element)
        {
            OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            Wait(400);
        }
        public void WaitClickable(IWebDriver driver, By element)
        {
            OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
        public void WaitSelectable(IWebDriver driver, By element)
        {
            OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeSelected(element));
        }
        public void WaitExists(IWebDriver driver, By element)
        {
            OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementExists(element));
        }
        public bool WaitExists(IWebDriver driver, By element, int waitValue = 30)
        {
            try
            {
                OpenQA.Selenium.Support.UI.WebDriverWait wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(waitValue));
                wait.Until(ExpectedConditions.ElementExists(element));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Click(IWebDriver driver, By element, int milliseconds = 0)
        {
            WaitExists(driver, element);
            WaitClickable(driver, element);
            driver.FindElement(element).Click();
            Wait(milliseconds);
        }
        public void DoubleClick(IWebDriver driver, By element, int milliseconds = 0)
        {
            WaitClickable(driver, element);


            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
            IWebElement webElement = driver.FindElement(element);
            actions.DoubleClick(webElement).Perform();
            Wait(milliseconds);
        }
        public void Clear(IWebDriver driver, By element)
        {
            WaitExists(driver, element);
            driver.FindElement(element).Clear();
        }
        public void SendKeys(IWebDriver driver, By element, string text, int milliseconds = 0)
        {
            WaitExists(driver, element);
            driver.FindElement(element).SendKeys(text);
            Wait(milliseconds);
        }
        public void SwitchFrame(IWebDriver driver, string frame)
        {
            driver.SwitchTo().ParentFrame();
            driver.SwitchTo().Frame(frame);
        }
        public void ParentFrame(IWebDriver driver)
        {
            driver.SwitchTo().ParentFrame();
        }
        public void ChangeFrame(IWebDriver driver, By element)
        {
            IWebElement webElement = driver.FindElement(element);
            driver.SwitchTo().Frame(webElement);
        }
        public void DefaultContentFrame(IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }
        public bool GetElementSelected(IWebDriver driver, By element)
        {
            WaitExists(driver, element);
            return driver.FindElement(element).Selected;
        }
        public string GetElementValue(IWebDriver driver, By element)
        {
            WaitExists(driver, element);
            return driver.FindElement(element).GetAttribute("value");
        }
        public string GetElementAttribute(IWebDriver driver, By element, string name)
        {
            WaitExists(driver, element);
            string result = "";
            IWebElement webElement = driver.FindElement(element);
            Size elementSize = webElement.Size;
            switch (name)
            {
                case "width":
                    result = elementSize.Width.ToString();
                    break;
                case "height":
                    result = elementSize.Height.ToString();
                    break;
                default:
                    result = driver.FindElement(element).GetAttribute(name);
                    break;
            }
            return result;
        }
        public string GetElementCssProperty(IWebDriver driver, By element, string property)
        {
            WaitExists(driver, element);
            return driver.FindElement(element).GetCssValue(property);
        }
        public string GetElementText(IWebDriver driver, By element)
        {
            WaitExists(driver, element);
            return driver.FindElement(element).Text;
        }
        public int GetElementsCount(IWebDriver driver, By element)
        {
            return driver.FindElements(element).Count;
        }
        public void SetElementAttribute(IWebDriver driver, By element, string name, string value)
        {
            WaitExists(driver, element);
            IWebElement webElement = driver.FindElement(element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0]." + name + "='" + value + "';", webElement);
        }
        public void SetSelectByValue(IWebDriver driver, By element, string value, int milliseconds = 0)
        {
            WaitExists(driver, element);
            var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(driver.FindElement(element));
            selectElement.SelectByValue(value);
            Wait(milliseconds);
        }
        public void SetSelectByText(IWebDriver driver, By element, string text)
        {
            WaitExists(driver, element);
            var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(driver.FindElement(element));
            selectElement.SelectByText(text);
        }
        public bool Exists(IWebDriver driver, By element)
        {
            bool result;
            try
            {
                IWebElement webElement = driver.FindElement(element);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public bool IsDisplayed(IWebDriver driver, By element)
        {
            bool result;
            try
            {
                IWebElement webElement = driver.FindElement(element);
                result = webElement.Displayed;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public bool IsVisible(IWebDriver driver, By element)
        {
            bool result;
            try
            {
                IWebElement webElement = driver.FindElement(element);
                result = (bool)((IJavaScriptExecutor)driver).ExecuteScript("var elem = arguments[0], box = elem.getBoundingClientRect(), cx = box.left + box.width / 2, cy = box.top + box.height / 2, e = document.elementFromPoint(cx, cy); for (; e; e = e.parentElement) { if (e === elem) return true; } return false;", webElement);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        public void SendKeys(IWebDriver driver, string text, int milliseconds = 0)
        {
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.SendKeys(text).Build().Perform();
            Wait(milliseconds);
        }
        public void MouseOver(IWebDriver driver, By element, int milliseconds = 0)
        {
            IWebElement webElement = driver.FindElement(element);
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(webElement).Perform();
            Wait(milliseconds);
        }


    }
}
