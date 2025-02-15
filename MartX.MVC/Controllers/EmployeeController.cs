using AutoMapper;
using MartX.BL.DTOs.EmployeeDtos;
using MartX.BL.Services.Abstractions;
using MartX.BL.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace MartX.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeController(IMapper mapper, IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                ICollection<EmployeeGetDto> documents = await _employeeService.GetAllEmployeeAsync();
                return View(documents);
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
                var department = await _departmentService.SelectAllDepartment();
                EmployeePostDto employeePostDto = new EmployeePostDto();
                employeePostDto.Departments = department;
                return View(employeePostDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeePostDto employeePostDto)
        {
            if (!ModelState.IsValid)
            {
                var department = await _departmentService.SelectAllDepartment();
                employeePostDto.Departments = department;
                return View(employeePostDto);
            }
            try
            {
                await _employeeService.CreateEmployeeAsync(employeePostDto);
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

                EmployeeGetDto employeeGet = await _employeeService.GetByIdEmployeeAsync(id);
                EmployeePutDto document = _mapper.Map<EmployeePutDto>(employeeGet);
                var department = await _departmentService.SelectAllDepartment();
                document.Departments = department;
                return View(document);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeePutDto employeePut)
        {
            if (!ModelState.IsValid)
            {
                var department = await _departmentService.SelectAllDepartment();
                employeePut.Departments = department;
                return View(employeePut);
            }
            try
            {
                await _employeeService.UpdateEmployeeAsync(employeePut);
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

                EmployeeGetDto employeeGet = await _employeeService.GetByIdEmployeeAsync(id);
                return View(employeeGet);

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

                await _employeeService.SoftDeleteEmployeeAsync(id);
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

                await _employeeService.RestoreEmployeeAsync(id);
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
                await _employeeService.DeleteEmployeeAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
