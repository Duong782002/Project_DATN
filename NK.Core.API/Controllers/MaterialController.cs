using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Service;
using NK.Core.Model.Entities;
using System.Linq.Expressions;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaterial(string id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMaterials()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterial(string id, MaterialCreateDTO materialDTO)
        {
            if (materialDTO == null)
            {
                return BadRequest("Material object is null");
            }

            var existingMaterial = await _materialService.GetMaterialByIdAsync(id);

            if (existingMaterial == null)
            {
                return NotFound("Material not found");
            }

            var checkNameMaterial = await _materialService.GetByNameMaterialAsync(materialDTO.Name);
            if(checkNameMaterial != null && checkNameMaterial != existingMaterial)
            {
                return Conflict("Material with the same Name already exists");
            }
            // Cập nhật thông tin vật liệu từ DTO
            existingMaterial.Name = materialDTO.Name;
            // Cập nhật các thuộc tính khác tùy theo yêu cầu của Material

            await _materialService.UpdateMaterialAsync(existingMaterial);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterial(MaterialCreateDTO materialDTO)
        {
            if (materialDTO == null)
            {
                return BadRequest("Material object is null");
            }

            var existingMaterial = await _materialService.GetByNameMaterialAsync(materialDTO.Name);
            if(existingMaterial != null)
            {
                return Conflict("Material with the same Name already exists");
            }
            var material = new Material
            {
                Name = materialDTO.Name
            };

            await _materialService.AddMaterialAsync(material);
            return CreatedAtAction("GetMaterial", new { id = material.Id }, material);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(string id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            await _materialService.RemoveMaterialAsync(material);
            return NoContent();
        }
    }
}
