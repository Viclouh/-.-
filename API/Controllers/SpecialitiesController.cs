using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialitiesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Speciality> Get()
        {
            Context context = new Context();
            return context.Specialities
                .Include(x=>x.Courses)
                .ThenInclude(x=>x.Groups).ToList();
        }
    }
}
