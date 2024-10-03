using DEPI_Final_Project.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_Final_Project.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProjectRepository _projectRepository;

        public DepartmentController(IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository,
            IProjectRepository projectRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
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

        [HttpGet]
        public IActionResult GetEmployeesPerDepartment(int deptId)
        {
            var employees = _employeeRepository
                .GetAll()
                .Where(e => e.DepartmentId == deptId)
                .Select(e => new
                {
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary
                }).ToList();

            return Json(employees);
        }

        [HttpGet]
        public IActionResult GetProjectsPerDepartment(int deptId)
        {
            var projects = _projectRepository
                .GetAll()
                .Where(p => p.DepartmentId == deptId)
                .Select(p => new
                {
                    Name = p.Name,
                    Budget = p.Budget,
                    Location = p.Location,
                }).ToList();
            return Json(projects);
        }
    }
}
