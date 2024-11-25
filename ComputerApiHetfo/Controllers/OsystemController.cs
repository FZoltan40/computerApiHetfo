﻿using ComputerApiHetfo.Models;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Osystem>> GetById(Guid id)
        {
            var os = await computerContext.Osystems.FirstOrDefaultAsync(os => os.Id == id);
            return os != null ? Ok(os) : NotFound(new { message = "Nincs ilyen találat." });

            /*if (os != null)
            {
                return Ok(os);
            }

            return NotFound(new { message = "Nincs ilyen találat." });*/
        }
    }
}
