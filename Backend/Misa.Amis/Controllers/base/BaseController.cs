using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs.Tokens;
using MISA.AMISDemo.Core.Interfaces.Base;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<TDto, TCreateDto, TUpdateDto> : BaseReadOnlyController<TDto>
    {
        IBaseService<TDto, TCreateDto, TUpdateDto> _baseService;
        public BaseController(IBaseService<TDto, TCreateDto, TUpdateDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
      

        #region Tạo mới Bản ghi theo Type
        /// <summary>
        /// Tên hàm: Tạo mới Bản ghi theo Type
        /// </summary>
        /// <param name="T">1 Bản ghi theo Type</param>
        ///  created by:  Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        /// <returns>Success: Trả về số bản ghi được tạo </returns>

        [HttpPost]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> CreateOne([FromBody] TCreateDto T)
        {
            var num = await _baseService.InsertService(T);

            return StatusCode(201, num);

        }
        #endregion

        #region Cập nhật Bản ghi theo Type 
        /// <summary>
        /// Tên hàm: Cập nhật Bản ghi theo Type
        /// </summary>
        /// <param name="T">Bản ghi theo Type </param>
        /// <param name="id">id của Bản ghi theo Type cập nhật </param>
        /// <returns>success: trả về số bản ghi được cập nhật</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOne([FromBody] TUpdateDto T, Guid id)
        {
            var num = await _baseService.UpdateService(T, id);
            return StatusCode(200, num);

        }
        #endregion

        #region Xóa Bản ghi theo Type
        /// <summary>
        /// Tên hàm: Xóa Bản ghi theo Type
        /// </summary>
        ///  created by:  Đặng Đình Quốc Khánh 
        ///  created_at: 2023/12/20 
        ///  <param name="id">id của Bản ghi theo Type muốn xóa </param>
        /// <returns> success: trả về num số bản ghi được xóa
        ///</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _baseService.DeleteService(id);
            return StatusCode(200, delete);

        }
        #endregion
        //#region Tìm Bản ghi theo Type
        ///// <summary>
        ///// Tên hàm: Tìm Bản ghi theo Type
        ///// </summary>
        /////  created by: Đặng Đình Quốc Khánh 
        /////  created_at: 2023/12/20 
        /////  <param name="id">id của Bản ghi theo Type muốn tìm </param>
        ///// <returns>trả về Bản ghi theo Type tìm thấy được. 
        ///// customerGroup là Null: khi không tìm thấy </returns>
        //[HttpGet("{id}")]
        //[Authorize]
        //public async Task<IActionResult> FindOne(Guid id)
        //{

        //    var customerGroup = await _baseService.FindOne(id);

        //    return StatusCode(200, customerGroup);

        //}
        //#endregion
    
}
}
