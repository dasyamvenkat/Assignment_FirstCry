using Assignment_FirstCry.BaseClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.PageObjects
{
    [Parallelizable(ParallelScope.All)]
    public class ProductPage : BaseClass
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        WebDriverWait wait;

        Actions actions = new Actions(_driver);


        [FindsBy(How = How.XPath, Using = "//div[@class = 'R14_61 p-prod-desc']")]
        private IWebElement productDetails;

        [FindsBy(How = How.XPath, Using = "//div[@class = 'btn btn-add-cart add-cart']")]
        private IWebElement addToCart;

        [FindsBy(How = How.XPath, Using = "//span[contains(@class,'prodQuant')]")]
        private IWebElement cartCount;

        [FindsBy(How = How.Id, Using = "txtPincode")]
        private IWebElement pincodeField;
        
        [FindsBy(How = How.XPath, Using = "(//*[@id=\"btnPincodeSubmit\"])[1]")]
        private IWebElement submitPinCode;

        [FindsBy(How = How.Id, Using = "error-pincode")]
        private IWebElement pinCodeErrorCodeMsg;

        [FindsBy(How = How.Id, Using = "tat_msg")]
        private IWebElement getDeliveryDate;

        [FindsBy(How = How.Id, Using = "span_review")]
        private IWebElement productReview;

        [FindsBy(How = How.Id, Using = "span_rating")]
        private IWebElement productRating;
        
        [FindsBy(How = How.Id, Using = "txtQuestion")]
        private IWebElement questionField;       

        [FindsBy(How = How.XPath, Using = "//p[contains(@class,'rev-time')]")]
        private IWebElement reviewDate;
        
        [FindsBy(How = How.XPath, Using = "//*[@class=\"span-pincode M14_link\"]")]
        private IWebElement changePinCode;
        //[FindsBy(How = How.Id, Using = "sizechart")]
        //private IWebElement sizeChart;


        [FindsBy(How = How.XPath, Using = "//a[@id='sizechart']")]
        private IWebElement sizeChart;

        //[FindsBy(How = How.Id, Using = "DivWaist")]
        //private IWebElement waistSize;

        //[FindsBy(How = How.Id, Using = "DivBottomLength")]
        //private IWebElement bottomLength;

        //[FindsBy(How = How.ClassName, Using = "custom-select")]
        //private IWebElement sizeOptions;
      
        //[FindsBy(How = How.ClassName, Using = "select-items")]
        //private IWebElement selectSize;

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'GO TO CART')]")]
        private IWebElement goToCart;

        [FindsBy(How = How.XPath, Using = "//div[@class='div-out-stock-main']/p")]
        private IWebElement outOfCart;
       
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'cpn_offrs_disc_swiper')]/div/div")]
        private IList<IWebElement> offers;

        [FindsBy(How = How.XPath, Using = "//div[@class='qna-col2 R14_42']")]
        private IWebElement qaList;

        [FindsBy(How = How.Id, Using = "email_addr_value")]
        private IWebElement emailForInventory;

        [FindsBy(How = How.Id, Using = "mob_cc")]
        private IWebElement mobilenumForInventory;
        
        [FindsBy(How = How.XPath, Using = "//input[contains(@class,'btn-notif')]")]
        private IWebElement notifyButton;

        //[FindsBy(How = How.XPath, Using = "//input[@id='txtPincode']")]
        //private IWebElement pinEnterBox;

        //[FindsBy(How = How.XPath, Using = "//button[@id='btnPincodeSubmit']")]
        //private IWebElement pinSubmit;

        [FindsBy(How = How.XPath, Using = "//*[@id=\"tat_msg\"]//b")]
        private IWebElement date;

        [FindsBy(How = How.XPath, Using = "//p[@id='error-pincode']")]
        private IWebElement error;

        [FindsBy(How = How.XPath, Using = "//img[@id='big-img']")]
        private IWebElement bigImg;

        [FindsBy(How = How.XPath, Using = "//span[@id='prod_price']")]
        private IWebElement price;

        [FindsBy(How = How.XPath, Using = "//p[@class='B16_42 yml-heading p-heading']")]
        private IWebElement recom;

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'FREQUENTLY BOUGHT TOGETHER')]")]
        private IWebElement fbtg;

        [FindsBy(How = How.XPath, Using = "(//*[@class=\"R14_61 p2\"])[1]")]
        private IWebElement outOf;

        
        [FindsBy(How = How.XPath, Using = "//*[@id=\"product_qty\"]/option[5]")]
        private IWebElement qty5fromdropdown;

        public ProductPage()
        {
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            PageFactory.InitElements(_driver, this);
        }

        // perform notify user action
        public void NotifyUser(string email, string phno)
        {
            emailForInventory.SendKeys(email);
            mobilenumForInventory.SendKeys(phno);
            notifyButton.Click();
        }

        // provides Outofstock product messgage
        public string VerifyOutOfStockProduct()
        {
             return outOfCart.Text;
        }

        // Validates QA section in product details
        public bool VerifyQA()
        {
            actions.ScrollByAmount(0,5000).Perform();
            //js.ExecuteScript("window.scrollBy(0,5000)", "");
            Thread.Sleep(3000);
            // _driver.FindElement(By.Id("txtQuestion")).SendKeys("Hello");
            return qaList.Displayed;
        }

        // Verifies Offers
        public bool VerifyOffers()
        {
           return offers.Count()>0;
        }

        // Validated Size chart details
        public bool VerifySizeChart()
        {
            //selectSize.Click();
            By sizeele = By.Id("DivWaist");
            IWebElement waiseSizeele = CommonPage.FindElementWithRetry(sizeele, 3);
           //  wait.Until(ExpectedConditions.ElementToBeClickable(waistSize)).Click();
            var isCorrectWaist = waiseSizeele.Text.Contains("Waist: 21 cm");
            //IWebElement len = FindElementWithRetryPage(bottomLength, 3);
            By lenele = By.Id("DivBottomLength");
            IWebElement lenSizeele = CommonPage.FindElementWithRetry(lenele, 3);
            
            var isCorrectLength = lenSizeele.Text.Contains("Bottom Length: 45 cm");
            return isCorrectLength && isCorrectWaist;
        }

        //public void SelectSizeOptions()
        //{
        //   // sizeOptions.Click();
        //    //selectSize.Click();
        //}

        // Opens Size Chart Link
        public void OpenSizeChart()
        {
            
            sizeChart.Click();
        }

        // Enters text in QA Question Field
        public void EnterTextInQuestion(string text)
        {
            questionField.SendKeys(text);
        }
        
        // Validates Product details
        public bool ValidateProductDetails()
        {
            if (productDetails.Text.Contains("Key Features"))
            {
                return true;
            }
            return false;
        }


        // Add product to Cart
        public void AddToCart()
        {
            addToCart.Click();
        }

        //Opens cart page
        public CartPage OpenCart()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(goToCart)).Click();
            //goToCart.Click();
            return new CartPage();
        }

        // Varifies if product added to cart or not
        public bool IsItemAddedToCart()
        {
            Thread.Sleep(3000);
            //string cart = cartCount.Text.Split("\r")[0].ToString();
            
            int cartprodCount = Int32.Parse(cartCount.Text);
            if(cartprodCount==1)
            {
                return true;
            }
            return false;
        }

        // Add multiple products to cart
        public void AddToCartMutipleProd()
        {
            WindowHandlingDynamic(1);
            AddToCart();
            WindowHandlingDynamic(2);
            AddToCart();
            WindowHandlingDynamic(3);
            AddToCart();
            //CloseTab();
        }

        // Enter Pincode
        public void EnterPinCode(String pinCode)
        {
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollBy(0,2000)", "");
            js.ExecuteScript("arguments[0].click();", changePinCode);
           // changePinCode.Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(pincodeField)).SendKeys(pinCode);
            //pincodeField.SendKeys(pinCode);
            Thread.Sleep(1000);
            submitPinCode.Click();
        }

        // Check if Delivery avaiable or not
        public bool IsDeliveryAvaiable()
        {
            Thread.Sleep(5000);
            return getDeliveryDate.Text != "";
        }

        // Gives pinCode ErrorMessage
        public string ValidatePinCodeErrorMsg()
        {
           return pinCodeErrorCodeMsg.Text;
        }

        //Open Review Section
        public void OpenReviews()
        {
            Thread.Sleep(1000);
            js.ExecuteScript("window.scrollBy(0,1800)", "");
            //productRating.Click();
        }

        // Validates Rating and reviews
        public bool VerifyRatingsandReviews()
        {
            return ((productReview.Text!="") && (productRating.Text != ""));
        }

        //Validates Latest Reviews
        public bool VerifyLatestReviews()
        {
            DateTime onemonthbackdate = DateTime.Now.AddMonths(-1);
            DateTime dateTime;
            string[] formats = { "M/d/yyyy", "yyyy-MM-dd", "dd-MM-yyyy" };
            bool parsed = DateTime.TryParseExact(reviewDate.Text, formats, null,
                                                System.Globalization.DateTimeStyles.None, out dateTime);
            //DateTime userReviewDate =  DateTime.ParseExact(reviewDate.Text, "dd/MM/yyyy", null);
            if(dateTime >= onemonthbackdate)
            {
                return true;
            }
            return false;
        }

        public string GetDate()
        {
            return date.Text;
        }

        // Same as - EnterPinCode - needs to discuss
        public void EnterPin(string pin)
        {
            pincodeField.SendKeys(pin);
            Thread.Sleep(3000);
            submitPinCode.Click();
        }

        public string GetErrorText()
        {
            return error.Text;
        }
      

        public bool IsImageDisplayed()
        {
            return bigImg.Displayed;

        }

        public string PriceofProduct()
        {
            return price.Text;

        }

        public string PriceAfterUpdate()
        {
            return price.Text;

        }

        public string PersonalizedText()
        {
            actions.ScrollByAmount(0, 1800).Perform();
          //  js.ExecuteScript("window.scrollBy(0,1800)", "");
            By sectionName = By.XPath("//*[@class='B16_42 yml-heading p-heading']");
           // return wait.Until(ExpectedConditions.ElementExists(sectionName)).Text;
            return recom.Text;

        }

        public string RecommondationsText()
        {
            actions.ScrollByAmount(0, 1800).Perform();
            //js.ExecuteScript("window.scrollBy(0,1800)");
            //wait.Until(ExpectedConditions.)
            Thread.Sleep(2000);
            return fbtg.Text;

        }

        public string StockAlertPresence()
        {
            return outOf.Text;

        }

        public void ChangeQtyTo10()
        {
          
            js.ExecuteScript("arguments[0].innerText = '10'", qty5fromdropdown);


            _driver.FindElement(By.XPath("//select[@id='product_qty']")).Click();
            qty5fromdropdown.Click();

        }



    }
}
