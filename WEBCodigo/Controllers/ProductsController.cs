using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WEBCodigo.Models;

namespace WEBCodigo.Controllers
{
    public class ProductsController : Controller
    {

        private readonly HttpClient _httpClient;

        public ProductsController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            string url = "https://localhost:7227/api/Products/GetByFilters";
            List<Product> products = new List<Product>();

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadFromJsonAsync<List<Product>>();
            }
            else
            {
                ViewBag.Error = $"Error: {response.StatusCode}";
            }
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product product)
        {
           
            string url = "https://localhost:7227/api/Products/Insert";

            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Product inserted successfully.";
            }
            else
            {
                ViewBag.Error = $"Error: {response.StatusCode}";
            }
            return View();            
        }
    }
}
