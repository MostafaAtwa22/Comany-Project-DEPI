using DEPI_Final_Project.Models;
using DEPI_Final_Project.Models.Enums;
using DEPI_Final_Project.Repositories.Interfaces;
using DEPI_Final_Project.ViewModels;
using DEPI_Final_Project.ViewModels.DepartmentVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentController(IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var department = _departmentRepository.GetById(id);

            if (department is null)
                return NotFound();

            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateDepartmentVM model = new CreateDepartmentVM
            {
                ManagersSelectList = _employeeRepository.GetUnassignedManagers()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.ManagersSelectList = _employeeRepository.GetUnassignedManagers();
                return View(model);
            }

            await _departmentRepository.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _departmentRepository.GetById(id);

            if (department is null)
                return NotFound();

            EditDepartmentVM model = new EditDepartmentVM
            {
                Id = id,
                Name = department.Name,
                Location = department.Location,
                ManagerId = department.ManagerId,
                ManagersSelectList = _employeeRepository.GetManagersForEdit(department.ManagerId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDepartmentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.ManagersSelectList = _employeeRepository.GetManagersForEdit(model.ManagerId);
                return View(model);
            }

            var department = await _departmentRepository.Update(model);

            if (department is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var isDeleted = _departmentRepository.Delete(id);

            if (!isDeleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
