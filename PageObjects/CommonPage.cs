using Assignment_FirstCry.BaseClasses;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_FirstCry.PageObjects
{
    [Parallelizable(ParallelScope.All)]
    public class CommonPage : BaseClass
    {

        [FindsBy(How = How.XPath, Using = "//h1[@class = 'topl lft M18_42']")]
        private IWebElement resultsHeading;

        [FindsBy(How = How.XPath, Using = "//a[@itemprop='breadcrumb']")]
        private IList<IWebElement> commonBreadcrumb;

        public CommonPage()
        {
            PageFactory.InitElements(_driver, this);
        }

        //select Category from the header dynamically
        public void SelectCategory(string cat)
        {
            //can not pass dynamic value between xapth using page factory
            _driver.FindElement(By.XPath("//div[@class='menu-container']//a[contains(text(),'" + cat + "')]")).Click();
        }

        //Gives the Heading in results page
        public string GetCategoryresults()
        {
            return resultsHeading.Text;
        }

        //Validated Breadcrumb Navigation
        public bool VerifyBreadCrumb()
        {

            foreach (var breadcrumb in commonBreadcrumb.Reverse())
            {
                string categoryType = breadcrumb.Text;
                breadcrumb.Click();
                Thread.Sleep(1000);
                if (resultsHeading.Text.Contains(categoryType))
                {
                    return true;
                }
            }
            return false;
        }

        //Converts products list or price to respective object
        public static dynamic ConvertToDynamicList(IList<IWebElement> results, string type)
        {
            List<string> listOfProdNames = new List<string>();
            List<double> priceListOfProdDecimal = new List<double>();

            List<int> priceListOfProd = new List<int>();
            if (type == "string")
            {
                for (int i = 0; i < results.Count; i++)
                {
                    string text = "";
                   
                    text= results[i].GetAttribute("title").ToString();
                    listOfProdNames.Add(text);
                }
                return listOfProdNames;
            }
            else if (type == "double")
            {
                for (int i = 0; i < results.Count; i++)
                {
                    double val = double.Parse(results[i].Text);
                    priceListOfProdDecimal.Add(val);
                }
                return priceListOfProdDecimal;
            }
            else
            {
                for (int i = 0; i < results.Count; i++)
                {
                    int val = Int32.Parse(results[i].Text.Split('.')[0].ToString());
                    priceListOfProd.Add(val);
                }
                return priceListOfProd;

            }

        }

        public static IWebElement FindElementWithRetry(By locator, int attempts = 3)
        {
            int attemptsMade = 0;
            while (attemptsMade < attempts)
            {
                try
                {
                    return _driver.FindElement(locator);
                }
                catch (NoSuchElementException)
                {
                    // Handle NoSuchElementException or StaleElementReferenceException
                    attemptsMade++;
                    Thread.Sleep(1000); // Add a small delay before retrying
                }
            }
            throw new NoSuchElementException($"Element with locator '{locator}' not found after {attempts} attempts.");
        }
    }
}
