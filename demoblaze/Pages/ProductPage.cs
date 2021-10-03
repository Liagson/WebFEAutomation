using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace WebFEAutomation.demoblaze.Pages
{
    public class ProductPage : DemoBlazePage
    {
        private string model;
        private static readonly string XPATH_ADD_TO_CART = "//a[contains(text(), 'Add to cart')]";
        private static readonly string XPATH_PRICE = "//h3[contains(@class, 'price-container')]";
        public ProductPage(IWebDriver driver, WebDriverWait wait, string model) : base(driver, wait) {
            this.model = model;
        }

        public override void Load() {
            TestContext.Out.WriteLine("Browser has loaded the '{0}' product page", model);

            wait.Until(driver => driver.FindElement(By.XPath("//h2[contains(text(),'" + model + "')]")));
        }

        public void AddToCart() {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPATH_ADD_TO_CART))).Click();
            wait.Until(ExpectedConditions.AlertIsPresent());
            driver.SwitchTo().Alert().Accept();

            TestContext.Out.WriteLine("The product '{0}' has been added to the cart", model);
        }

        public decimal GetPrice() {
            string value = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(XPATH_PRICE))).Text;

            // Element text: "$XXXX *includes tax"
            return decimal.Parse(value.Split(' ')[0].Replace("$", string.Empty));
        }
    }
}
