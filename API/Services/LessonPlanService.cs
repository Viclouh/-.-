using API.Models;
using API.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Reflection.Metadata.Ecma335;
using System;
using System.Net.Http.Headers;
using Document = iTextSharp.text.Document;
using System.Text;

namespace API.Services
{
    public class LessonPlanService 
    {
        private Database.Context _context;
        public LessonPlanService(Database.Context context)
        {
            _context = context;
        }
        public IEnumerable<LessonPlan> GetAll()
        {
            return _context.LessonPlan
                .Include(ls => ls.Subject)
                .Include(ls => ls.Audience)
                    .ThenInclude(a => a.AudienceType)
                .Include(ls => ls.Group)
                    .ThenInclude(g=>g.Speciality)
                .Include(ls => ls.LessonTeachers)
                    .ThenInclude(lt => lt.Teacher);
        }
        public IEnumerable<LessonPlan> GetByGroup(int id) {
            return _context.LessonPlan
               .Include(ls => ls.Subject)
               .Include(ls => ls.Audience)
                   .ThenInclude(a => a.AudienceType)
               .Include(ls => ls.Group)
                   .ThenInclude(g => g.Speciality)
               .Include(ls => ls.LessonTeachers)
                   .ThenInclude(lt => lt.Teacher).Where(x=>x.Group.Id == id);
        }
        public IEnumerable<LessonPlan> Search(int? teacherId, int? groupId, int? audienceId)
        {
            IQueryable<LessonPlan> query = _context.LessonPlan.Include(lp => lp.LessonTeachers).ThenInclude(lt=>lt.Teacher).Include(lp => lp.Audience).Include(lp => lp.Group).ThenInclude(g=>g.Speciality).Include(lp=>lp.Subject).AsNoTracking();

            if (teacherId.HasValue)
            {
                query = query.Where(item => item.LessonTeachers.Any(lt=>lt.TeacherId == teacherId.Value))
                    .OrderByDescending(lp=>lp.LessonTeachers.FirstOrDefault(lt => lt.TeacherId == teacherId.Value).IsGeneral);
            }

            if (groupId.HasValue)
            {
                query = query.Where(item => item.GroupId == groupId.Value);
            }

            if (audienceId.HasValue)
            {
                query = query.Where(item => item.AudienceId == audienceId.Value);
            }

            return query.ToList();
        }

        public LessonPlan GetByParameters(int weekday, int groupId, int weekNumber, int lessonNumber)
        {
            LessonPlan lesson = _context.LessonPlan
                .Include(ls => ls.Subject)
                .Include(ls => ls.Audience)
                    .ThenInclude(a => a.AudienceType)
                .Include(ls => ls.Group)
                    .ThenInclude(g => g.Speciality)
                .Include(ls => ls.LessonTeachers)
                    .ThenInclude(lt => lt.Teacher)
                .Where(ls => ls.Weekday == weekday
                && ls.Group.Id == groupId
                && ls.WeekNumber == weekNumber
                && ls.LessonNumber == lessonNumber)
                .FirstOrDefault();
            if (lesson != null)
            {
                return lesson;
            }
            return null;
		}

        public bool Delete(int id)
        {
            var item  = _context.LessonPlan.Where(lp => lp.Id == id).FirstOrDefault();

            if(item == null)
            {
                return false;
            }

            _context.LessonPlan.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public LessonPlan Post(LessonPlanDTO lesson)
        {

            var newLesson = new LessonPlan
            {
                LessonNumber = lesson.LessonNumber,
                Weekday = lesson.Weekday,
                GroupId = lesson.Group.Id,
                AudienceId = lesson.Audience != null ? lesson.Audience.Id : null,
                SubjectId = lesson.Subject.Id,
                WeekNumber = lesson.WeekNumber,
            };

            _context.LessonPlan.Add(newLesson);

            _context.SaveChanges();

            newLesson = _context.LessonPlan.Where(lp => lp.LessonNumber == newLesson.LessonNumber &&
            lp.Weekday == newLesson.Weekday &&
            lp.GroupId == newLesson.GroupId &&
            lp.WeekNumber == newLesson.WeekNumber).FirstOrDefault();

            _context.LessonTeacher.Add(new LessonTeacher
            {
                Lesson = newLesson,
                TeacherId = lesson.Teachers.First().Id,
                IsGeneral = true,
            });

            if (lesson.Teachers.Last() != null)
            {
                _context.LessonTeacher.Add(new LessonTeacher
                {
                    Lesson = newLesson,
                    TeacherId = lesson.Teachers.Last().Id,
                    IsGeneral = false,
                });
            }

            _context.SaveChanges();

            return GetByParameters(lesson.Weekday, lesson.Group.Id, lesson.WeekNumber, lesson.LessonNumber);
        }

        public LessonPlan Put(LessonPlanDTO lesson)
        {
            var updatedLesson = GetByParameters(lesson.Weekday, lesson.Group.Id, lesson.WeekNumber, lesson.LessonNumber);

            updatedLesson.LessonTeachers.First().TeacherId = lesson.Teachers.First().Id;

            if (lesson.Teachers.Last() != null)
            {
                if (updatedLesson.LessonTeachers.First() == updatedLesson.LessonTeachers.Last() )
                {
                    _context.LessonTeacher.Add(new LessonTeacher { Lesson = updatedLesson, TeacherId =  lesson.Teachers.Last().Id, IsGeneral = false});
                }
                else
                {
                    var secondTeacher = updatedLesson.LessonTeachers.Last();
                    secondTeacher.TeacherId = lesson.Teachers.Last().Id;
                    _context.LessonTeacher.Update(secondTeacher);
                }
            }

            updatedLesson.AudienceId = lesson.Audience == null ? null : lesson.Audience.Id;
            updatedLesson.SubjectId = lesson.Subject.Id;
            _context.LessonPlan.Update(updatedLesson);

            _context.SaveChanges();

            return GetByParameters(lesson.Weekday, lesson.Group.Id, lesson.WeekNumber, lesson.LessonNumber);
        }
        public string GetPDF(int? teacherId, int? groupId, int? audienceId)
        {

            iTextSharp.text.Document document = new Document();

            //string outputPath = "C:\\Users\\User\\Desktop\\example_table.pdf";
            //PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
            //document.Open();
            //System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            //Encoding.RegisterProvider(ppp);
            //BaseFont baseFont = BaseFont.CreateFont("c:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            //Font font = new Font(baseFont);

            // Используем относительный путь для выходного PDF

            string desktopPath =AppDomain.CurrentDomain.BaseDirectory;
            string outputPath = Path.Combine(desktopPath, "example_table.pdf");

            PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
            document.Open();

            // Register the code pages encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Use the abstract path for the font file
            string fontPath = Path.Combine(Environment.CurrentDirectory, "Fonts", "arial.ttf");
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(baseFont);



            PdfPTable table = new PdfPTable(3);
            float[] widths = new float[] { 0.5f, 2f, 2f }; // Первая колонка уже остальных

            table.SetWidths(widths);
            string[] week = { "ПОНЕДЕЛЬНИК", "ВТОРНИК", "СРЕДА", "ЧЕТВЕРГ", "ПЯТНИЦА", "СУББОТА" };
            string[] paras = { 
                "1-2 (8:30-9:15;9:20-10:05)", 
                "3-4 (10:15-11:00; 11:05-11:50)", 
                "5-6 (12:00-12:45; 12:50-13:35)", 
                "7-8 (14:05-14:50; 14:55-15:40)",
            "9-10 (15:45-16:30; 16:40-17:25)",
            "11-12 (17:35-18:20; 18:25-19:10)"
            };

            string ss = $"{_context.Group
                .Include(ls => ls.Speciality)
                  .Where(x => x.Id == groupId).FirstOrDefault().Speciality.Shortname} - {_context.Group
                .Include(ls => ls.Speciality)
                  .Where(x => x.Id == groupId).FirstOrDefault().Name}";
            Paragraph title = new Paragraph(ss,font);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20; // Отступ после заголовка
            document.Add(title);

            table.AddCell(new Phrase("Дни недели", font));
            table.AddCell(new Phrase("Часы", font));
            table.AddCell(new Phrase("Предметы", font));

            // Добавление нескольких строк с данными
            for (int i = 0; i < 6; i++)
            {
                PdfPCell cell = new PdfPCell(new Phrase(week[i], font));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Rowspan = 6;
                cell.Rotation = 90;
                table.AddCell(cell);
                for (int j = 0; j < 6; j++)
                {
                    table.AddCell(paras[j]);
                    if (GetByParameters(i + 1, (int)groupId, 0, j + 1) != null) 
                    {
                        if (GetByParameters(i + 1, (int)groupId, 1, j + 1) != null)
                        {
                            string str = $"{GetByParameters(i + 1, (int)groupId, 0, j + 1).Subject.Name}\n----------------\n{GetByParameters(i + 1, (int)groupId, 1, j + 1).Subject.Name}";
                            PdfPCell cell1 = new PdfPCell(new Phrase(str, font));
                            table.AddCell(cell1);
                        }
                        else 
                        {
                            PdfPCell cell1 = new PdfPCell(new Phrase(GetByParameters(i + 1, (int)groupId, 0, j + 1).Subject.Name, font));
                            table.AddCell(cell1);
                        }



                    }
                    else table.AddCell("");
                }
            }

            // Добавление таблицы в документ
            document.Add(table);

            // Закрытие документа
            document.Close();

           
            byte[] pdfBytes = File.ReadAllBytes(outputPath);

            // Преобразование массива байтов в строку Base64
            string base64String = Convert.ToBase64String(pdfBytes);


            return base64String;


            Console.WriteLine("PDF файл с таблицей создан: " + outputPath);
        }
        

    }
}
