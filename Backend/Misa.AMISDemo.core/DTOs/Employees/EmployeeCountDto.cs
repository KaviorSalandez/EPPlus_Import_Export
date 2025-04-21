using MISA.AMISDemo.Core.DTOs.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs.Employees
{
    public class EmployeeCountDto
    {
        public int Count { get; set; }
        public List<EmployeeDto> Employees { get; set; }

    }
}
