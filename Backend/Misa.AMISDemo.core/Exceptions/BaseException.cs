using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Exception
{
    public class BaseException
    {
       

        /// <summary>
        /// kiểu của lỗi là gì 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        ///  tiêu đề cho lỗi đó 
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// trạng thái của kiểu trả về 
        /// </summary>
        public HttpStatusCode status { get; set; }
        /// <summary>
        /// trace id là gì 
        /// </summary>

        public string traceId { get; set; }
        /// <summary>
        /// dữ liệu trả về là sao 
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// tin nhắn cho người dùng 
        /// </summary>
        public string userMsg { get; set; }
        /// <summary>
        /// tin nhắn cho developer 
        /// </summary>
        public string devMsg { get; set; }
        public object? errors { get; set; }
        /// <summary>
        /// thành công có hay không 
        /// </summary>
        /// 

        public bool success { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
