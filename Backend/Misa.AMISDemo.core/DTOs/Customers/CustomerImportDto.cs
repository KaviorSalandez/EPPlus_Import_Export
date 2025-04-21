using MISA.AMISDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs.Customers
{
    public class CustomerImportDto : Customer
    {
        public List<String> Errors { get; set; }
        public bool IsImported { get; set; }

        public CustomerImportDto()
        {
            Errors = new List<String>();
        }
    }
}
