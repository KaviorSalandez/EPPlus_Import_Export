using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Exceptions
{
    // xử lý ngoại lệ khi có lỗi xảy ra ở các thời điểm chạy code 
    // created by: khanhddq
    // created at: 1/20/2023
    public class HandleException
    {
        /// <summary>
        /// tạo đường ống cần
        /// Request delegate 
        /// Invoke 
        /// service đường ống khi đi qua
        /// response sau next
        /// </summary>
        public RequestDelegate _next;

        public HandleException(RequestDelegate next)
        {

            _next = next;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">Đây là http request</param>
        /// <returns>ghi ra context.response.write...</returns>
        /// ///  created: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        public async Task Invoke(HttpContext context)
        {
            try
            {
               
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    var response = new MISAServiceResult
                    {
                        success = false,
                        status = System.Net.HttpStatusCode.Unauthorized,
                        devMsg = MISA.AMISDemo.Core.Resource.Resource_VN.Authorize
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            }
            catch (ValidateException vx)
            {
                var response = new MISAServiceResult
                {
                    success = false,
                    status = System.Net.HttpStatusCode.BadRequest
                };
                response.errors.Add(vx.Message);
                context.Response.StatusCode = 400;
                await Console.Out.WriteLineAsync("===================");
                await Console.Out.WriteLineAsync(response.ToString());
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                var response = new MISAServiceResult
                {
                    success = false,
                    status = System.Net.HttpStatusCode.InternalServerError,

                };
                response.errors.Add(ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
