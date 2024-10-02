using DEPI_Final_Project.Models.Enums;
using DEPI_Final_Project.Repositories.Interfaces;
using DEPI_Final_Project.ViewModels.EmployeeVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository,
            IProjectRepository projectRepository,
            IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee is null)
                return NotFound();

            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateEmployeeVM model = new CreateEmployeeVM
            {
                Departments = _departmentRepository.GetSelectList(),
                Projects = _projectRepository.GetSelectList(),
                GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _departmentRepository.GetSelectList();
                model.Projects = _projectRepository.GetSelectList();
                return View(model);
            }

            await _employeeRepository.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee is null)
                return NotFound();

            EditEmployeeVM model = new EditEmployeeVM
            {
                Id = id,
                Name = employee.Name,
                Address = employee.Address,
                CurrentImage = employee.Image,
                DepartmentId = employee.DepartmentId,
                Age = employee.Age,
                Salary = employee.Salary,
                GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList(),
                Projects = _projectRepository.GetSelectList(),
                Departments = _departmentRepository.GetSelectList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Projects = _projectRepository.GetSelectList();
                model.Departments = _departmentRepository.GetSelectList();
                model.GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                            .Select(g => new SelectListItem
                            {
                                Value = ((int)g).ToString(),
                                Text = g.ToString()
                            }).ToList();
                return View(model);
            }

            var employee = await _employeeRepository.Update(model);

            if (employee is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var isDeleted = _employeeRepository.Delete(id);

            if (!isDeleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}

