using AutoMapper;
using MartX.BL.DTOs.SupplierDtos;
using MartX.BL.Services.Abstractions;
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
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<SupplierGetDto> suppliers =  await _supplierService.GetAllSupplierAsync();
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
                return View(supplierGetDto);
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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
