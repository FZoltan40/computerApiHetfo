using ComputerApiHetfo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerApiHetfo.Controllers
{
    [Route("computers")]
    [ApiController]
    public class CompController : ControllerBase
    {
        private readonly ComputerContext _computerContext;

        public CompController(ComputerContext computerContext)
        {
            this._computerContext = computerContext;
        }

        [HttpPost]
        public async Task<ActionResult<Comp>> Post(CreateComputer createComputer)
        {
            var computer = new Comp
            {
                Id = Guid.NewGuid(),
                Brand = createComputer.Brand,
                Type = createComputer.Type,
                Display = createComputer.Display,
                Memory = createComputer.Memory,
                CreatedTime = DateTime.Now,
                OsId = createComputer.OsId
            };

            if (computer != null)
            {
                await _computerContext.Comps.AddAsync(computer);
                await _computerContext.SaveChangesAsync();
                return StatusCode(201, computer);

            }

            Exception e = new Exception();
            return BadRequest(new { message = e.Message });
        }

        [HttpGet("numOfComputer")]
        public async Task<ActionResult<Comp>> GetNumberOfCumputers()
        {
            var num = await _computerContext.Comps.ToListAsync();

            if (num != null)
            {
                return Ok(new { message = $"Az adatbázisban {num.Count} számítógép van." });
            }

            return BadRequest(new { message = "Hiba a lekérdezésben." });

        }

        [HttpGet("allComputerWithOs")]
        public async Task<ActionResult<Comp>> GetAllComputerWithOs()
        {
            var allComputer = await _computerContext.Comps.Select(cmp => new { cmp.Brand, cmp.Type, cmp.Os.Name }).ToListAsync();
            return Ok(allComputer);
        }

        [HttpGet("allMicrosoftOs")]
        public async Task<ActionResult<Comp>> GetallMicrosoftOs()
        {
            var allMicrosoft = await _computerContext.Comps.Where(cmp => cmp.Os.Name.Contains("Microsoft")).Select(cmp
                => new { cmp.Brand, cmp.Type, cmp.Memory, cmp.Os.Name }).ToListAsync();
            return Ok(allMicrosoft);
        }

        [HttpGet("maxDisplay")]
        public async Task<ActionResult<Comp>> GetmaxDisplay()
        {
            var maxDisplay = await _computerContext.Comps.MaxAsync(cmp => cmp.Display);
            var cmpMaxDiplay = await _computerContext.Comps.Where(cmp => cmp.Display == maxDisplay).Select(cmp => new { cmp.Brand, cmp.Type, cmp.Display, cmp.Os.Name }).ToListAsync();
            return Ok(cmpMaxDiplay);
        }

        [HttpGet("newComputer")]
        public async Task<ActionResult<Comp>> GetNewComputer()
        {
            var newComputer = await _computerContext.Comps.MaxAsync(cmp => cmp.CreatedTime);
            var newComputerDate = await _computerContext.Comps.Where(cmp => cmp.CreatedTime == newComputer).Select(cmp => new { cmp.Brand, cmp.Type, cmp.CreatedTime, cmp.Os.Name }).ToListAsync();

            var newComputerDate2 = await _computerContext.Comps.OrderByDescending(cmp => cmp.CreatedTime).FirstOrDefaultAsync();

            return Ok(newComputerDate);
        }

    }
}
