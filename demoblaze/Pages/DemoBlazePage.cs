using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebFEAutomation.demoblaze.Pages
{
    public abstract class DemoBlazePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        private static readonly string ID_CART = "cartur";
        private static readonly string XPATH_HOME = "//a[contains(text(), 'Home')]";

        public DemoBlazePage(IWebDriver driver, WebDriverWait wait) {
            this.driver = driver;
            this.wait = wait;
        }

        public virtual void Load() {
            // We ensure a page is (mostly) loaded when X relevant element is visible
            // This element will probably be a header
        }

        public HomePage MoveToHome() {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPATH_HOME))).Click();
            HomePage home = new HomePage(driver, wait);
            home.Load();
            return home;
        }

        public CartPage MoveToCart() {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_CART))).Click();
            CartPage cartPage = new CartPage(driver, wait);
            cartPage.Load();
            return cartPage;
        }
    }
}
