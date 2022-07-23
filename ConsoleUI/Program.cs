using DAL.DbContexts;
using System;
using System.Collections.Generic;
using Models.Entities;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var conn=new TodebCampDbContext())
            {
                var cat2 = new Category()
                {
                    Name = "Kıyafet",
                    Description = "Kıyafet kategoryleri",
                    Products = new List<Product>()
                    {
                        new Product()
                        {
                            Discontinued = false,
                            ProductDetail = "Deneme-1",
                            ProductName = "Pantolon",
                            SupplierID = 1,
                            UnitPrice = 100,
                            UnitsInStock = 10,
                            UnitsOnOrder = 5,
                            QuantityPerUnit = "3",
                            ReorderLevel = 6,
                        },
                        new Product()
                        {
                            Discontinued = false,
                            ProductDetail = "Deneme-2",
                            ProductName = "Gömlek",
                            SupplierID = 1,
                            UnitPrice = 100,
                            UnitsInStock = 10,
                            UnitsOnOrder = 5,
                            QuantityPerUnit = "3",
                            ReorderLevel = 6,
                        }
                    }
                };


                conn.Categories.Add(cat2);
                conn.SaveChanges();
            }
        }
    }
}
