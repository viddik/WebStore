using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebStore.Domain.ViewModel;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesApiController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesApiController(IEmployeesData employeesData)
        {
            _employeesData = employeesData ?? throw new ArgumentNullException(nameof(employeesData));
        }

        // GET api/employees
        [HttpGet]
        public IEnumerable<EmployeeView> GetAll()
        {
            return _employeesData.GetAll();
        }

        // GET api/employees/{id}
        [HttpGet("{id}")]
        public EmployeeView GetById(int id)
        {
            return _employeesData.GetById(id);
        }

        // POST api/employees
        [HttpPost]
        public void AddNew([FromBody]EmployeeView model)
        {
            _employeesData.AddNew(model);
        }

        // PUT api/employees/{id}
        [HttpPut("{id}")]
        public EmployeeView UpdateEmployee(int id, [FromBody]EmployeeView entity)
        {
            return _employeesData.UpdateEmployee(id, entity);
        }

        // DELETE api/employees/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeesData.Delete(id);
        }

    }
}