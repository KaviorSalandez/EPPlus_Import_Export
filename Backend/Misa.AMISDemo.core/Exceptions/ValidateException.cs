using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Exceptions
{
    // xử lý validate property 
    // created by: khanhddq
    // created at: 1/20/2023
    public class ValidateException : Exception
    {
        public string MsgError { get; set; }

        public ValidateException(string error)
        {
            MsgError = error;
        }

        public override string Message => MsgError;
    }
}
