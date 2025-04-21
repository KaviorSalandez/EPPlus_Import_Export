using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs.Customers;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Caches;
using MISA.AMISDemo.Core.Interfaces.Positions;
using MISA.AMISDemo.Core.Interfaces.Positions;
using System;

namespace MISA.AMISDemo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    // controller của vị trí 
    // createdby: Khanhddq
    // Created at: 12/30/2023
    public class PositionsController : BaseReadOnlyController<PositionDto>
    {
        public ICacheService _cacheService;
        IPositionService _positionService;
        public PositionsController(IPositionService positionService, ICacheService cacheService) : base(positionService)
        {
            _cacheService = cacheService;
            _positionService = positionService;
        }

        [HttpGet]
        [Route("TestRedis")]
        /// <summary>
        /// Tên hàm: thử get all redis 
        /// </summary>
        /// created by: Đặng Đình Quốc Khánh 
        ///  created_at: 2023/1/20 
        public async Task<IActionResult> TestGet()
        {

            // check cache data
            var cacheData = _cacheService.GetData<IEnumerable<PositionDto>>("position");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return StatusCode(200, cacheData);

            }
            // set expirity time    
            cacheData = await _positionService.FindAll();

            _cacheService.SetData<IEnumerable<PositionDto>>("position", cacheData, DateTimeOffset.Now.AddSeconds(30));
            return Ok(cacheData);
        }

        //[HttpPost]
        //[Route("create")]
        ///// <summary>
        ///// Tên hàm: tạo thử 
        ///// </summary>
        ///// created by: Đặng Đình Quốc Khánh 
        /////  created_at: 2023/1/20 
        //public IActionResult TestCreate(PositionCreateDto positionCreateDto)
        //{
        //    var create = _positionService.InsertService(positionCreateDto);
        //    var cacheData = _cacheService.SetData<PositionCreateDto>($"position{Guid.NewGuid}", positionCreateDto, DateTimeOffset.Now.AddSeconds(30));
        //    return Ok(cacheData);
        //}
        
        //[HttpPost]
        //[Route("createMany")]
        ///// <summary>
        ///// Tên hàm: tạo thử 
        ///// </summary>
        ///// created by: Đặng Đình Quốc Khánh 
        /////  created_at: 2023/1/20 
        //public IActionResult TestCreate(List<Position> positionCreateDto)
        //{
        //    //var create = _positionService.InsertService(positionCreateDto);
        //    //var cacheData = _cacheService.SetData<PositionCreateDto>($"position{Guid.NewGuid}", positionCreateDto, DateTimeOffset.Now.AddSeconds(30));
        //    var createMany = _positionRepository.InsertMany(positionCreateDto);
        //    return Ok(createMany);
        //}

        //[HttpDelete("delete/{id}")]
        ///// <summary>
        ///// Tên hàm: tạo thử 
        ///// </summary>
        ///// created by: Đặng Đình Quốc Khánh 
        /////  created_at: 2023/1/20 
        //public IActionResult TestDelete(Guid id)
        //{
        //    var position = _positionService.FindOne(id);
        //    _positionService.DeleteService(id);
        //    var cacheData = _cacheService.Delete($"position{id}");

        //    return NoContent();
        //}
    }
}
