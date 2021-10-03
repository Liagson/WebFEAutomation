using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using WebFEAutomation.demoblaze.DTOs;
using WebFEAutomation.demoblaze.Pages;
using TechTalk.SpecFlow;

namespace WebFEAutomation
{
    [Binding]
    public class DemoBlazePurchaseSteps
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private HomePage homePage;
        private ProductPage productPage;
        private CartPage cartPage;
        
        private PurchaseDTO purchase;
        private decimal expectedPurchaseCost = 0;

        [Given(@"I have DemoBlaze open in my browser")]
        public void GivenIHaveDemoBlazeOpenInMyBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver,
                TimeSpan.FromSeconds(Constants.STANDARD_WAIT_TIME));
        }
        
        [When(@"I search for ""(.*)"" in the Laptops category to add it in the cart")]
        public void WhenISearchForInTheLaptopsCategoryToAddItInTheCart(string product)
        {
            homePage = new HomePage(driver, wait);
            homePage.Load();
            productPage = homePage.SearchLaptop(product);
            expectedPurchaseCost += productPage.GetPrice();
            productPage.AddToCart();
        }

        [When(@"I move back to the Home page")]
        public void WhenIMoveBackToTheHomePage() {
            homePage = homePage.MoveToHome();
        }

        [When(@"I move to the cart page")]
        public void WhenIMoveToTheCartPage() {
            cartPage = homePage.MoveToCart();
        }

        [When(@"I delete the ""(.*)"" from the cart")]
        public void WhenIDeleteTheFromTheCart(string product)
        {
            expectedPurchaseCost -= cartPage.DeleteProduct(product);
        }
        
        [When(@"I make the purchase")]
        public void WhenIMakeThePurchase()
        {
            PlaceOrderDTO orderData = new PlaceOrderDTO {
                Name = "Mr. Test",
                Country = "Spain",
                City = "Zaragoza",
                CreditCard = "0123456789",
                Month = "01",
                Year = "2022"
            };

            purchase = cartPage.PlaceOrder(orderData);
            TestContext.Out.WriteLine("A purchase with ID:{0} with the cost of ${1} has been created",
                purchase.Id, purchase.Amount);
        }
        
        [Then(@"the amount should be the expected")]
        public void ThenTheAmountShouldBeTheExpected()
        {
            Assert.AreEqual(expectedPurchaseCost.ToString(), purchase.Amount);
            driver.Quit();
        }
    }
}
