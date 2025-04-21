using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Interfaces.Base;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseReadOnlyController<TDto> : ControllerBase
    {

        IBaseReadonlyService<TDto> _baseService;
        private IBaseService<TDto, object, object> baseService;

        public BaseReadOnlyController(IBaseReadonlyService<TDto> services)
        {
            _baseService = services;
        }

        public BaseReadOnlyController(IBaseService<TDto, object, object> baseService)
        {
            this.baseService = baseService;
        }
        #region lấy tất cả Bản ghi theo Type 

        /// <summary>
        /// Tên hàm: lấy ra tất cả các Bản ghi theo Type 
        /// </summary>
        /// <returns>trả về danh sách tất cả Bản ghi theo Type</returns>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        [HttpGet]
        [Authorize]
        [ResponseCache(CacheProfileName ="Default30")]
        public async Task<IActionResult> FindAll()
        {
            var Ts = await _baseService.FindAll();
            return Ok(Ts);
        }

        #endregion

        #region Tìm Bản ghi theo Type
        /// <summary>
        /// Tên hàm: Tìm Bản ghi theo Type
        /// </summary>
        ///  created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        ///  <param name="id">id của Bản ghi theo Type muốn tìm </param>
        /// <returns>trả về Bản ghi theo Type tìm thấy được. 
        /// customerGroup là Null: khi không tìm thấy </returns>
        [HttpGet("{id}")]
        [Authorize]
        [ResponseCache(CacheProfileName = "Default30")]
      
        public async Task<IActionResult> FindOne(Guid id)
        {

            var customerGroup = await _baseService.FindOne(id);

            return StatusCode(200, customerGroup);

        }
        #endregion

    }
}
