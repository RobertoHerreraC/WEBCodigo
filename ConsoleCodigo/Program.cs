using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using ConsoleCodigo;
using System.ComponentModel.DataAnnotations;

class Program
{
    static async Task Main(string[] args)
    {
        await InsertProduct();
        Console.Read();

    }


    private static async Task InsertProduct()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "https://localhost:7227/api/Products/Insert";

            var product = new
            {
                productID = 0,
                price = 12.50,
                name = "Test",
                isActive = true,
                createDate = DateTime.Now
            };
            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Product inserted successfully.");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }


    private static async Task GetCustomers()
    {
        using (HttpClient client = new HttpClient())
        {
            // URL del servicio
            string url = "https://localhost:7227/api/Customers/GetByFilters";

            //Guardar response
            HttpResponseMessage response = await client.GetAsync(url);

            // Verificar si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Leer y deserializar el contenido de la respuesta
                List<Customer> customers = await response.Content.ReadFromJsonAsync<List<Customer>>();

                foreach (var customer in customers)
                {
                    Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.Name}, Document: {customer.DocumentNumber}, Type: {customer.DocumentType}, Active: {customer.IsActive}");
                }

            }
            else
            {
                Console.WriteLine("Llamada con error");

            }
        }
    }

}
