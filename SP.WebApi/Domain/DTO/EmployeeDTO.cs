using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SP.WebApi.Domain.DTO
{
    public class EmployeeDTO
    {

        public string LoginAlias { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string ManagerLoginAlias { get; set; }
        public List<string> Skills { get; set; }
    }

    public class EmployeeCreateDTO
    {
        [Required]
        public string LoginAlias { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string ManagerLoginAlias { get; set; }
        public List<string> Skills { get; set; }
    }

    public class EmployeeUpdateDTO
    {
        [Required]
        public string LoginAlias { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string ManagerLoginAlias { get; set; }
        public List<string> Skills { get; set; }
    }


}