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
        public IEnumerable<Group> Get()
        {
            Context context = new Context();
            return context.Groups.Include(x => x.Course)
                .ThenInclude(x => x.Speciality).ToList();
        }
    }
}
