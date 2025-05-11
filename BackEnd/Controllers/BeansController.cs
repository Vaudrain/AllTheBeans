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
        public async Task<ActionResult<IEnumerable<BeanApiDTO>>> GetBeans()
        {
            Bean[] beans = await _context.Beans.ToArrayAsync();
            return beans.Select(b => b.ToBeanApiDTO()).ToList();
        }

        [HttpGet("{index}")]
        public async Task<ActionResult<BeanApiDTO>> GetBean(int index)
        {
            Bean? bean = await _context.Beans.FirstOrDefaultAsync(b => b.Index == index);

            if (bean == null)
            {
                return NotFound();
            }

            return bean.ToBeanApiDTO();
        }

        [HttpGet("botd")]
        public async Task<ActionResult<BeanApiDTO>> GetBeanOfTheDay()
        {
            string BotdId = await _context.BeanOfTheDay.Select(b => b.BeanId).FirstOrDefaultAsync() ?? "";
            Bean? bean = await _context.Beans.Where(b => b.Id == BotdId).FirstOrDefaultAsync();

            if (bean == null)
            {
                return NotFound();
            }

            return bean.ToBeanApiDTO();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BeanApiDTO>>> SearchBeans([FromQuery] string? name, [FromQuery] string? country, [FromQuery] string? colour, [FromQuery] float? lowerPrice, [FromQuery] float? upperPrice)
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

            return Ok(results.Select(b => b.ToBeanApiDTO()).ToList());
        }

        [HttpPut("addBean")]
        [HttpPost]
        public async Task<IActionResult> AddBean(BeanApiDTO beanDTO)
        {
            // We should also sanitise input here, but I'll say that's beyond the scope of this task
            // for the purposes of this task, we'll just assume the input is safe
            // but we would never assume this in a real application
            BadRequestObjectResult? resultsInvalid = ValidateInput(beanDTO);
            if (resultsInvalid != null)
            {
                return resultsInvalid;
            }

            int highestIndex = await _context.Beans.MaxAsync(b => b.Index);
            Bean newBean = beanDTO.CreateBean(highestIndex + 1);

            _context.Beans.Add(newBean);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private BadRequestObjectResult? ValidateInput(BeanApiDTO beanDTO)
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(beanDTO.Name))
            {
                errorMessage += "Name is required. ";
            }
            if (string.IsNullOrEmpty(beanDTO.Description))
            {
                errorMessage += "Description is required. ";
            }
            if (string.IsNullOrEmpty(beanDTO.Country))
            {
                errorMessage += "Country is required. ";
            }
            if (string.IsNullOrEmpty(beanDTO.Colour))
            {
                errorMessage += "Colour is required. ";
            }
            if (string.IsNullOrEmpty(beanDTO.Image))
            {
                errorMessage += "Image is required. ";
            } else {
                try
                {
                    var uri = new Uri(beanDTO.Image);
                    if (!uri.IsAbsoluteUri 
                        || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                        // this is also where we should check if the image is a valid image type and not malicious
                        // but that's a bit more complicated and probably beyond the scope of this task
                    {
                        errorMessage += "Image format incorrect, it should be a link to an image. ";
                    }
                }
                catch
                {
                    errorMessage += "Image format incorrect, it should be a link to an image. ";
                }
            }
            if (beanDTO.CostGBP <= 0)
            {
                errorMessage += "Cost is required and must be greater than 0. ";
            }

            // There's probably more validation we could do here, but this will do for now

            if (string.IsNullOrEmpty(errorMessage))
            {
                return null;
            }

            return BadRequest(errorMessage);
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> DeleteBean(int index)
        {
            Bean? bean = await _context.Beans.FirstOrDefaultAsync(b => b.Index == index);
            if (bean == null)
            {
                return NotFound();
            }

            _context.Beans.Remove(bean);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}