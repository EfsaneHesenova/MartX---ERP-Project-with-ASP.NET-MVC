using System.Drawing;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.ProductDtos;
using MartX.BL.DTOs.SupplierDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SupplierController(IMapper mapper, ISupplierService supplierService)
        {
            _mapper = mapper;
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int size = 10, int page = 1)
        {
            try
            {
                ViewBag.Size = size;
                int currentPage = page - 1;

                ICollection<SupplierGetDto> suppliers = await _supplierService.GetAllSupplier(size, currentPage);
                ICollection<SupplierGetDto> allSuppliers = await _supplierService.GetAllSupplier();
                int totalItems = allSuppliers.Count();
                var totalPages = (int)Math.Ceiling((double)totalItems / size);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;

                return View(suppliers);
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
        public async Task<IActionResult> Create(SupplierPostDto supplierPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierPostDto);
            }
            try
            {
                await _supplierService.CreateSupplierAsync(supplierPostDto);
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
                SupplierGetDto supplierGetDto = await _supplierService.GetByIdSupplierAsync(id);
                SupplierPutDto supplierPutDto = _mapper.Map<SupplierPutDto>(supplierGetDto);
                return View(supplierPutDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(SupplierPutDto supplierPutDto)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierPutDto);
            }
            try
            {
                await _supplierService.UpdateSupplierAsync(supplierPutDto);
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
                SupplierGetDto supplierGetDto = await _supplierService.GetByIdSupplierAsync(id);
                return PartialView("_Detail", supplierGetDto);
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
                await _supplierService.DeleteSupplierAsync(id);
                return RedirectToAction("Index");
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
                await _supplierService.SoftDeleteSupplierAsync(id);
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
                await _supplierService.RestoreSupplierAsync(id);
                return RedirectToAction("Trash");
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
                ICollection<SupplierGetDto> suppliers = await _supplierService.GetAllSoftDeletedSupplier();
                return View(suppliers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
