using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Implementations
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly List<EmployeeView> _employees;

        public InMemoryEmployeesData()
        {
            _employees = new List<EmployeeView>()
            {
                new EmployeeView(){Id = 1, FirstName = "Василий", SurName = "Пупкин", Patronymic =
                    "Иванович", Age = 38, Position = "Директор", EmploymentDate = new DateTime(2010, 1, 1)},
                new EmployeeView(){Id = 2, FirstName = "Иван", SurName = "Холявко", Patronymic =
                    "Александрович", Age = 35, Position = "Программист", EmploymentDate = new DateTime(2010, 2, 5)},
                new EmployeeView(){Id = 3, FirstName = "Роберт", SurName = "Серов", Patronymic =
                    "Сигизмундович", Age = 50, Position = "Зав. склада", EmploymentDate = new DateTime(2013, 6, 20)}
            };
        }

        /// <summary>
        /// Список сотрудников
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeView GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="model"></param>
        public void AddNew(EmployeeView model)
        {
            model.Id = _employees.Max(e => e.Id) + 1;
            _employees.Add(model);
        }

        /// <summary>
        /// Удаление сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }

        

        
    }
}
