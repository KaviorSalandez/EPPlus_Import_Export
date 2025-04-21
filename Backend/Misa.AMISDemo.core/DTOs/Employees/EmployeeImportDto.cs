using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs.Employees
{
    public class EmployeeImportDto : Employee
    {
        public List<String> Errors { get; set; }
        public bool IsImported { get; set; }

        public EmployeeImportDto()
        {
            Errors = new List<String>();
        }
    }
}
