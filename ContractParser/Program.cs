using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using HtmlAgilityPack;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Program is started!");
            Parser parser = new Parser();
            parser.writeData();
            parser.createTable();
            Console.ReadLine();

        }

    }

    /*
     * This class cosists of 
     * methods for parsing html-pages into arrays
     * for use it for writing in excel-table
     */
    class Parser
    {
        // Предмет закупки
        List<string> purchaseName = new List<string>() { "Предмет закупки" };

        // Начальная цена
        List<string> startPrice = new List<string> { "Начальная цена закупки" };

        // Имя Заказчика
        List<string> customerName = new List<string> { "Заказчик" };

        // Дата размещения
        List<string> dateOfPurchase = new List<string> { "Дата размещения" };

        // Дата окончания торгов
        List<string> dateOfEnd = new List<string> { "Дата окончания торгов" };

        // Номер закупки
        List<string> numberOfPurchase = new List<string> { "Номер закупки" };

        // Раздел - 223-ФЗ\44-ФЗ
        List<string> purchaisingSection = new List<string> { "Раздел 223-ФЗ/44-ФЗ" };

        // Тип закупки 
        List<string> purchaisingType = new List<string> { "Тип закупки" };

        // Статус
        List<string> status = new List<string> { "Статус" };


        // function for write parsed data into all Lists
        public void writeData()
        {
            DownloaderHtml downloader = new DownloaderHtml();
            downloader.CreateFiles();
            foreach (String fileName in downloader.nameOfFiles)
            {
                getParse(fileName);
            }
            foreach (String fileName in downloader.nameOfFiles)
            {
                File.Delete(fileName);
                Console.WriteLine(fileName + " is deleted!");
            }
        }


        void getParse(string fileName)
        {
            string pageContent = null;
            using (StreamReader sr = new StreamReader(fileName))
            {
                pageContent = sr.ReadToEnd();
            }
            var document = new HtmlDocument();
            document.LoadHtml(pageContent);

            List<string> tags = new List<string> {
                // purchaseName
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[1]/div[2]/div[1]/div[2]",
                // startPrice
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[2]/div[2]/div[2]",
                // customerName
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[1]/div[2]/div[2]/div[2]/a/text()",
                // dateOfPurchase
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[2]/div[3]/div[2]",
                // dateOfEnd
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[2]/div[3]/div[4]",
                // numberOfPurchase
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/a",
                // purchaicingSection
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[1]/div[1]/div[2]/div/span",
                // purchaisingType
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[1]/div[1]/div[2]/div/text()",
                // status
                "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/text()"
            };


            // Some contracts hav not date of end therefore we need to parse such cases differently
            string st = "/html/body/form/section[2]/div/div/div[1]/div[3]/div/div[1]/div[2]/div[3]";
            HtmlNodeCollection data = document.DocumentNode.SelectNodes(st);
            foreach (HtmlNode d in data)
            {
                string s1 = d.InnerHtml;
                Console.WriteLine(s1);
                string startDate = d.XPath + "/div[2]";
                HtmlNode node = document.DocumentNode.SelectSingleNode(startDate);
                string s = node.InnerText;
                s = s.Trim();
                dateOfPurchase.Add(s);

                if (!s1.Contains("Окончание подачи заявок"))
                {
                    dateOfEnd.Add("Дата не указана");
                    Console.WriteLine("Нет даты");
                    continue;
                }
                string endDate = d.XPath + "/div[4]";
                node = document.DocumentNode.SelectSingleNode(endDate);
                s = node.InnerText;
                s = s.Trim();
                dateOfEnd.Add(s);
            }

            // Parcing purchaseName to Array
            HtmlNodeCollection points = document.DocumentNode.SelectNodes(tags[0]);
            foreach (HtmlNode point in points)
            {
                string s = point.InnerText;
                s = s.Trim();
                purchaseName.Add(s);
            }

            // Parcing startPrice to Array
            points = document.DocumentNode.SelectNodes(tags[1]);
            foreach (HtmlNode point in points)
            {
                string s = point.InnerText;
                s = s.Trim();
                s = s.Replace("&nbsp;", " ");
                startPrice.Add(s);
            }

            // Parcing customerName to Array
            points = document.DocumentNode.SelectNodes(tags[2]);
            foreach (HtmlNode point in points)
            {
                string s = point.InnerText;
                s = s.Trim();
                customerName.Add(s);
            }


            // Parcing numberOfPurchase to Array
            points = document.DocumentNode.SelectNodes(tags[5]);
            foreach (HtmlNode point in points)
            {
                string s = point.InnerText;
                s = s.Trim();
                numberOfPurchase.Add(s);
            }

            // Parcing purchaisingSection to Array
            points = document.DocumentNode.SelectNodes(tags[6]);
            foreach (HtmlNode point in points)
            {
                string s = point.InnerText;
                s = s.Trim();
                purchaisingSection.Add(s);
            }

            // Parcing purchaisingType to Array
            points = document.DocumentNode.SelectNodes(tags[7]);
            foreach (HtmlNode point in points)
            {
                string s = point.InnerText;
                s = s.Trim();
                if (s.Length == 0)
                {
                    continue;
                }
                purchaisingType.Add(s);
            }

            // Parcing status to Array
            points = document.DocumentNode.SelectNodes(tags[8]);
            foreach (HtmlNode point in points)
            {
                string s = point.InnerText;
                s = s.Trim();
                if (s.Length == 0)
                {
                    continue;
                }
                status.Add(s);
            }



            foreach (string tag in tags)
            {
                HtmlNodeCollection links = document.DocumentNode.SelectNodes(tag);
                int i = 0;
                foreach (HtmlNode link in links)
                {
                    string s = link.InnerText;
                    s = s.Trim();
                    if (s.Length == 0)
                    {
                        continue;
                    }

                    s = s.Replace("&nbsp;", " ");
                    Console.WriteLine(i + ". {0}", s);
                    i++;
                }
            }
            Console.WriteLine(fileName + " is ready!");
        }

        Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();

        /*
         * Function for preparation excel table
         */
        public void createTable()
        {
            ex.Visible = true;
            ex.SheetsInNewWorkbook = 1;
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            sheet.Name = "Госзакупки";
            Excel.Range range1 = (Excel.Range)sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 9]];
            range1.Cells.Font.Bold = true;
            Excel.Range range = (Excel.Range)sheet.Range[sheet.Cells[1, 1], sheet.Cells[status.Count, 9]];
            range.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            range.EntireColumn.AutoFit();
            range.EntireRow.AutoFit();

            for (int i = 0; i < purchaseName.Count; i++)
            {
                sheet.Cells[i + 1, 1] = purchaseName[i];
            }
            for (int i = 0; i < startPrice.Count; i++)
            {
                sheet.Cells[i + 1, 2] = startPrice[i];
            }
            for (int i = 0; i < customerName.Count; i++)
            {
                sheet.Cells[i + 1, 3] = customerName[i];
            }
            for (int i = 0; i < dateOfPurchase.Count; i++)
            {
                sheet.Cells[i + 1, 4] = dateOfPurchase[i];
            }
            for (int i = 0; i < dateOfEnd.Count; i++)
            {
                sheet.Cells[i + 1, 5] = dateOfEnd[i];
            }
            for (int i = 0; i < numberOfPurchase.Count; i++)
            {
                sheet.Cells[i + 1, 6] = numberOfPurchase[i];
            }
            for (int i = 0; i < purchaisingSection.Count; i++)
            {
                sheet.Cells[i + 1, 7] = purchaisingSection[i];
            }
            for (int i = 0; i < purchaisingType.Count; i++)
            {
                sheet.Cells[i + 1, 8] = purchaisingType[i];
            }
            for (int i = 0; i < status.Count; i++)
            {
                sheet.Cells[i + 1, 9] = status[i];
            }

        }
    }


    /*
     * Class wich consist of methods of downloading pages 
     * and for creation files for each page from site
     */
    class DownloaderHtml
    {
        int numberOfPages = 0;
        // List of names of file for searching them
        public List<string> nameOfFiles = new List<string>();

        // create object of  Browser (Chrome)
        IWebDriver driver = new ChromeDriver();

        // Constructor for known number of pages
        public DownloaderHtml(int numberOfPages)
        {
            this.numberOfPages = numberOfPages;
        }

        // Construсtor for user which wants to enter certain number of pages
        public DownloaderHtml()
        {
            Console.WriteLine("Enter number of pages to parse (from 1 to 100):");
            this.numberOfPages = Convert.ToInt32(Console.ReadLine());
            if (this.numberOfPages <= 0 || this.numberOfPages > 100)
            {
                this.numberOfPages = 10;
                Console.WriteLine("You enter invalid value, therefore we are parcing 10 pages");
            }
        }
        // Function for creation .html-files for each pages which you want to parse
        public void CreateFiles()
        {
            Console.WriteLine("Start of file creation!");
            // Loop for downloading html-pages and writing them to files
            for (int i = 1; i <= numberOfPages; i++)
            {
                // names of files for each pages
                string filename = i + ".html";
                // uri-adress for downloading pages.
                string url =
                    "http://zakupki.gov.ru/epz/order/extendedsearch/results.html?" +
                    "searchString=&morphology=on&search-filter=&pageNumber=" +
                    i + "&sortDirection=false&recordsPerPage=_10&showLotsInfoHidden=false&fz44=on&" +
                    "fz223=on&sortBy=UPDATE_DATE&af=on&ca=on&pc=on&pa=on&placingWaysList=&placingWaysList223=" +
                    "&placingChildWaysList=&publishDateFrom=&publishDateTo=&applSubmissionCloseDate=&priceFromGeneral=" +
                    "&priceFromGWS=&priceFromUnitGWS=&priceToGeneral=&priceToGWS=&priceToUnitGWS=&currencyIdGeneral=" +
                    "-1&advancePercentFrom=&advancePercentTo=&priceContractAdvantages44=&priceContractAdvantages94=" +
                    "&requirementsToPurchase44=&orderPlacement94_0=&orderPlacement94_1=&orderPlacement94_2=&contractStageList_0=on" +
                    "&contractStageList_1=on&contractStageList_2=on&contractStageList_3=on&contractStageList=0%2C1%2C2%2C3&npaHidden=" +
                    "&restrictionsToPurchase44=&extAttSearchEnable=false&selectedExtAttrCustomerId=";

                // open link in browser
                driver.Url = url;
                // html page in string is prepared for writing in file
                string html = driver.PageSource;
                // log of writing file
                Console.WriteLine("Page number " + i + " was downloaded!");
                File.WriteAllText(filename, html);
                nameOfFiles.Add(filename);
                Console.WriteLine("File " + filename + " created");
            }
        }

    }
}