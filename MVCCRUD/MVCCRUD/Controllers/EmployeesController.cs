using Microsoft.AspNetCore.Mvc;
using MVCCRUD.DAL;
using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _dbcontext;
        public EmployeesController(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            var employee = new Employee
            {
                Name = viewModel.Name,
                Age = viewModel.Age,
                Dep = viewModel.Dep,
                Salary = viewModel.Salary
            };
            await _dbcontext.Employees.AddAsync(employee);
            await _dbcontext.SaveChangesAsync();
            ModelState.Clear();
            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> List() 
        {
            var employees =  _dbcontext.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _dbcontext.Employees.FindAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee ViewModel)
        {
            var employee = await _dbcontext.Employees.FindAsync(ViewModel.Id);

            if (employee is not null)
            {
                employee.Name = ViewModel.Name;
                employee.Age = ViewModel.Age;
                employee.Dep = ViewModel.Dep;
                employee.Salary = ViewModel.Salary;

                await _dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("List","Employees");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _dbcontext.Employees.FindAsync(id);
            if (employee is not null)
            {
                _dbcontext.Employees.Remove(employee);
                await _dbcontext.SaveChangesAsync();
            }
            return RedirectToAction("List","Employees");
        }
    }
}
