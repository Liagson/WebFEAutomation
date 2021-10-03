using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using WebFEAutomation.demoblaze.DTOs;

namespace WebFEAutomation.demoblaze.Forms
{
    public class PlaceOrderForm
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public static readonly string XPATH_PLACE_ORDER_TITLE = "//h5[contains(text(),'Place order')]";

        private static readonly string ID_NAME = "name";
        private static readonly string ID_COUNTRY = "country";
        private static readonly string ID_CITY = "city";
        private static readonly string ID_CREDIT_CARD = "card";
        private static readonly string ID_MONTH = "month";
        private static readonly string ID_YEAR = "year";

        private static readonly string XPATH_PURCHASE_DATA= "//div[contains(@class, 'sweet-alert')]/p";
        private static readonly string XPATH_PURCHASE_BUTTON = "//button[contains(text(), 'Purchase')]";
        private static readonly string XPATH_PURCHASE_OK_BUTTON = "//button[contains(text(), 'OK')]";

        public PlaceOrderForm(IWebDriver driver, WebDriverWait wait) {
            this.driver = driver;
            this.wait = wait;
        }

        public PurchaseDTO Purchase(PlaceOrderDTO orderData) {
            FillForm(orderData);
            PurchaseDTO purchaseData = GetPurchaseData();
            AcceptPurchaseData();
            return purchaseData;
        }

        private void FillForm(PlaceOrderDTO order) {
            driver.SwitchTo().ActiveElement();

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_NAME))).Click();
            driver.FindElement(By.Id(ID_NAME)).SendKeys(order.Name);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_COUNTRY))).Click();
            driver.FindElement(By.Id(ID_COUNTRY)).SendKeys(order.Country);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_CITY))).Click();
            driver.FindElement(By.Id(ID_CITY)).SendKeys(order.City);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_CREDIT_CARD))).Click();
            driver.FindElement(By.Id(ID_CREDIT_CARD)).SendKeys(order.CreditCard);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_MONTH))).Click();
            driver.FindElement(By.Id(ID_MONTH)).SendKeys(order.Month);

            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(ID_YEAR))).Click();
            driver.FindElement(By.Id(ID_YEAR)).SendKeys(order.Year);

            TestContext.Out.WriteLine("The place order form has been filled");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPATH_PURCHASE_BUTTON))).Click();
        }

        private PurchaseDTO GetPurchaseData() {
            driver.SwitchTo().ActiveElement();
            string purchaseData = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(XPATH_PURCHASE_DATA))).Text;
            string[] splitPurchaseData = purchaseData.Split(
                new string[] { "\r\n" }, StringSplitOptions.None);

            return new PurchaseDTO { 
                Id = splitPurchaseData[0].Split(' ')[1], // "Id: 1234"
                Amount = splitPurchaseData[1].Split(' ')[1] // "Amount: 1234 USD"
            };
        }

        private void AcceptPurchaseData() {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPATH_PURCHASE_OK_BUTTON))).Click();
        }
    }
}
