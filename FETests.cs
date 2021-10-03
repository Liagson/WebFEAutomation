using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using WebFEAutomation.demoblaze.DTOs;
using WebFEAutomation.demoblaze.Pages;

namespace WebFEAutomation
{
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void Setup() {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver,
                TimeSpan.FromSeconds(Constants.STANDARD_WAIT_TIME));
        }

        [Test]
        public void PurchaseTest() {
            PlaceOrderDTO orderData = new PlaceOrderDTO {
                Name = "Mr. Test",
                Country = "Spain",
                City = "Zaragoza",
                CreditCard = "0123456789",
                Month = "01",
                Year = "2022"
            };
            decimal expectedPurchaseCost = 0;

            HomePage mainPage = new HomePage(driver, wait);
            mainPage.Load();
            ProductPage product1 = mainPage.SearchLaptop("Sony vaio i5");
            expectedPurchaseCost += product1.GetPrice();
            product1.AddToCart();
            product1.MoveToHome();

            ProductPage product2 = mainPage.SearchLaptop("Dell i7 8gb");
            expectedPurchaseCost += product2.GetPrice();
            product2.AddToCart();
            CartPage cart = product2.MoveToCart();

            expectedPurchaseCost -= cart.DeleteProduct("Dell i7 8gb");
            PurchaseDTO purchase = cart.PlaceOrder(orderData);

            TestContext.Out.WriteLine("A purchase with ID:{0} with the cost of ${1} has been created",
                purchase.Id, purchase.Amount);
            Assert.AreEqual(expectedPurchaseCost.ToString(), purchase.Amount);
        }

        [TearDown]
        public void Clean() {
            driver.Quit();
        }
    }
}