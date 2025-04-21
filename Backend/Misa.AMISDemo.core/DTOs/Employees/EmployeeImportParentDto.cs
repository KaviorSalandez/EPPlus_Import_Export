using MISA.AMISDemo.Core.DTOs.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs.Employees
{
    public class EmployeeImportParentDto
    {

        public int CountSuccess { get; set; }
        public int CountFail { get; set; }

        public string IdImport { get; set; }

        public List<EmployeeImportDto> EmployeeImportDtos { get; set; }

        public EmployeeImportParentDto()
        {
            EmployeeImportDtos = new List<EmployeeImportDto>();
        }
    }
}
