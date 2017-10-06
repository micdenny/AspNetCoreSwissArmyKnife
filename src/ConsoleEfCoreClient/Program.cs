using System;
using EfCoreApp.Sdk;
using Newtonsoft.Json;

namespace ConsoleEfCoreClient
{
    // autorest --input-file=http://localhost:56485/swagger/v1/swagger.json --csharp --namespace=EfCoreApp.Sdk --output-folder=EfCoreApp.Sdk
    class Program
    {
        static void Main(string[] args)
        {
            var api = new ASPNETCoreEntityFrameworkApi(new Uri("http://localhost:56485"));

            Console.WriteLine("Removing all the items from the cart...");

            {
                var cart = api.GetCart();
                if (cart != null)
                {
                    foreach (var item in api.GetCart().Items)
                    {
                        api.RemoveItemFromCart(item.ProductId);
                    }
                }
            }

            Console.WriteLine("Adding 1 tv and 5 dash buttons to the cart...");

            {
                // 1 tv
                api.AddItemToCart(1, 1);

                // 5 dash button
                api.AddItemToCart(2, 2);
                api.AddItemToCart(2, 1);
                api.AddItemToCart(2, 1);
                api.AddItemToCart(2, 1);
            }

            Console.WriteLine("Cart has been updated:");

            {
                var cart = api.GetCart();

                Console.WriteLine(JsonConvert.SerializeObject(cart, Formatting.Indented));

                Console.WriteLine();
                Console.Write("Do you want to buy it (y/n)? ");
                var response = Console.ReadLine();

                if (response.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    api.BuyCart();
                    Console.WriteLine("All the items in the cart has been purchased!");
                }
                else
                {
                    Console.WriteLine("Ok, as you wish.");
                }
            }

            Console.WriteLine("Bye bye!");
        }
    }
}
