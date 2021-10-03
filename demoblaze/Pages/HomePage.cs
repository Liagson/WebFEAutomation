using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebFEAutomation.demoblaze.Pages
{
    public class HomePage : DemoBlazePage
    {
        private static readonly string ID_LOGO = "nava";        
        private static readonly string ID_NEXT_PRODUCT_BUTTON = "next2";
        private static readonly string XPATH_LAPTOPS_MENU = "//a[contains(text(), 'Laptops')]";

        // This is a hack. I can't find a good way to ensure the laptops are loaded
        private static readonly string XPATH_LAPTOPS_LOAD_CONDITION = "//p[contains(text(), 'Intel')]";

        public HomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public override void Load() {
            TestContext.Out.WriteLine("Browser has loaded the Home page");

            wait.Until(driver => driver.FindElement(By.Id(ID_LOGO)));
        }

        public ProductPage SearchLaptop(string model) {
            ShowLaptops();
            SearchThroughAllResultsUntilProductFound(model);
            ProductPage product = new ProductPage(driver, wait, model);
            product.Load();
            return product;
        }
        private void SearchThroughAllResultsUntilProductFound(string model) {
            TestContext.Out.WriteLine("Browser is searching for '{0}'...", model);

            do {
                try {
                    wait.Until(ExpectedConditions.ElementToBeClickable(
                        By.XPath("//a[contains(text(), '" + model + "')]"))).Click();
                } catch (NoSuchElementException e) {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_NEXT_PRODUCT_BUTTON))).Click();
                }
                // There won't be anymore a "next" button at the end of the list
            } while (driver.FindElements(By.Id(ID_NEXT_PRODUCT_BUTTON)).Count == 1);
        }

        private void ShowLaptops() {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPATH_LAPTOPS_MENU))).Click();
            wait.Until(driver => driver.FindElement(By.XPath(XPATH_LAPTOPS_LOAD_CONDITION)));

            TestContext.Out.WriteLine("Laptop section has been selected");
        }
    }
}
