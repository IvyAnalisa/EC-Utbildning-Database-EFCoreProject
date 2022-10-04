using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductEFCoreExam.Models;
using static System.Console;

namespace ProductEFCoreExam
{
    class Program
    {
        static Context context = new Context();

        static void Main(string[] args)
        {
            string username, password;
            do
            {
                WriteLine("  Enter username:");
                username = Convert.ToString(ReadLine());
                WriteLine("  Enter password:");
                password = Convert.ToString(ReadLine());

                if ((username != "Admin") || (password != "13579"))
                {
                    WriteLine("Login failed.Try again...");
                }
            } while ((username != "Admin") || (password != "13579"));
            {
                WriteLine("Login succesfully");
                bool appicationRunning = false;
                WriteLine("*******************************************************");
                WriteLine("*********    Project Arbete -Datalagring      *********");
                WriteLine("***** Namn: IVY ANALISA LA - Webutvecklare.Net 2021****");
                WriteLine("*******************************************************");
                do
                {
                    WriteLine("********************************");
                    WriteLine("***  1. Add product          ***");
                    WriteLine("***  2. Search product       ***");
                    WriteLine("***  3. Display Product List ***");
                    WriteLine("***  4. Add Category           ***");
                    WriteLine("***  5. Add Product to Category***");
                    WriteLine("***  6. Display List Category  ***");
                    WriteLine("***  7. Delete product ***");
                    WriteLine("***  8. Exit                   ***");
                    WriteLine("********************************");
                    ConsoleKeyInfo input = ReadKey(true);
                    switch (input.Key)
                    {
                        case ConsoleKey.D1:
                            AddProduct();
                            break;

                        case ConsoleKey.D2:
                            SearchProduct();
                            break;

                        case ConsoleKey.D3:
                            DisplayProduct();
                            break;

                        case ConsoleKey.D4:
                            AddCategory();
                            break;

                        case ConsoleKey.D5:
                            AddProductToCategory();
                            break;

                        case ConsoleKey.D6:
                            DisplayListCategory();
                            break;

                        case ConsoleKey.D7:
                            DeleteProduct();
                            break;

                        case ConsoleKey.D8:
                            {
                                appicationRunning = true;
                            }
                            return;
                        default:
                            WriteLine("Wrong input");
                            break;
                    }
                } while (!appicationRunning);
            }
        }

        static void AddProduct()
        {

            Product products = new Product();
            // List<Product> products = db.Products.ToList();
            do
            {

                //WriteLine("Article number:");
                // string articleNumber = ReadLine();

                WriteLine("Product Name:");

                products.ProductName = ReadLine();


                WriteLine("Description");
                products.Description = ReadLine();

                WriteLine("Price");
                products.Price = double.Parse(ReadLine());

                WriteLine("Image URl");
                products.ImageUrl = ReadLine();

                WriteLine("  Is this correct Y(es) N(o)" + "\n");


            } while (ReadKey().Key == ConsoleKey.N);

            if (products.Equals(products.ProductName) && ReadKey().Key == ConsoleKey.Y)
            {

                throw new ArgumentException(String.Format("Product exist"));
            }
            else
            {

                context.products.Add(products);
                context.SaveChanges();
                Console.WriteLine("A new product has been added." + Environment.NewLine);

            }
            Thread.Sleep(2000);


        }

        static void DisplayProduct()
        {
            {

                List<Product> products = context.products.ToList();
                WriteLine("***Product List***");
                WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}{4,-10}", "Product ID", "Name", "Price", "Description", "Image URL");
                WriteLine("-----------------------------------------------------------------------------------");
                for (int i = 0; i < products.Count; i++)
                {

                    WriteLine(products[i].ProductId + "                 " + products[i].ProductName + "                " + products[i].Price.ToString() + "                " + products[i].Description + "                 " + products[i].ImageUrl + "                 ");
                }
                context.SaveChanges();


            }

        }
        static void AddCategory()
        {

            Category categories = new Category();
            do
            {


                Console.Write("Enter a category name: ");
                categories.CategoryName = Console.ReadLine();
                WriteLine("Is this correct (Y)es or (N)o?" + "\n");
            } while (ReadKey().Key == ConsoleKey.N);
            if (ReadKey().Key == ConsoleKey.Y)
            {
                throw new ArgumentException(String.Format("Category aldready  exist"));
            }
            else
            {
                context.Categories.Add(categories);
                context.SaveChanges();
                Console.WriteLine("A new category has been added." + Environment.NewLine);

            }



            Thread.Sleep(2000);


        }
        static void SearchProduct()
        {
            WriteLine("*** 2.Search Product :***");
            WriteLine("*************************");

            WriteLine(" Write product ID   you want to search:");
            string productName = ReadLine();

            var product = FindProduct(productName);
            if (product == null)
            {
                Notify("Product is not found");
                return;
            }

            WriteLine("Result of searching: " + "\n" + product.ProductId + "   " + product.ProductName + "   " + product.Description + "   " + product.ImageUrl + "   " + product.Price + "   " + Environment.NewLine);
        }

        static Product? FindProduct(string? productName)
        => context.products.FirstOrDefault(y => y.ProductName == productName);

        static void AddProductToCategory()
        {
            WriteLine("Write category:");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string selectCategoryName = ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            var category = FindCategoryName(selectCategoryName);
            if (category == null)
            {
                Notify("Category is not found");
                return;
            }

            WriteLine("Product u wanan add");
            string selectProductByName = ReadLine();
            var product = FindProductByName(selectProductByName);
            if (product == null)
            {
                Notify("Product is not found");
                return;
            }

            category.Products.Add(product);
            context.SaveChanges();
            Notify("Product added to category");

        }
        static Category? FindCategoryName(string? selectCategoryName)
       => context.Categories.FirstOrDefault(x => x.CategoryName == selectCategoryName);

        static Product? FindProductByName(string? selectProductByName)
       => context.products.FirstOrDefault(x => x.ProductName == selectProductByName);

        private static void Notify(string message)
        {
            Clear();
            WriteLine(message);
            Thread.Sleep(2000);
        }
        static void DisplayListCategory()
        {
            // Tips - använd Include() för att eager load alla produkter som är koppalde till
            // respektive category
            List<Category> categories = context.Categories.Include(x => x.Products).ToList();
            // denna kan du ta bort
            //  List<Product> products = context.products.ToList();
            WriteLine();
            WriteLine("*****************************");
            WriteLine("List of Category ");
            WriteLine("*****************************");
            WriteLine("{0,-20}{1,-10}", "Name", "Price");
            WriteLine("-----------------------------");

            /*  for (int i = 0; i < categories.Count; i++)
              {
                  WriteLine(categories[i].CategoryName + " (" + products.Count.ToString() + ")");
                  WriteLine("__________________________");

                  // Här ska du enbart skriva ut de produkter som är kopplade till kategorin

                  foreach (var product in products)
                  {
                      WriteLine(product.ProductName + "          " + product.Price.ToString());
                  }


              }*/

            foreach (var category in categories)
            {

                WriteLine($" {category.CategoryName}        ( {category.Products.Count.ToString()}) ");


                foreach (var product in category.Products)
                {

                    WriteLine("    " + product.ProductName + "             " + product.Price);
                }

            }
        }

        static void DeleteProduct()
        {
            List<Category> categories = context.Categories.ToList();
            List<Product> products = context.products.ToList();
            WriteLine("Enter product name to delete");
            string deleteProduct = ReadLine();

            var product = DeleteProductByName(deleteProduct);
            if (product == null)
            {
                Notify("Product is not found");
                return;
            }

            context.products.Remove(product);
            context.SaveChanges();
            Notify("Product is deleted");
        }
        static Product? DeleteProductByName(string? deleteProduct)
       => context.products.FirstOrDefault(y => y.ProductName == deleteProduct);

    }
}
