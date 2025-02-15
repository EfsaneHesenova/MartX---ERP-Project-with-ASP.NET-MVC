using AutoMapper;
using MartX.BL.DTOs.CategoryDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<CategoryGetDto> categorys = await _categoryService.GetAllCategoryAsync();
                return View(categorys);
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
                return View(categoryGetDto);
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
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
    }
}
