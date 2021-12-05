using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCRM.API.Data;

namespace MiniCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly ICRMRepository _crmRepo;
        private readonly IMapper _mapper;

        public CustomersController(ICRMRepository crmRepo, IMapper mapper)
        {
            _crmRepo = crmRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _crmRepo.GetCustomers();
            return Ok(customers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _crmRepo.GetCustomer(id);
            return Ok(customer);
        }

    }
}