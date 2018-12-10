using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Models;

namespace WebStore.Controllers
{
    [Route("users")]
    [Authorize()]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }
        
        /// <summary>
        /// Список сотрудников
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_employeesData.GetAll());
        }

        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var employee = _employeesData.GetById(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        /// <summary>
        /// Открытие формы редактирования сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("edit/{id?}")]
        [Authorize(Roles="Administrator")]
        public IActionResult Edit(int? id)
        {
            EmployeeView model;
            if (id.HasValue)
            {
                model = _employeesData.GetById(id.Value);
                if (model == null)
                    return NotFound();
            }
            else
            {
                model = new EmployeeView();
            }

            return View(model);
        }

        /// <summary>
        /// Сохранение редактирования
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("edit/{id?}")]
        public IActionResult Edit(EmployeeView model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var item = _employeesData.GetById(model.Id);
                    item.FirstName = model.FirstName;
                    item.SurName = model.SurName;
                    item.Patronymic = model.Patronymic;
                    item.Age = model.Age;
                    item.EmploymentDate = model.EmploymentDate;
                    item.Position = model.Position;
                }
                else
                {
                    _employeesData.AddNew(model);
                }

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
            
        }

        /// <summary>
        /// Удаление сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            _employeesData.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}