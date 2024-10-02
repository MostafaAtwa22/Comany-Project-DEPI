using DEPI_Final_Project.Models.Enums;
using DEPI_Final_Project.Repositories;
using DEPI_Final_Project.Repositories.Interfaces;
using DEPI_Final_Project.ViewModels;
using DEPI_Final_Project.ViewModels.DependentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DEPI_Final_Project.Controllers
{
    public class DependentController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDependentRepository _dependentRepository;

        public DependentController(IEmployeeRepository employeeRepository,
            IDependentRepository dependentRepository)
        {
            _employeeRepository = employeeRepository;
            _dependentRepository = dependentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var dependents = _dependentRepository.GetAll();
            return View(dependents);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var dependents = _dependentRepository.GetByEmployee(id);

            if (dependents is null)
                return NotFound();

            return View(dependents);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateDependentVM model = new CreateDependentVM
            {
                EmployeesList = _employeeRepository.GetSelectList(),
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
        public async Task<IActionResult> Create(CreateDependentVM model)
        {
            if (!ModelState.IsValid)
            {
                model.GenderSelectList = Enum.GetValues(typeof(Gender)).Cast<Gender>()
                    .Select(g => new SelectListItem
                    {
                        Value = ((int)g).ToString(),
                        Text = g.ToString()
                    }).ToList();
                return View(model);
            }

            await _dependentRepository.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id, string name)
        {
            var isDeleted = _dependentRepository.Delete(id, name);

            if (!isDeleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
