﻿using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IActionResult Get(string? query)
        {
            return StatusCode(200, _groupService.Get(query));
        }
        [HttpPost]
        public IActionResult Post(Group group)
		{
            return StatusCode(200, _groupService.Post(group));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return StatusCode(200, _groupService.Delete(id));
        }
    }
}
