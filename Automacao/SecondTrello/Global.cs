using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecondTrello
{
    class Global
    {
        public static IWebDriver driver;
        public static Elements_Extensions Elements_Extensions;
        public static Trello trello;
        public static SQLConnect sql;
        public static bool osLinux = false;

    }
}
