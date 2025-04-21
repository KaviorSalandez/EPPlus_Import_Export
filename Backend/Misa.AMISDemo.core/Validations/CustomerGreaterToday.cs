using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Validations
{
    public class CustomerGreaterToday : ValidationAttribute
    {
        /// <summary>
        /// Validate datetime khi nó không bằng với ngày hiện tại 
        /// </summary>
        /// <param name="value">giá trị của dữ liệu truyền tới </param>
        /// <param name="validationContext"></param>
        /// <returns>trả về object validatetionResult </returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            DateTime date;
            if(DateTime.TryParse(value.ToString(), out date)) // chuyển object và gán vào date
            {
                var TodayDate = DateTime.Now;
                if(date > TodayDate)
                {
                    return new ValidationResult(ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("Kiểu date không hợp lệ");
            }
        }
    }
}
