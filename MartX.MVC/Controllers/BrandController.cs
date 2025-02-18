using System.Drawing.Drawing2D;
using System.Numerics;
using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.SupplierDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using MartX.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public BrandController(IMapper mapper, ISupplierService supplierService, IBrandService brandService)
        {
            _mapper = mapper;
            _supplierService = supplierService;
            _brandService = brandService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<BrandGetDto> brands = await _brandService.GetAllBrand();
                return View(brands);
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
                ICollection<BrandGetDto> brands = await _brandService.GetAllSoftDeletedBrand();
                return View(brands);
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
                var supplier = await _supplierService.SelectAllSupplier();
                BrandPostDto brandPostDto = new BrandPostDto();
                brandPostDto.Suppliers = supplier;
                return View(brandPostDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandPostDto brandPostDto)
        {
            if (!ModelState.IsValid)
            {
                var supplier = await _supplierService.SelectAllSupplier();             
                brandPostDto.Suppliers = supplier;
                return View(brandPostDto);
            }
            try
            {
               await _brandService.CreateBrandAsync(brandPostDto);
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

                BrandGetDto brandGetDto = await _brandService.GetByIdBrandAsync(id);
                BrandPutDto brandPutDto = _mapper.Map<BrandPutDto>(brandGetDto);
                var supplier = await _supplierService.SelectAllSupplier();
                brandPutDto.Suppliers = supplier;
                return View(brandPutDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandPutDto brandPutDto)
        {
            if (!ModelState.IsValid)
            {
                var supplier = await _supplierService.SelectAllSupplier();
                brandPutDto.Suppliers = supplier;
                return View(brandPutDto);
            }
            try
            {
                await _brandService.UpdateBrandAsync(brandPutDto);
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

                BrandGetDto brandGetDto = await _brandService.GetByIdBrandAsync(id);
                return PartialView("_Detail", brandGetDto);
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

                await _brandService.SoftDeleteBrandAsync(id);
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

                await _brandService.RestoreBrandAsync(id);
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
                await _brandService.DeleteBrandAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
