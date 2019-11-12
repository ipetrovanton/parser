using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using ContractParser.DBmodel;

namespace ContractParser
{
    internal class SaveToDB
    {
        internal static void SaveDB(List<string> list1,
             List<string> list2,
             List<string> list3,
             List<string> list4,
             List<string> list5,
             List<string> list6,
             List<string> list7,
             List<string> list8,
             List<string> list9)
        {
            using (DataBase context = new DataBase())
            {
                Console.WriteLine("Connect to DB Server");
                context.Database.Delete();
                Console.WriteLine("Previous DB was DELETED!");
                Console.WriteLine("Start of preparing data for saving to the DB");
                for (int i = 1; i < list1.Count; i++)
                {
                    ContractNumber number = new ContractNumber();
                    number.number = list1[i];
                    CustomerName name = new CustomerName();
                    name.Name = list2[i];
                    DateOfEnd dateOfEnd = new DateOfEnd();
                    dateOfEnd.dateOfEnd = list3[i];
                    DateOFPurchase date = new DateOFPurchase();
                    date.dateOfPurchase = list4[i];
                    PurchaseName purchaseName = new PurchaseName();
                    purchaseName.name = list5[i];
                    PurchaseType purchaseType = new PurchaseType();
                    purchaseType.type = list6[i];
                    Section section = new Section();
                    section.section = list7[i];
                    StartPrice startPrice = new StartPrice();
                    string s = list8[i];
                    try
                    {
                        s = s.Replace(", ", ".");
                        s = s.Replace(" ", "");
                        s = s.Replace("₽", "");
                        startPrice.startPrice = Convert.ToDouble(s);
                    }
                    catch
                    {
                        Console.WriteLine("**********************************\n**********************************");
                        Console.WriteLine($"It's impossible to parse {s} to Double!");
                        Console.WriteLine("**********************************\n**********************************");
                    }
                    Status status = new Status();
                    status.status = list9[i];

                    context.ContractNumbers.Add(number);
                    context.CustomerNames.Add(name);
                    context.DateOfEnds.Add(dateOfEnd);
                    context.DateOFPurchases.Add(date);
                    context.PurchaseNames.Add(purchaseName);
                    context.PurchaseTypes.Add(purchaseType);
                    context.Sections.Add(section);
                    context.StartPrices.Add(startPrice);
                    context.Statuses.Add(status);

                }
                Console.WriteLine("Data was prepared!");
                context.SaveChanges();
                Console.WriteLine("Data was saved in DB!");

                Console.WriteLine("**********************************\n**********************************");
                Console.WriteLine("All changes was saved in DataBase");
                Console.WriteLine("**********************************\n**********************************");

            }
        }
    }


}
