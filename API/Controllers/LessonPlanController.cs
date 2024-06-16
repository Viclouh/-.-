using API.DTO;
using API.Models;
using API.Services;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LessonService _LessonService;

        public LessonController(LessonService LessonService, IMapper mapper)
        {
            _LessonService = LessonService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll(string formatting = "Standard")
        {
            switch (formatting)
            {
                case "Standard":
                    {
                        List<LessonDTO> lessons = new List<LessonDTO>();
                        foreach (var item in _LessonService.GetAll())
                        {
                            lessons.Add(_mapper.Map<LessonDTO>(item));
                        }
                        return StatusCode(200, lessons);
                    }
                case "MobileApp":
                    {
                        List<LessonForMobileDTO> lessons = new List<LessonForMobileDTO>();
                        foreach (var item in _LessonService.GetAll())
                        {
                            lessons.Add(_mapper.Map<LessonForMobileDTO>(item));
                        }
                        return StatusCode(200, lessons);
                    }
            }
            return StatusCode(400);

        }
        [HttpGet("{groupId}")]
        public IActionResult GetLessonPlanByGroup(int? teacherId, int? groupId, int? audienceId)
        {
            try
            {
                return StatusCode(200, _lessonPlanService.GetPDF( teacherId, groupId, audienceId));
            }
            catch (Exception ex )
            {
                return StatusCode(500,ex.Message+"\n"+ex.StackTrace.ToString());
            }

        }

        [HttpGet("WithParams")]
        public IActionResult GetByParameters(
            [FromQuery][Required] int weekday,
			[FromQuery][Required] int groupId,
			[FromQuery][Required] int weekNumber,
			[FromQuery][Required] int lessonNumber)
		{
            LessonDTO lesson = _mapper.Map<LessonDTO>(_LessonService.GetByParameters(weekday, groupId, weekNumber, lessonNumber));
            if(lesson == null || weekday<1 || weekday>7 ||lessonNumber < 1||lessonNumber>6 || weekNumber > 1 || weekNumber < 0)
            {
                return StatusCode(400);
            }
            return StatusCode(200, lesson);
        }


        [HttpGet]
        [Route("Search")]
        public IActionResult Search(int? teacherId, int? groupId, int? audienceId, int? scheduleId, int? department, string formatting = "Standard")
        {
            IEnumerable<Lesson> lessons = _LessonService.Search(teacherId, groupId, audienceId, scheduleId, department);
            switch (formatting)
            {
                case "Standard":
                    return StatusCode(200, lessons.Select(item => _mapper.Map<LessonDTO>(item)));

                case "MobileApp":
                    return StatusCode(200, lessons.Select(item => _mapper.Map<LessonForMobileDTO>(item)));
            }
            return StatusCode(400, "could not find format mode");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_LessonService.Delete(id) != 0)
                return StatusCode(200, id);
            else
                return StatusCode(400, 0);
        }

        [HttpPost]
        public IActionResult Post([FromBody](LessonDTO lesson, Schedule schedule, List<dynamic> teachers) data)
        {
            Lesson newLesson = _LessonService.Post(data.lesson, data.schedule, data.teachers);

            return StatusCode(200, _mapper.Map<LessonDTO>(newLesson));
        }

        [HttpPut]
        public IActionResult Put([FromBody]LessonWithTeachersDTO lesson)
        {
            Lesson newLesson = _LessonService.Put(lesson);
            return StatusCode(200, _mapper.Map<LessonDTO>(newLesson));
        }

        [HttpGet("Pdf")]
        public IActionResult GetLessonPlanPdf(int? teacherId, int? groupId)
        {
            try
            {
                return StatusCode(200, _LessonService.GetPDF(teacherId, groupId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace.ToString());
            }

        }
    }
}
