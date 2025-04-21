using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Const
{
    public static class MISAConst
    {
        public const string ERRMSG_CustomerCode = "mã khách hàng Không được trống ";
        public const string ERRMSG_EmployeeCode = "mã nhân viên Không được trống ";
        public const string ERRMSG_PositionName = "tên vị trí Không được trống ";
        public const string ERRMSG_CustomerGroupCode = "mã nhóm khách hàng Không được trống ";
        public const string ERRMSG_DepartmentId = "Id Department Không được trống ";
        public const string ERRMSG_PositionId = "Id position hàng Không được trống ";
        public const string ERRMSG_CustomerGroupNameCode = "tên nhóm Không được trống ";
        public const string ERRMSG_DepartmentName = "tên văn phòng Không được trống ";
        public const string ERRMSG_MinLength_Code = "Độ dài phải lớn hơn hoặc bằng 5 ký tự ";
        public const string ERRMSG_MaxLength_Code = "Không được vượt quá 100 ký tự";
        public const string ERRMSG_MaxLength_Address = "Không được vượt quá 200 ký tự";
        public const string ERRMSG_DateCreate = "Ngày tạo phải bằng với ngày hôm nay";
    }

}
