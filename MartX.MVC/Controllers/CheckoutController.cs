using System.Text.Json.Serialization;
using MartX.BL.DTOs.CheckoutItemDtos;
using MartX.Core.Models;
using MartX.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MartX.MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;

        public CheckoutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCheckout(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }
            Product? product = _context.Products.Find(productId);
            if (product is null)
            {
                return NotFound();
            }
            var cookieOptions = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };

            CheckoutDto checkout = GetCheckout();
            if (checkout is null)
            {
                checkout = new CheckoutDto();
            }
            CheckoutItemDto? existingCheckoutItem = checkout.Items.FirstOrDefault( p => p.ProductId == product.Id );
            if (existingCheckoutItem is null)
            {
                CheckoutItemDto checkoutItemDto = new CheckoutItemDto()
                {
                    ProductId = product.Id,
                    CheckoutId = product.Id,
                    Quantity = 1,
                    UnitPrice = product.RealPrice,
                    TotalPrice = product.SalePrice
                };

                checkout.Items.Add(checkoutItemDto);                
            }
            else
            {
                existingCheckoutItem.Quantity += 1;
            }
            var cookieCheckout = JsonConvert.SerializeObject(checkout);
            Response.Cookies.Append("Checkout", cookieCheckout, cookieOptions);
            return Ok();
        }

        public CheckoutDto GetCheckout()
        {
            var checkout = Request.Cookies["Checkout"];
            if (checkout is null)
            {
                CheckoutDto? existingCheckout =  JsonConvert.DeserializeObject<CheckoutDto>(checkout) ?? throw new Exception("Something went wrong");
                return existingCheckout;
                 
            }
            return null;
        }
    }
}
