using System.Drawing;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.EmployeeDtos;
using MartX.BL.DTOs.ProductDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    [Authorize(Roles = "Admin, Boss, Adminstrator, Worker")]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int size = 10, int page = 1)
        {
            try
            {
                ViewBag.Size = size;
                int currentPage = page - 1;

                ICollection<ProductGetDto> products = await _productService.GetAllProduct(size, currentPage);
                ICollection<ProductGetDto> allProducts = await _productService.GetAllProduct();
                int totalItems = allProducts.Count();
                var totalPages = (int)Math.Ceiling((double)totalItems / size);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var brand = await _brandService.SelectAllBrand();
                var category = await _categoryService.SelectAllCategory();
                ProductPostDto productPostDto = new ProductPostDto();
                productPostDto.Brands = brand;
                productPostDto.Categories = category;
                return View(productPostDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDto productPostDto)
        {
            if (!ModelState.IsValid)
            {
                var brand = await _brandService.SelectAllBrand();
                var category = await _categoryService.SelectAllCategory();
                productPostDto.Brands = brand;
                productPostDto.Categories = category;   
                return View(productPostDto);
            }
            try
            {
                await _productService.CreateProductAsync(productPostDto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            try
            {

                ProductGetDto productGetDto = await _productService.GetByIdProductAsync(id);
                ProductPutDto productPutDto = _mapper.Map<ProductPutDto>(productGetDto);
                var brand = await _brandService.SelectAllBrand();
                var category = await _categoryService.SelectAllCategory();
                productPutDto.Brands = brand;
                productPutDto.Categories = category;
                return View(productPutDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductPutDto productPutDto)
        {
            if (!ModelState.IsValid)
            {
                var brand = await _brandService.SelectAllBrand();
                var category = await _categoryService.SelectAllCategory();
                productPutDto.Brands = brand;
                productPutDto.Categories = category;
                return View(productPutDto);
            }
            try
            {
                await _productService.UpdateProductAsync(productPutDto);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {

                ProductGetDto productGetDto = await _productService.GetByIdProductAsync(id);
                return PartialView("_Detail", productGetDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            try
            {

                await _productService.SoftDeleteProductAsync(id);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Restore(Guid id)
        {
            try
            {

                await _productService.RestoreProductAsync(id);
                return RedirectToAction("Trash");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Boss, Adminstrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            try
            {
                ICollection<ProductGetDto> products = await _productService.GetAllSoftDeletedProduct();
                return View(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
