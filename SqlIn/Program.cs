﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Sqlite;
using static NewIn.Methods;

namespace NewIn
{
    class Program
    {
        static void Main(string[] args)
        {

        /*
        var product1 = new Product(){
            productName         = "Electrical Razor",
            brand               = "Braun",
            productPrice        = 80,
            productQuantity     = 10
        };

        var product2 = new Product(){
            productName         = "Electrical Razor",
            brand               = "Philips",
            productPrice        = 75,
            productQuantity     = 10
        };

        var product3 = new Product();
            product3.productName       = "CD";
            product3.brand             = "Sony";
            product3.productPrice      = 10.99M;
            product3.productQuantity   = 10;
            
        var prod = new List<Product>();
        prod.Add(product1);
        prod.Add(product2);
        prod.Add(product3);*/
        var prod = new List<Product>();
        Console.WriteLine("\n\tPress <ESC> when finished ...\n\tEnter 'exit' to abort.");
        
        //<!-- ADD NEW INVENTORY -->
        // Inventory will be stored in DB AFTER operations are done
        do {
            var pr = new Product();
            var tempList = new List<Product>();
                Console.WriteLine("Add to inventory: ");
                string input = Console.ReadLine();
                    if(string.IsNullOrEmpty(input)) break;
                Console.Clear();
                if(input == "exit"){
                    break;}
                else{
                    string[] temp = input.Split(",");
                    pr.productName = temp[0];
                    pr.brand = temp[1];
                    pr.productPrice = Decimal.Parse(temp[2],
                                              NumberStyles.Float, 
                                              CultureInfo.InvariantCulture);
                    pr.productQuantity = Int32.Parse(temp[3]);
                    tempList.Add(pr);
                    prod.AddRange(tempList);

                    Console.WriteLine("Current inventory");
                    ListTheProducts(prod);
                } // end: do-loop
            }while(Console.ReadKey(true).Key != ConsoleKey.Escape); // req windows?
        //<!-- END: ADD NEW INVENTORY -->

            Console.Clear();
            Console.Write("Would you like to change anything in the inventory? [Y/n] ");
            string answ = Convert.ToString(Console.ReadLine()).ToLower();

            if(answ == "y"){
                Console.WriteLine("Your current inventory.\n");
                ListTheProducts(prod);

                Console.WriteLine("Type remove to remove item, or edit to edit: ");
                string act = Convert.ToString(Console.ReadLine()).ToLower();
                
                if(act == "remove"){
                    Console.WriteLine("Please enter the index of the item you want to remove: ");
                    int rem = Convert.ToInt32(Console.ReadLine());
                    rem -= 1;
                    removeItem(prod, rem);
                } // end: if-remove
                else{
                    Console.WriteLine("You chose to edit an item.");
                    
                    foreach(Product ob in prod)
                        Console.WriteLine($"{ob.productName}");
                    Console.WriteLine("Enter index of item to edit: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    index -= 1;

                    Console.WriteLine("Commands (only one): name, brand, price, quantity.\n\t");
                    string ans = Convert.ToString(Console.ReadLine()).ToLower();

                    if     (ans == "name")      editName(prod,      index);
                    else if(ans == "brand")     editBrand(prod,     index);
                    else if(ans == "price")     editPrice(prod,     index);
                    else if(ans == "quantity")  editQuantity(prod,  index);
                    else                        Console.WriteLine("Unknown command");
                }
                
            } // end: if-yes
            else
                if(answ == "n"){
                    if ( (prod != null) && (prod.Any()) )
                    {
                        try
                        {
                        AddProduct(prod);
                        Console.WriteLine("Products successfully added to database!");
                        AddedInventoryValue(prod);
                        }
                        catch(Exception ex){
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                    }else
                        Console.WriteLine("No new inventory added");
                }else
                    Console.WriteLine("Unknown command");
                    
            
        // NEWLINE
        
        } // MAIN CLASS
    } // program class
} // namespace
