using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model.Address;
using NK.Core.Business.Service;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IAddressService _addressService;
        private readonly IProvinceService _provinceService;
        private readonly IWardsService _wardsService;
        private readonly IDistrictService _districtService;

        public AddressController(AppDbContext dbContext, IAddressService addressService, IProvinceService provinceService, IWardsService wardsService, IDistrictService districtService)
        {
            _dbContext = dbContext;
            _addressService = addressService;
            _provinceService = provinceService;
            _wardsService = wardsService;
            _districtService = districtService;
        }

        // GET api/<AddressController>/5
        [HttpGet("{userId}")]
        public async Task<ActionResult> Get(string userId)
        {
            var addresses = await _addressService.GetByUserId(userId);
            return Ok(addresses);
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddressAPI address)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    var add = await _addressService.AddNew(address);
                    transaction.Commit();
                    return Ok(add);
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] AddressUpdateAPI address)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != address.Id)
                return BadRequest(new { error = "Id địa chỉ không hợp lệ" });

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _addressService.Update(address);
                    transaction.Commit();
                    return NoContent();
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(new { error = ex.Message });
                    throw;
                }
            }
        }

        [HttpGet("province")]
        public async Task<ActionResult<Province>> GetAllProvince()
        {
            try
            {
                var res = await _provinceService.GetAllProvince();

                if (res == null) return NotFound();

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("district")]
        public async Task<ActionResult<District>> GetAllDistrict(int provinceId)
        {
            try
            {
                var res = await _districtService.GetAllDistrict(provinceId);

                if (res == null) return NotFound();

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("wards")]
        public async Task<ActionResult<Wards>> GetAllWards(int districtId)
        {
            try
            {
                var res = await _wardsService.GetAllWards(districtId);

                if (res == null) return NotFound();

                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
