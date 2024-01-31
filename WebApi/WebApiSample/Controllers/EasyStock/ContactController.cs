using Asp.Versioning;
using AuthPermissions.AspNetCore;
using AuthpServices.PermissionsCode;
using EasyStockServices;
using Microsoft.AspNetCore.Mvc;
using SharedServices.Models.EasyStock;

namespace WebApiSample.Controllers.EasyStock
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly EasyStockContactServices _contactServices;
        public ContactController(EasyStockContactServices contactServices)
        {
            _contactServices = contactServices;
        }
        [HttpGet]
        [Route("GetContact")]
        [HasPermission(UserPermissions.AccessPageLevelOne)]
        public async Task<IActionResult> GetContact()
        {
            return Ok(await _contactServices.GetContactListAsycn());
        }

    
        [HttpPost]
        [Route("Create")]

        [HasPermission(UserPermissions.AccessPageLevelOne)]
        public async Task<ActionResult<IEnumerable<ContactDto>>> Create([FromBody] ContactDto objDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _contactServices.CreateAsync(objDTO);

            if (result == null)
            {
                // Return appropriate status code if the result is null
                return NotFound();
            }

            return Ok(result);
        }
       
    }
}
