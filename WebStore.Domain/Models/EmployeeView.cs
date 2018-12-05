using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class EmployeeView
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Domain.Resources.Resource), 
            ErrorMessageResourceName = "RequiredFieldMsg")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Domain.Resources.Resource),
            ErrorMessageResourceName = "RequiredFieldMsg")]
        [Display(Name = "Фамилия")]
        public string SurName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Range(18, 100)]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Display(Name = "Дата принятия на работу")]
        public DateTime EmploymentDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Domain.Resources.Resource),
            ErrorMessageResourceName = "RequiredFieldMsg")]
        [Display(Name = "Должность")]
        public string Position { get; set; }
    }
}
