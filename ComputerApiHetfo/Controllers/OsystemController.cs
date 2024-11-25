using ComputerApiHetfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerApiHetfo.Controllers
{
    [Route("osystem")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        private readonly ComputerContext computerContext;
        public OsystemController(ComputerContext computerContext)
        {
            this.computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<Osystem>> Post(CreateOsDto createOsDto)
        {
            var os = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = createOsDto.Name
            };

            if (os != null)
            {
                await computerContext.Osystems.AddAsync(os);
                await computerContext.SaveChangesAsync();
                return StatusCode(201, os);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<Osystem>> Get()
        {
            return Ok(await computerContext.Osystems.ToListAsync());
        }
    }
}
