using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Model.Size;
using NK.Core.Business.Service;
using NK.Core.Model.Entities;
using System.Net;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Size>>> GetAllSize()
        {
            try
            {
                var size = await _sizeService.GetAllSizeAsync();
                if (size == null || !size.Any())
                {
                    return NotFound();
                }
                return Ok(size);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("Get/{Id}")]
        public async Task<ActionResult<Size>> GetByIdSize(string Id)
        {
            var size = await _sizeService.GetByIdSizeAsync(Id);

            if (size == null)
            {
                return NotFound();
            }
            return size;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSize(SizeDto sizeDto)
        {
            if (sizeDto == null)
            {
                return BadRequest("Sole object is null");
            }

            var existingSize = await _sizeService.GetByNumberSizeAsync(sizeDto.NumberSize);
            if (existingSize != null)
            {
                return Conflict("Size with the same NumberSize already exists");
            }

            var size = new Size
            {
                NumberSize = sizeDto.NumberSize
            };

            await _sizeService.CreateAsync(size);
            return CreatedAtAction("GetByIdSize", new { size.Id }, size);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSize(string id, SizeDtoUpdate sizeDtoUpdate)
        {
            try
            {
                if (id != sizeDtoUpdate.Id)
                {
                    return BadRequest("The provided id does not match the id in the user data.");
                }

                var existingSize = await _sizeService.GetByNumberSizeAsync(sizeDtoUpdate.NumberSize);
                var sizeItem = await _sizeService.GetByIdSizeAsync(id);
                if (existingSize != null && existingSize != sizeItem)
                {
                    return Conflict("Size with the same NumberSize already exists");
                }

                var size = sizeDtoUpdate.Adapt<Size>();
                await _sizeService.UpdateByIdSize(id, size);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteSize(string id)
        {
            try
            {
                return await _sizeService.DeleteSize(id);
            }
            catch
            {

                throw;
            }
        }
    }
}
