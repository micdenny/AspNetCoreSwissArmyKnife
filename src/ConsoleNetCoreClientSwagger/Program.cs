using AspNetCoreSwagger.Sdk;
using AspNetCoreSwagger.Sdk.Models;
using System;
using System.Collections.Generic;

namespace ConsoleNetCoreClientSwagger
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new ASPNETCoreApi(new Uri("http://localhost:57479"));

            Console.WriteLine("------------------- SHOW -------------------");

            {
                IList<Product> products = api.GetProducts();
                Console.WriteLine($"Before {products.Count} products");
                foreach (var product in products)
                {
                    Console.WriteLine($"id = {product.Id} desc = {product.Description} price = {product.Price}");
                }
            }

            Console.WriteLine("------------------- ADD -------------------");

            {
                Console.WriteLine("Adding 10 products...");
                for (int i = 0; i < 10; i++)
                {
                    api.AddProduct(new Product
                    {
                        Description = Guid.NewGuid().ToString(),
                        Price = (i + 1) * 5.55
                    });
                }
            }

            Console.WriteLine("------------------- SHOW -------------------");

            {
                IList<Product> products = api.GetProducts();
                Console.WriteLine($"After {products.Count} products");
                foreach (var product in products)
                {
                    Console.WriteLine($"id = {product.Id} desc = {product.Description} price = {product.Price}");
                }
            }

            Console.WriteLine("------------------- DELETE ALL -------------------");

            {
                IList<Product> products = api.GetProducts();
                Console.WriteLine($"Deleting all {products.Count} products...");
                foreach (var product in products)
                {
                    api.DeleteProduct(product.Id);
                }
            }

            Console.WriteLine("------------------- ADD 3 PRODUCTS -------------------");

            {
                Console.WriteLine("Adding 3 products...");
                for (int i = 0; i < 3; i++)
                {
                    api.AddProduct(new Product
                    {
                        Description = Guid.NewGuid().ToString(),
                        Price = (i + 1) * 2.65
                    });
                }
            }

            Console.WriteLine("------------------- UPDATE ALL PRODUCTS -------------------");

            {
                IList<Product> products = api.GetProducts();
                Console.WriteLine("Update all 3 products...");
                foreach (var product in products)
                {
                    product.Description = "<updated>";
                    api.UpdateProduct(product);
                }
            }

            Console.WriteLine("------------------- SHOW -------------------");

            {
                IList<Product> products = api.GetProducts();
                Console.WriteLine($"After {products.Count} products");
                foreach (var product in products)
                {
                    Console.WriteLine($"id = {product.Id} desc = {product.Description} price = {product.Price}");
                }
            }
        }
    }
}
