using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.DTOs
{
    public class MISAServiceResult
    {
        /// <summary>
        /// Lỗi trả về 
        /// </summary>
        public List<String> errors { get; set; }
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
        /// <summary>
        /// thành công có hay không 
        /// </summary>
        public bool success { get; set; }
        public MISAServiceResult()
        {
            errors = new List<String>();
        }

      
    }
}
