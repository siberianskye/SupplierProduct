using Microsoft.AspNetCore.Mvc;
using SupplierProductExercise.Business.Entities;
using SupplierProductExercise.Business.Enums;
using SupplierProductExercise.Business.Importers;
using SupplierProductExercise.Business.Repositories;
using SupplierProductExercise.Models;

namespace SupplierProductExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceParameterController : ControllerBase
    {
        private readonly ISupplierServiceParameterRepository __SupplierServiceParameterRepository;

        public ServiceParameterController(ISupplierServiceParameterRepository supplierServiceParameterRepository)
        {
            __SupplierServiceParameterRepository = supplierServiceParameterRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupplierServiceParameterModel supplierService)
        {
            int _ID = await __SupplierServiceParameterRepository.CreateAsync(new SupplierServiceParameter()
            {
                Service = supplierService.Service,
                BearerToken = supplierService.BearerToken
            });

            return Ok(_ID);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SupplierServiceParameterModel supplierService)
        {
            int _ID = await __SupplierServiceParameterRepository.UpdateAsync(new SupplierServiceParameter()
            {
                Service = supplierService.Service,
                BearerToken = supplierService.BearerToken
            });

            return Ok(_ID > 0);
        }
    }
}
