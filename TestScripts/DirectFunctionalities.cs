﻿using Assignment_FirstCry.BaseClasses;
using Assignment_FirstCry.PageObjects;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OfficeOpenXml.Drawing.Chart.ChartEx;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V85.HeadlessExperimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
 
namespace Assignment_FirstCry.TestScripts
{
   
    public class DirectFunctionalities : BaseClass
    {
        private HomePage homePage;
        private ResultPage resultPage;
        private ProductPage productPage;
        private CartPage cartPage;
        private SupportPage supportPage;
        private CommonPage commonPage;

        // 1. Search for Products - Valid Search
     
        [Test]
        
        public void ValidSearchKeyword()
        {
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey"]);
            ClassicAssert.IsTrue(resultPage.VerifySearchResults());

        }

        // 2. Apply Filters on Search Results - Valid Filters

        [Test] 
       
        public void ValidFilter()
        {
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey"]);
            resultPage.ApplyBrandFilter(dataFromExcel["BandFilter"]);
            Thread.Sleep(3000);
            ClassicAssert.IsTrue(resultPage.VerifyResultsForSpecificValue(dataFromExcel["BandFilter"]));
        }

        // 3. Sort Search Results - Valid Sorting
        [Test] 
        public void ValidSorting()
        {
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey"]);
            resultPage.SelectSortPriceOption("Price");
            ClassicAssert.IsTrue(resultPage.VerifyResultsSortingPriceOrder("asc"));
        }

        // 4. View Product Details
        [Test] 
        public void ValidateProdDetails()
        {
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey"]);
            productPage = resultPage.OpenFirstProductLink();
            WindowHandling();
            ClassicAssert.IsTrue(productPage.ValidateProductDetails());
        }
        // 5. Add Product to Cart
        
       [Test] 
        public void ValidateAddToCart()
        {
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey2"]);
            productPage = resultPage.OpenFirstProductLink();
            WindowHandling();
            productPage.AddToCart();
            ClassicAssert.IsTrue(productPage.IsItemAddedToCart());
        }
        // 6. View Cart
        [Test] 
        public void ValidateCartDetails()
        {
            List<string> listOfProds = new List<string>();
            List<string> cartProducts = new List<string>();

            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey2"]);
            listOfProds = resultPage.OpenNumberofProducts(3);
            productPage = new ProductPage();
            productPage.AddToCartMutipleProd();
            Thread.Sleep(1000);
            cartPage = resultPage.OpenCart();
            cartProducts = cartPage.ValidateCartProducts(listOfProds);
            ClassicAssert.AreEqual(listOfProds.OrderBy(x => x), cartProducts.OrderBy(y => y));
        }

        // 7. Remove Product from Cart
        
       [Test] 
        public void RemoveProductfromCart()
        {
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey2"]);
            productPage = resultPage.OpenFirstProductLink();
            WindowHandling();
            productPage.AddToCart();
            Thread.Sleep(2000);
            cartPage = resultPage.OpenCart();
            cartPage.RemoveFromCart();
            ClassicAssert.IsTrue(cartPage.VerifyCartRemoval());
        }

        // 8. Save Product for Later
        [Test] 
        public void MoveProductToShortList()
        {

            NavigateToUrl(dataFromExcel["ProductLink"]);
            productPage = new ProductPage();
            productPage.AddToCart();
            cartPage = productPage.OpenCart();
            cartPage.MoveToShortList();
            ClassicAssert.IsTrue(cartPage.VerifyMoveToShortList());
        }
        // 9. Check Product Availability by Location
        
       [Test] 
        public void CheckProductAvailabilitybyLocation()
        {
            //    homePage = new HomePage();
            //    resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey2"]);
            //    productPage = resultPage.OpenFirstProductLink();
            NavigateToUrl(dataFromExcel["ProductLink1"]);
            productPage = new ProductPage();
            //WindowHandling();
            productPage.EnterPinCode(dataFromExcel["ValidPincode"]);
            ClassicAssert.IsTrue(productPage.IsDeliveryAvaiable());
        }

        // 11. View Customer Reviews and Ratings
        [Test] 
        public void ViewCustomerReviewandRatings()
        {

            NavigateToUrl(dataFromExcel["ProductLink1"]);
            productPage = new ProductPage();
            productPage.OpenReviews();
            ClassicAssert.IsTrue(productPage.VerifyRatingsandReviews());
        }

        // 12. Browse Categories
        [Test] 
        public void BrowseCategories()
        {
            commonPage = new CommonPage();
            commonPage.SelectCategory("Footwear");
            ClassicAssert.IsTrue(commonPage.GetCategoryresults().Contains("Footwear"));
        }

        // 13. Check Offers and Discounts
        [Test] 
        public void CheckOffersandDiscounts()
        {
            NavigateToUrl(dataFromExcel["ProductLink"]);
            productPage = new ProductPage();
            ClassicAssert.True(productPage.VerifyOffers());
        }

        // 14. View Product Images

        [Test] 
        public void ProductImage()
        {
            NavigateToUrl(dataFromExcel["ProductLink"]);
            productPage = new ProductPage();
            ClassicAssert.True(productPage.IsImageDisplayed());
        }

        // 15. Access Help and Support
        [Test] 
        public void AccessHelpandSupport()
        {
            homePage = new HomePage();
          homePage.SupportClik();
            WindowHandling();
           supportPage= new SupportPage();
           ClassicAssert.That(supportPage.getEmailForContact(), Is.EqualTo("customercare@firstcry.com"));
        }

        // 16. Check Size Guide
        [Test] 
        public void SizeGuide()
        {

            NavigateToUrl(dataFromExcel["ProductLink"]);
            productPage = new ProductPage();
            productPage.OpenSizeChart();
            SwitchToFrame("popiframe");
            //productPage.SelectSizeOptions();
            ClassicAssert.IsTrue(productPage.VerifySizeChart());
        }

        // 17. Product Q&A Section
        
       [Test] 
        public void ProductQASection()
        {
            NavigateToUrl(dataFromExcel["ProductWithQA"]);
            productPage = new ProductPage();
            ClassicAssert.True(productPage.VerifyQA());
        }

        // 19. Navigate Using Breadcrumbs
        [Test] 
        public void NavigateUsingBreadcrumbs()
        {
            commonPage = new CommonPage();
            commonPage.SelectCategory("Footwear");
            resultPage = new ResultPage();
            productPage = resultPage.OpenFirstProductLink();
            WindowHandling();
            ClassicAssert.IsTrue(commonPage.VerifyBreadCrumb());
        }

        // 20. Dynamic Cart Summary
        [Test] 
        public void DynamicCartSummary()
        {
            List<string> listOfProds = new List<string>();
            List<string> cartProducts = new List<string>();
            List<double> priceList = new List<double>();
            double totalPrice = 0;
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey3"]);
            listOfProds = resultPage.OpenNumberofProducts(3);
            priceList = resultPage.GetPriceOfProducts(3);
            totalPrice = Math.Round(priceList.Sum());
            productPage = new ProductPage();
            productPage.AddToCartMutipleProd();
            Thread.Sleep(2000);
            cartPage = productPage.OpenCart();
            cartProducts = cartPage.ValidateCartProducts(listOfProds);
            ClassicAssert.That(cartPage.GetOrderTotal(), Is.GreaterThanOrEqualTo(totalPrice));
            ClassicAssert.AreEqual(listOfProds.OrderBy(x => x), cartProducts.OrderBy(y => y));
        }

        // 21. Dynamic Price Updates
        [Test] 
        public void PriceUpdate()
        {
            NavigateToUrl(dataFromExcel["ProductLink"]);
            productPage = new ProductPage();
            string price = productPage.PriceofProduct();
            Thread.Sleep(2000);
            string newprice = productPage.PriceAfterUpdate();

            ClassicAssert.That(price, Is.EqualTo(newprice));
        }

        // 22.	Dynamic Review and Rating Updates
        [Test] 
        public void DynamicReview()
        {

            NavigateToUrl(dataFromExcel["ProductLink1"]);
            productPage = new ProductPage();
            productPage.OpenReviews();
            ClassicAssert.IsTrue(productPage.VerifyLatestReviews());
        }

        // 23. Dynamic Cart Total Calculation
        
       [Test] 
        public void DynamicCartTotal()
        {
            List<string> cartProducts = new List<string>();
            List<double> priceList = new List<double>();
            double totalPrice = 0;
            homePage = new HomePage();
            resultPage = homePage.EnterSearchText(dataFromExcel["ValidSearchKey2"]);
            resultPage.OpenNumberofProducts(3);
            priceList = resultPage.GetPriceOfProducts(3);
            totalPrice = Math.Round(priceList.Sum());
            productPage = new ProductPage();
            productPage.AddToCartMutipleProd();
            Thread.Sleep(2000);
            cartPage = productPage.OpenCart();
            cartPage.SelectQtyFromCart();
            ClassicAssert.That(cartPage.GetOrderTotal(), Is.GreaterThanOrEqualTo(totalPrice));
          
        }

        // 24. Real-time Delivery Date Estimation
        
       [Test] 
        public void RealtimeDeliveryDateEstimation()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            NavigateToUrl(dataFromExcel["ValidProductUrl"]);
            productPage = new ProductPage();
            Thread.Sleep(2000);
            productPage.EnterPin(dataFromExcel["ValidPincode"]);

            string pattern = @"^(Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday), (Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec) [0-3][0-9]$";
            bool isMatch = Regex.IsMatch(productPage.GetDate(), pattern);
            ClassicAssert.IsTrue(isMatch);
        }


        // 27. Dynamic Search Suggestions
        
       [Test] 
        public void DynamicSearchSuggestions()
        {
            List<string> suggestions = new List<string>();
            homePage = new HomePage();
            suggestions = homePage.SearchSuggestions(dataFromExcel["ValidSearchKey4"]);

            ClassicAssert.IsNotNull(suggestions);
        }

        // 28. Dynamic Category Update Based on Location
        [Test] 
        public void PincodeHomePage()
        {
            homePage = new HomePage();
            homePage.SelectLocation(dataFromExcel["ValidPincode"]);
            ClassicAssert.That(homePage.PincodeInHomepage(), Is.EqualTo(dataFromExcel["ValidPincode"]));

        }

        // 29. Dynamic Inventory Alerts
        [Test] 
        public void DynamicInventoryAlerts()
        {
            NavigateToUrl(dataFromExcel["OutofStockProduct"]);
            productPage = new ProductPage();
            productPage.NotifyUser("sdfsdfsdr", "dfg3343fgf");
        }

        // 34. Dynamic Product Recommendations
        
       [Test] 
        public void ProductRecommendations()
        {
            NavigateToUrl(dataFromExcel["ProductLink"]);
            productPage = new ProductPage();
            ClassicAssert.That(productPage.RecommondationsText(), Is.EqualTo("FREQUENTLY BOUGHT TOGETHER"));

        }

        // 35. Search for Products - Invalid Search
        [Test] 
        public void InValidSearchKeyword()
        {
            homePage = new HomePage();
            resultPage =  homePage.EnterSearchText(dataFromExcel["InvalidSearchKey"]);
            ClassicAssert.That(resultPage.InvalidResult(), Is.EqualTo("Please make sure the spelling is correct or try a different search term"));

        }

        // 37. Add Out-of-Stock Product to Cart
        [Test] 
        public void AddOutofCartProduct()
        {
            NavigateToUrl(dataFromExcel["OutofStockProduct"]);
            productPage = new ProductPage();
            string msg = productPage.VerifyOutOfStockProduct();
            ClassicAssert.That(msg, Is.EqualTo("OUT OF STOCK"));
        }

        // 38. View Product Details - Invalid Product
        [Test] 
        public void InvalidProduct()
        {
            NavigateToUrl(dataFromExcel["InvalidUrl"]);

            InvalidPage invalidPage = new InvalidPage();
            ClassicAssert.That(invalidPage.GetPageText(), Is.EqualTo("Oops... Page Not Found..."));

        }

        // 39. Check Product Availability - Invalid PIN Code
        
       [Test] 
        public void InvalidPincode()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
            NavigateToUrl(dataFromExcel["ValidProductUrl"]);
            productPage = new ProductPage();
            Thread.Sleep(3000);
            productPage.EnterPin(dataFromExcel["InvalidPincode"]);
            Thread.Sleep(3000);
            ClassicAssert.That(productPage.GetErrorText(), Is.EqualTo("Please enter valid Pincode"));
        }

        // 45. Dynamic Data Corruption During Checkout
        
       [Test] 
        public void Corrupt()
        {
            homePage = new HomePage();
            homePage.SelectLocation(dataFromExcel["ValidPincode"]);
            NavigateToUrl(dataFromExcel["ProductLink2"]);          
            productPage = new ProductPage();
            productPage.ChangeQtyTo10();
            Thread.Sleep(10000);
            productPage.AddToCart();
            Thread.Sleep(2000);
            productPage.OpenCart();
            //Thread.Sleep(10000);

        }
        // 49. Dynamic Delivery Address Validation
        [Test] 
        public void DeliveryAddressValidation()
        {
            homePage = new HomePage();
            homePage.SelectLocation(dataFromExcel["ValidPincode"]);
            NavigateToUrl(dataFromExcel["ProductLink2"]);
            productPage = new ProductPage();
            productPage.AddToCart();
            Thread.Sleep(2000);
            productPage.OpenCart();
            cartPage = new CartPage();
            string olddate = cartPage.GetDate();
            cartPage.PinCodechange(dataFromExcel["ValidPincodeRural"]);
            Thread.Sleep(3000);
            string newdate = cartPage.GetDate();
            ClassicAssert.That(olddate, Is.Not.EqualTo(newdate));
        }


        // 53. Dynamic Navigation Bar Updates
        
       [Test] 
        public void NavigationBars()
        {
            homePage = new HomePage();
            Thread.Sleep(5000);
            homePage.MovetoBoysSuites();
            Thread.Sleep(1000);
            ClassicAssert.That(homePage.IsHomePageLinkPresent(), Is.EqualTo("Home"));
            ClassicAssert.That(homePage.IsClothesLinkPresent(), Is.EqualTo("Clothes & Shoes"));
        }

        // 54. Real-time Stock Alerts for Subscribed Products
        [Test] 
        public void StockAlertPresenceForOutofStock()
        {
            NavigateToUrl(dataFromExcel["OutofStockProduct"]);
            productPage = new ProductPage();
            ClassicAssert.That(productPage.StockAlertPresence(), Is.EqualTo("Notify me by Email & Mobile when the product is in stock"));
        }

        // 55. Dynamic Content Personalization
        [Test] 
        public void ContentPersonalization()
        {
            NavigateToUrl(dataFromExcel["ProductLink"]);
            NavigateToUrl(dataFromExcel["ProductLink1"]);
            NavigateToUrl(dataFromExcel["ProductLink2"]);
            productPage = new ProductPage();
            ClassicAssert.That(productPage.PersonalizedText(), Is.EqualTo("YOU MAY ALSO LIKE"));
        }

        // 56. Real-time Update of User Reviews
        [Test] 
        public void RealTimeUserReviews()
        {

            NavigateToUrl(dataFromExcel["ProductLink1"]);
            productPage = new ProductPage();
            productPage.OpenReviews();
            ClassicAssert.IsTrue(productPage.VerifyLatestReviews());
        }

        // 57. Dynamic Shipping Cost Calculation
       
        
       [Test] 
        public void ShippingCost()
        {
            Thread.Sleep(1000);
            _driver.Manage().Cookies.DeleteAllCookies();
            NavigateToUrl(dataFromExcel["ProductLink"]);
            productPage = new ProductPage();
            Thread.Sleep(2000);
            productPage.AddToCart();
            Thread.Sleep(2000);
            productPage.OpenCart();
            cartPage = new CartPage();
            string SCostBefore = cartPage.ShippingCost();
            NavigateToUrl(dataFromExcel["ProductLink1"]);
            productPage.AddToCart();
            productPage.OpenCart();
            string SCostAfter = cartPage.ShippingCostAfter();
            ClassicAssert.That(SCostBefore, Is.Not.EqualTo(SCostAfter));
        }


        // 58. Dynamic Update of Promotion Banners
        [Test] 
        public void BannerTest()
        {

            homePage = new HomePage();
           // homePage.MovetoBoysFashion();
            Thread.Sleep(15000);

            resultPage = new ResultPage();
            List<string> altTags = resultPage.GetAllAltTags();

            ClassicAssert.IsNotEmpty(altTags);

        }

        


    }
}
