using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebFEAutomation.demoblaze.DTOs;
using WebFEAutomation.demoblaze.Forms;

namespace WebFEAutomation.demoblaze.Pages
{
    public class CartPage : DemoBlazePage
    {
        private static readonly string XPATH_TITLE = "//h2[contains(text(),'Products')]";
        private static readonly string XPATH_PLACE_ORDER = "//button[text() = 'Place Order']";

        public CartPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public override void Load() {
            TestContext.Out.WriteLine("Browser has loaded the Cart page");

            wait.Until(driver => driver.FindElement(By.XPath(XPATH_TITLE)));
        }

        public PurchaseDTO PlaceOrder(PlaceOrderDTO orderData) {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPATH_PLACE_ORDER))).Click();
            wait.Until(driver => driver.FindElement(
                By.XPath(PlaceOrderForm.XPATH_PLACE_ORDER_TITLE)));

            PlaceOrderForm orderForm = new PlaceOrderForm(driver, wait);
            return orderForm.Purchase(orderData);
        }

        public decimal DeleteProduct(string name) {
            string productRow = "//tr[td/text() = '" + name + "']/td/a";
            decimal productCost = GetSingleProductCost(name);

            wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath(productRow))).Click();

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(
                By.XPath(productRow)));

            TestContext.Out.WriteLine("The product {0} with the cost of ${1} " +
                "has been deleted from the cart",
                name, productCost);
            return productCost;
        }

        private decimal GetSingleProductCost(string name) {
            string productCostLocation = "//tr[td/text() = '" + name + "']/td[3]";
            string productCost = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath(productCostLocation))).Text;
            return decimal.Parse(productCost);
        }
    }
}
