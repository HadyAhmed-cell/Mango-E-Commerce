using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _appDb;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        public CouponAPIController( AppDbContext appDb , IMapper mapper)
        {
            _appDb = appDb;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _appDb.Coupons.ToList();
                _response.Result= _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message; 
            }
            return _response;
        }

            [HttpGet]
            [Route("{id:int}")]
            public ResponseDto Get(int id)
            {
                try
                {
                    Coupon obj = _appDb.Coupons.First(u=>u.CouponId==id);
                _response.Result = _mapper.Map<CouponDto>(obj);
                    
                   
                }
                catch (Exception ex)
                {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
                }
            return _response;
            }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                Coupon obj = _appDb.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if (obj ==null)
                {
                    _response.IsSuccess = false;
                }
                
                _response.Result = _mapper.Map<CouponDto>(obj);


            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _appDb.Coupons.Add(obj);
                _appDb.SaveChanges();
                if (obj == null)
                {
                    _response.IsSuccess = false;
                }

                _response.Result = _mapper.Map<CouponDto>(obj);


            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]

        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _appDb.Coupons.Update(obj);
                _appDb.SaveChanges();
                if (obj == null)
                {
                    _response.IsSuccess = false;
                }

                _response.Result = _mapper.Map<CouponDto>(obj);


            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]

        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _appDb.Coupons.First(u => u.CouponId == id);
                _appDb.Coupons.Remove(obj);
                _appDb.SaveChanges();
               


            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
