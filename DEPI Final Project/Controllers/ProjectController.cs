using DEPI_Final_Project.Repositories.Interfaces;
using DEPI_Final_Project.ViewModels.ProjectVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_Final_Project.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ProjectController(IProjectRepository projectRepository,
            IDepartmentRepository departmentRepository)
        {
            _projectRepository = projectRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            var projects = _projectRepository.GetAll();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = _projectRepository.GetById(id);

            if (project is null)
                return NotFound();

            return View(project);
        }

        public IActionResult Create()
        {
            var model = new CreateProjectVM
            {
                Departments = _departmentRepository.GetSelectList() 
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _departmentRepository.GetSelectList();
                return View(model);
            }

            await _projectRepository.Create(model);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var project = _projectRepository.GetById(id);
            if (project is null)
            {
                return NotFound();
            }

            var model = new EditProjectVM
            {
                Id = project.Id,
                Name = project.Name,
                Location = project.Location,
                Budget = project.Budget,
                DepartmentId = project.DepartmentId,
                Departments = _departmentRepository.GetSelectList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProjectVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _departmentRepository.GetSelectList();
                return View(model);
            }

            await _projectRepository.Update(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var isDeleted = _projectRepository.Delete(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
