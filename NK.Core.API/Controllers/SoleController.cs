using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Model.Size;
using NK.Core.Business.Service;
using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoleController : ControllerBase
    {
        private readonly ISoleService _soleService;

        public SoleController(ISoleService soleService)
        {
            _soleService = soleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSole(string id)
        {
            var sole = await _soleService.GetSoleByIdAsync(id);
            if (sole == null)
            {
                return NotFound();
            }

            return Ok(sole);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSoles()
        {
            var soles = await _soleService.GetAllSolesAsync();
            return Ok(soles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSole(SoleDto soleDTO)
        {
            if (soleDTO == null)
            {
                return BadRequest("Sole object is null");
            }

            var existingSole = await _soleService.GetSoleByNameAsync(soleDTO.Name);
            if(existingSole != null)
            {
                return Conflict("Sole with the same Name already exists");
            }

            var sole = new Sole
            {
                Name = soleDTO.Name
            };

            await _soleService.AddSoleAsync(sole);
            return CreatedAtAction("GetSole", new { id = sole.Id }, sole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSole(string id, SoleDto soleDTO)
        {
            if (soleDTO == null)
            {
                return BadRequest("Sole object is null");
            }

            var existingSole = await _soleService.GetSoleByIdAsync(id);

            if (existingSole == null)
            {
                return NotFound("Sole not found");
            }

            var checkNameSole = await _soleService.GetSoleByNameAsync(soleDTO.Name);
            if (checkNameSole != null && checkNameSole != existingSole)
            {
                return Conflict("Sole with the same Name already exists");
            }

            existingSole.Name = soleDTO.Name;

            await _soleService.UpdateSoleAsync(existingSole);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSole(string id)
        {
            var sole = await _soleService.GetSoleByIdAsync(id);

            if (sole == null)
            {
                return NotFound();
            }

            await _soleService.RemoveSoleAsync(sole);
            return NoContent();
        }
    }
}
