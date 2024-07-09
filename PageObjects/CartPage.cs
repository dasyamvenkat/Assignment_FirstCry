using Assignment_FirstCry.BaseClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment_FirstCry.PageObjects
{
    public class CartPage : BaseClass
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

        WebDriverWait wait;

        [FindsBy(How = How.XPath, Using = "//div[@id='productlist']//div[@class ='J13SB_42 prod-name']/div[@class ='J13SB_42 prod-name']")]
        private IList<IWebElement> productsinCart;

        [FindsBy(How = How.Id, Using = "subtotal")]
        private IWebElement cartOrderTotal;

        [FindsBy(How = How.Id, Using = "VatCst")]
        private IWebElement gst;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'REMOVE')]")]
        private IWebElement removeFromCart;

        [FindsBy(How = How.ClassName, Using = "wraptext")]
        private IWebElement removeMsg;

        [FindsBy(How = How.ClassName, Using = "empty-cart-info")]
        private IWebElement cartInfo;

        [FindsBy(How = How.XPath, Using = "//div[@class='product_txt cl_fff']")]
        private IWebElement shortListMsg;

        [FindsBy(How = How.XPath, Using = "//div[@class='prod_img']//div[@class='prod_qty']")]
        private IWebElement qtyField;

        [FindsBy(How = How.XPath, Using = "//section[@class='qty_popup closecontentpopup pocl goTop']//span[contains(text(),'5')]")]
        private IWebElement select5Qty;

        [FindsBy(How = How.Id, Using = "activeshortlist")]
        private IWebElement moveToShortList;

        [FindsBy(How = How.XPath, Using = "//*[@class=\"J11SB_42 gt_time\"]")]
        private IWebElement getTime;

        [FindsBy(How = How.XPath, Using = "//input[@id='cty_pin']")]
        private IWebElement pinChange;

        [FindsBy(How = How.Id, Using = "apply_pin")]
        private IWebElement pinApply;

        [FindsBy(How = How.XPath, Using = "//span[@class='cart-icon J14R_42']")]
        private IWebElement shipCost;

        [FindsBy(How = How.XPath, Using = "//*[@class=\"J14R_42 cl_21 rht_val youSave\"]")]
        private IWebElement shipCostAfter;

        public CartPage()
        {
            wait =  new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            PageFactory.InitElements(_driver, this);
        }


        //Returns Visible Products Listed in Cart
        public List<string> ValidateCartProducts(List<string> addedProducts)
        {
            return ConvertToList(productsinCart);
        }

        public List<string> ConvertToList(IList<IWebElement> results)
        {
            List<string> listOfProdNames = new List<string>();

            for (int i = 0; i < results.Count; i++)
            {
                string text = results[i].Text.Replace(" ", "");
                listOfProdNames.Add(text);
            }
            return listOfProdNames;
        }


        //Move Product in Cart to ShortList
        public void MoveToShortList()
        {
            Thread.Sleep(1000);
            js.ExecuteScript("window.scrollBy(0,500)", "");
            moveToShortList.Click();
        }

        //Gets the Order total for the items exclusing GST AND fee
        public int GetOrderTotal()
        {
            Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(cartOrderTotal));
            int index = cartOrderTotal.Text.Count();
            int gST = Int32.Parse(gst.Text.Remove(gst.Text.Count() - 2));
            return Int32.Parse(cartOrderTotal.Text.Remove(index - 2)) - gST;
        }

        //Removed Item from Cart
        public void RemoveFromCart()
        {
            removeFromCart.Click();
        }


        //Verifies if Item is removed from Cart or not
        public bool VerifyCartRemoval()
        {
            return removeMsg.Text.Contains("has been removed from your cart.");
        }

        //Verifies if Item is moved to shortlist from Cart or not
        public bool VerifyMoveToShortList()
        {
            Thread.Sleep(100);
            return cartInfo.Text.Contains("Hey! No items in your cart");
        }

        //Select Qty for the product from Cart
        public void SelectQtyFromCart()
        {
            qtyField.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(select5Qty)).Click();

        }
        public string GetDate()
        {
            return getTime.Text;
        }

        public void PinCodechange(string NewPin)
        {
            pinChange.Clear();
            pinChange.SendKeys(NewPin);
            Thread.Sleep(3000);
            //By applyPin = By.Id("apply_pin");
            //wait.Until(ExpectedConditions.ElementToBeClickable(applyPin)).Click();
            Actions actions = new Actions(_driver);
            actions.MoveToElement(pinApply).Click().Build().Perform();

        }

        public string ShippingCost()
        {
            return shipCost.Text;
        }

        public string ShippingCostAfter()
        {
            return shipCostAfter.Text;
        }

    }
}
