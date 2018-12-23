using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.ViewModel;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InMemory
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
        /// Обновление сотрудника
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <param name="entity">Сотрудник для обновления</param>
        /// <returns></returns>
        public EmployeeView UpdateEmployee(int id, EmployeeView entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var employee = _employees.FirstOrDefault(e => e.Id.Equals(id));
            if (employee == null)
                throw new InvalidOperationException("Employee not exits");

            employee.Age = entity.Age;
            employee.FirstName = entity.FirstName;
            employee.Patronymic = entity.Patronymic;
            employee.SurName = entity.SurName;
            employee.Position = entity.Position;
            return employee;
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
