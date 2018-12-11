using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.Clients.Base;
using WebStore.Interfaces.Services;
using WebStore.Models;

namespace WebStore.Clients.Services.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        protected sealed override string ServiceAddress { get; set; }

        public EmployeesClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/employees";
        }
        
        public IEnumerable<EmployeeView> GetAll()
        {
            var url = $"{ServiceAddress}";
            var list = Get<List<EmployeeView>>(url);
            return list;
        }

        public EmployeeView GetById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<EmployeeView>(url);
            return result;
        }

        public EmployeeView UpdateEmployee(int id, EmployeeView entity)
        {
            var url = $"{ServiceAddress}/{id}";
            var response = Put(url, entity);
            var result = response.Content.ReadAsAsync<EmployeeView>().Result;
            return result;
        }

        public void AddNew(EmployeeView model)
        {
            var url = $"{ServiceAddress}";
            Post(url, model);
        }

        public void Delete(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            Delete(url);
        }

    }
}
