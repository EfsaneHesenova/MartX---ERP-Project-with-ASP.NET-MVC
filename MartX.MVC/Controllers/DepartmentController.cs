using AutoMapper;
using MartX.BL.DTOs.CategoryDtos;
using MartX.BL.DTOs.DepartmentDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using MartX.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IMapper mapper, IDepartmentService departmentService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<DepartmentGetDto> departments = await _departmentService.GetAllDepartmentAsync();
                return View(departments);
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
        public async Task<IActionResult> Create(DepartmentPostDto departmentPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentPostDto);
            }
            try
            {
                await _departmentService.CreateDepartmentAsync(departmentPostDto);
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
                DepartmentGetDto departmentGetDto = await _departmentService.GetByIdDepartmentAsync(id);
                DepartmentPutDto departmentPutDto = _mapper.Map<DepartmentPutDto>(departmentGetDto);
                return View(departmentPutDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentPutDto departmentPutDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentPutDto);
            }
            try
            {
                await _departmentService.UpdateDepartmentAsync(departmentPutDto);
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
                DepartmentGetDto departmentGetDto = await _departmentService.GetByIdDepartmentAsync(id);
                return View(departmentGetDto);
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
                await _departmentService.SoftDeleteDepartmentAsync(id);
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
                await _departmentService.RestoreDepartmentAsync(id);
                return RedirectToAction("Trash");
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
                await _departmentService.DeleteDepartmentAsync(id);
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
                ICollection<DepartmentGetDto> departments = await _departmentService.GetAllSoftDeletedDepartment();
                return View(departments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
