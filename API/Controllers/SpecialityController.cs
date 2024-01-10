using API.DTO;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SpecialityController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly SpecialityService _specialityService;

		public SpecialityController(SpecialityService specialityService, IMapper mapper)
		{
			_specialityService = specialityService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAll() 
		{
		List<SpecialityDTO> specialities = new List<SpecialityDTO>();
			foreach (var item in _specialityService.GetAll())
			{
				specialities.Add(_mapper.Map<SpecialityDTO>(item));
			}
			return StatusCode(200, specialities);
		}
	}
}
