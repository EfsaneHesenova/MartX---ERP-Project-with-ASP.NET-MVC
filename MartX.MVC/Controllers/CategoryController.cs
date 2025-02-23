using System.Drawing;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.CategoryDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using MartX.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    [Authorize(Roles = "Admin, Boss, Adminstrator, Worker")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
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

                ICollection<CategoryGetDto> categories = await _categoryService.GetAllCategory(size, currentPage);
                ICollection<CategoryGetDto> allCategories = await _categoryService.GetAllCategory();
                int totalItems = allCategories.Count();
                var totalPages = (int)Math.Ceiling((double)totalItems / size);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryPostDto categoryPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryPostDto);
            }
            try
            {
                await _categoryService.CreateCategoryAsync(categoryPostDto);
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
                CategoryGetDto categoryGetDto = await _categoryService.GetByIdCategoryAsync(id);
                CategoryPutDto categoryPutDto = _mapper.Map<CategoryPutDto>(categoryGetDto);
                return View(categoryPutDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryPutDto categoryPutDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryPutDto);
            }
            try
            {
                await _categoryService.UpdateCategoryAsync(categoryPutDto);
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
                CategoryGetDto categoryGetDto = await _categoryService.GetByIdCategoryAsync(id);
                return PartialView("_Detail", categoryGetDto);
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
                await _categoryService.SoftDeleteCategoryAsync(id);
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
                await _categoryService.RestoreCategoryAsync(id);
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
                await _categoryService.DeleteCategoryAsync(id);
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
                ICollection<CategoryGetDto> categories = await _categoryService.GetAllSoftDeletedCategory();
                return View(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
