using System.Text.Json.Serialization;
using AspNetCoreGeneratedDocument;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.CategoryDtos;
using MartX.BL.DTOs.CheckoutItemDtos;
using MartX.BL.DTOs.ProductDtos;
using MartX.BL.Services.Abstractions;
using MartX.Core.Models;
using MartX.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MartX.MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CheckoutController(AppDbContext context, IBrandService brandService, ICategoryService categoryService, IProductService productService)
        {
            _context = context;
            _brandService = brandService;
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            CheckoutDto checkoutDto = new CheckoutDto();
            ICollection<BrandGetDto> brands = await _brandService.GetAllBrand();
            ICollection<CategoryGetDto> categories = await _categoryService.GetAllCategoryAsync();
            ICollection<ProductGetDto> products = await _productService.GetAllProductAsync(); 
            CheckoutGetDto dto = new CheckoutGetDto()
            {
                CheckoutDto = checkoutDto,
                Brands = brands,
                Categories = categories,
                Products = products
            };
            return View(dto);
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
