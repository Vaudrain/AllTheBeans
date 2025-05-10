using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeansAPI.Models;

namespace BeanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeansController : ControllerBase
    {
        private readonly BeanContext _context;

        public BeansController(BeanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bean>>> GetBeans()
        {
            return await _context.Beans.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bean>> GetBean(string id)
        {
            Bean? bean = await _context.Beans.FindAsync(id);

            if (bean == null)
            {
                return NotFound();
            }

            return bean;
        }

        [HttpGet("botd")]
        public async Task<ActionResult<Bean>> GetBeanOfTheDay()
        {
            string BotdId = await _context.BeanOfTheDay.Select(b => b.BeanId).FirstOrDefaultAsync() ?? "";
            Bean? bean = await _context.Beans.Where(b => b.Id == BotdId).FirstOrDefaultAsync();

            if (bean == null)
            {
                return NotFound();
            }

            return bean;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Bean>>> SearchBeans([FromQuery] string? name, [FromQuery] string? country, [FromQuery] string? colour, [FromQuery] float? lowerPrice, [FromQuery] float? upperPrice)
        {
            var query = _context.Beans.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                // Potential scope to include description in the search here, if bean descriptions might contain alternate or related names
                // Probably something that is more worth it for future iterations that include keywords etc.
                query = query.Where(b => b.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(b => b.Country.ToLower().Contains(country.ToLower()));
            }

            if (!string.IsNullOrEmpty(colour))
            {
                query = query.Where(b => b.Colour.ToLower().Contains(colour.ToLower()));
            }

            if (lowerPrice.HasValue)
            {
                query = query.Where(b => b.CostGBP >= lowerPrice.Value);
            }

            if (upperPrice.HasValue)
            {
                query = query.Where(b => b.CostGBP <= upperPrice.Value);
            }

            var results = await query.ToListAsync();

            if (results.Count == 0)
            {
                return NotFound("No beans match the search criteria.");
            }

            return Ok(results);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBean(string id, Bean bean)
        {
            if (id != bean.Id)
            {
                return BadRequest();
            }

            _context.Entry(bean).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Bean>> PostBean(Bean bean)
        {
            _context.Beans.Add(bean);
            await _context.SaveChangesAsync(); // TODO error handling

            return CreatedAtAction(nameof(GetBean), new { id = bean.Id }, bean);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBean(string id)
        {
            Bean? bean = await _context.Beans.FindAsync(id);
            if (bean == null)
            {
                return NotFound();
            }

            _context.Beans.Remove(bean);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeanExists(string id)
        {
            return _context.Beans.Any(e => e.Id == id);
        }
    }
}