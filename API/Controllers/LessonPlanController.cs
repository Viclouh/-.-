﻿using API.DTO;
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
    public class LessonPlanController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly LessonPlanService _lessonPlanService;
        private readonly NotificationService _notificationService;

        public LessonPlanController(LessonPlanService lessonPlanService, IMapper mapper, NotificationService notificationService)
        {
            _lessonPlanService = lessonPlanService;
            _mapper = mapper;
            _notificationService = notificationService;

        }

        [HttpGet]
        public IActionResult GetAll(string formatting = "Standard")
        {
            switch (formatting)
            {
                case "Standard":
                    {
                        List<LessonPlanDTO> lessons = new List<LessonPlanDTO>();
                        foreach (var item in _lessonPlanService.GetAll())
                        {
                            lessons.Add(_mapper.Map<LessonPlanDTO>(item));
                        }
                        return StatusCode(200, lessons);
                    }
                case "MobileApp":
                    {
                        List<LessonPlanForMobileDTO> lessons = new List<LessonPlanForMobileDTO>();
                        foreach (var item in _lessonPlanService.GetAll())
                        {
                            lessons.Add(_mapper.Map<LessonPlanForMobileDTO>(item));
                        }
                        return StatusCode(200, lessons);
                    }
            }
            return StatusCode(400);

        }
        [HttpGet("Pdf")]
        public IActionResult GetLessonPlanPdf(int? teacherId, int? groupId)
        {
            try
            {
                return StatusCode(200, _lessonPlanService.GetPDF( teacherId, groupId));
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
            LessonPlanDTO lesson = _mapper.Map<LessonPlanDTO>(_lessonPlanService.GetByParameters(weekday, groupId, weekNumber, lessonNumber));
            if (lesson == null || weekday < 1 || weekday > 7 || lessonNumber < 1 || lessonNumber > 6 || weekNumber > 1 || weekNumber < 0)
            {
                return StatusCode(400);
            }
            return StatusCode(200, lesson);
        }


        [HttpGet]
        [Route("Search")]
        public IActionResult Search(int? teacherId, int? groupId, int? audienceId, string formatting = "Standard")
        {
            IEnumerable<LessonPlan> lessons = _lessonPlanService.Search(teacherId, groupId, audienceId);
            switch (formatting)
            {
                case "Standard":
                    return StatusCode(200, lessons.Select(item => _mapper.Map<LessonPlanDTO>(item)));

                case "MobileApp":
                    return StatusCode(200, lessons.Select(item => _mapper.Map<LessonPlanForMobileDTO>(item)));
            }
            return StatusCode(400, "could not find format mode");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_lessonPlanService.Delete(id))
                return StatusCode(200, id);
            else
                return StatusCode(400);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LessonPlanDTO lesson)
        {
            LessonPlan newLesson = _lessonPlanService.Post(lesson);

            if (newLesson.WeekNumber != null)
            {
                var notificationMessage = _notificationService.GetScheduleChangeMessage((int)newLesson.WeekNumber, newLesson.Weekday);
                await _notificationService.SendNotificationAsync(notificationMessage, "group", newLesson.GroupId);

                foreach (var teacher in lesson.Teachers)
                {
                    await _notificationService.SendNotificationAsync(notificationMessage, "teacher", teacher.Id);
                }
            }

            return StatusCode(200, _mapper.Map<LessonPlanDTO>(newLesson));
        }

        [HttpPut]
        public IActionResult Put([FromBody] LessonPlanDTO lesson)
        {
            LessonPlan newLesson = _lessonPlanService.Put(lesson);

            return StatusCode(200, _mapper.Map<LessonPlanDTO>(newLesson));
        }
    }
}
