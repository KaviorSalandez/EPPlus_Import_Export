using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs.Customers
{
    public class CustomerImportParentDto
    {
        public int CountSuccess { get; set; }
        public int CountFail { get; set; }

        public List<CustomerImportDto> CustomerImportDtos { get; set; }

        public CustomerImportParentDto()
        {
            CustomerImportDtos = new List<CustomerImportDto>();
        }
    }
}
