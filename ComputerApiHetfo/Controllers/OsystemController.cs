using ComputerApiHetfo.Models;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<Osystem> Post(CreateOsDto createOsDto)
        {
            var os = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = createOsDto.Name
            };

            if (os != null)
            {
                computerContext.Osystems.Add(os);
                computerContext.SaveChanges();
                return StatusCode(201, os);
            }

            return BadRequest();
        }
    }
}
