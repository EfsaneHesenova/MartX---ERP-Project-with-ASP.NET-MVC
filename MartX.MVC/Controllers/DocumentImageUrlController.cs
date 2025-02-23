using AutoMapper;
using MartX.BL.DTOs.BrandDtos;
using MartX.BL.DTOs.DocumentImageUrlDtos;
using MartX.BL.DTOs.EmployeeDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using MartX.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    [Authorize(Roles = "Admin, Boss, Adminstrator, Worker")]
    public class DocumentImageUrlController : Controller
    {
        private readonly IDocumentImageUrlService _documentImageUrlService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public DocumentImageUrlController(IMapper mapper, IEmployeeService employeeService, IDocumentImageUrlService documentImageUrlService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
            _documentImageUrlService = documentImageUrlService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            try
            {
                ICollection<DocumentImageUrlGetDto> documents = await _documentImageUrlService.GetAllDocumentImageUrlAsync();
                return View(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create(Guid employeeId)
        {
            try
            {
                EmployeeGetDto dto = await _employeeService.GetByIdEmployeeAsync(employeeId);
                Employee employee = _mapper.Map<Employee>(dto);
                DocumentImageUrlPostDto documentImageUrlPostDto = new DocumentImageUrlPostDto()
                {
                    EmployeeId = employeeId,
                    Employee = employee
                };

                return View(documentImageUrlPostDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentImageUrlPostDto documentImageUrlPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(documentImageUrlPostDto);
            }
            try
            {
                await _documentImageUrlService.CreateDocumentImageUrlAsync(documentImageUrlPostDto);
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

                DocumentImageUrlGetDto documentImageUrlGet = await _documentImageUrlService.GetByIdDocumentImageUrlAsync(id);
                DocumentImageUrlPutDto document = _mapper.Map<DocumentImageUrlPutDto>(documentImageUrlGet);
                return View(document);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(DocumentImageUrlPutDto documentImageUrlPut)
        {
            if (!ModelState.IsValid)
            {
                var employee = await _employeeService.SelectAllEmployee();
                return View(documentImageUrlPut);
            }
            try
            {
                await _documentImageUrlService.UpdateDocumentImageUrlAsync(documentImageUrlPut);
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

                DocumentImageUrlGetDto documentImageUrlGet = await _documentImageUrlService.GetByIdDocumentImageUrlAsync(id);
                return View(documentImageUrlGet);

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

                await _documentImageUrlService.SoftDeleteDocumentImageUrlAsync(id);
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

                await _documentImageUrlService.RestoreDocumentImageUrlAsync(id);
                return RedirectToAction("Index");

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
                await _documentImageUrlService.DeleteDocumentImageUrlAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
