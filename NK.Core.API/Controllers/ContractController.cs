using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model;
using NK.Core.Business.Service;
using NK.Core.Model.Entities;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContract()
        {
            var contract = await _contractService.GetAllContract();
            return Ok(contract);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContract([FromBody] ContractDto contractDto)
        {
            if(contractDto == null)
            {
                return BadRequest("Contract object is null");
            }

            var contract = new Contract()
            {
                CustomerName = contractDto.CustomerName,
                PhoneNumber = contractDto.PhoneNumber,
                Email = contractDto.Email,
                Note = contractDto.Note,
                UserId = contractDto.UserId
            };

            await _contractService.CreateContract(contract);
            return Ok(contract);
        }
    }
}
