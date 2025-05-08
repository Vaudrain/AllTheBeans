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
        public async Task<ActionResult<Bean>> GetBean(Guid id)
        {
            var Bean = await _context.Beans.FindAsync(id);

            if (Bean == null)
            {
                return NotFound();
            }

            return Bean;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBean(Guid id, Bean bean)
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
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBean), new { id = bean.Id }, bean);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBean(Guid id)
        {
            var Bean = await _context.Beans.FindAsync(id);
            if (Bean == null)
            {
                return NotFound();
            }

            _context.Beans.Remove(Bean);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeanExists(Guid id)
        {
            return _context.Beans.Any(e => e.Id == id);
        }
    }
}