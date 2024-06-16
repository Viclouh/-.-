using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Models;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly GroupService _groupService;
		public GroupController(GroupService groupService, IMapper mapper)
		{
			_groupService= groupService;
			_mapper= mapper;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			return StatusCode(200, _groupService.GetAll().ToList());
		}
	}
}
