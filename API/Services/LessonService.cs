using API.DTO;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Office.Interop.Excel;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;

using iTextSharp.text;
using iTextSharp.text.pdf;

using Document = iTextSharp.text.Document;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace API.Services
{
    public class LessonService
    {
        private Database.Context _context;
        private readonly NotificationService _notificationService;
        public LessonService(NotificationService notificationService, Database.Context context)
        {
            _notificationService = notificationService;
            _context = context;
        }
        private IQueryable<Lesson> GetAllWithIncludes()
        {
            return _context.Lessons
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.LessonGroupTeachers)
                    .ThenInclude(lgt => lgt.Teacher)
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.Group)
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.Subject)
                .Include(ls => ls.Classroom)
                    .ThenInclude(r => r.ClassroomType);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return GetAllWithIncludes();
        }
        public IEnumerable<Lesson> Search(int? teacherId, int? groupId, int? classroom, int? scheduleId, int? department)
        {
            IQueryable<Lesson> query = GetAllWithIncludes()
                .AsNoTracking();
            if (scheduleId.HasValue)
            {
                query = query.Where(item => item.ScheduleId == scheduleId);
            }

            if (department.HasValue)
            {
                query = query.Where(item => item.LessonGroup.Group.Department == department);
            }

            if (teacherId.HasValue)
            {
                query = query.Where(item => item.LessonGroup.LessonGroupTeachers.Any(lgt => lgt.TeacherId == teacherId.Value))
                    .OrderByDescending(l => l.LessonGroup.LessonGroupTeachers.Any(lgt => lgt.TeacherId == teacherId.Value && lgt.IsMain));
            }

            if (groupId.HasValue)
            {
                query = query.Where(item => item.LessonGroup.GroupId == groupId.Value);
            }

            if (classroom.HasValue)
            {
                query = query.Where(item => item.ClassroomId == classroom.Value);
            }

            return query.ToList();
        }

        public Lesson GetByParameters(int weekday, int groupId, int weekNumber, int lessonNumber)
        {
            Lesson lesson = GetAllWithIncludes()
                .Where(l => l.DayOfWeek == weekday
                && l.LessonGroup.GroupId == groupId
                && l.WeekOrderNumber == weekNumber
                && l.LessonNumber == lessonNumber)
                .FirstOrDefault();
            if (lesson != null)
            {
                return lesson;
            }
            if (_context.Groups.Where(g => g.Id == groupId).FirstOrDefault() != null)
            {
                return new Lesson
                {
                    DayOfWeek = weekday,
                    WeekOrderNumber = weekNumber,
                    LessonNumber = lessonNumber,
                    LessonGroup = new LessonGroup()
                    {
                        GroupId = _context.Groups.Where(g => g.Id == groupId).FirstOrDefault().Id
                    }
                };
            }
            return null;
        }

        public int Delete(int id)
        {
            var item = _context.Lessons.FirstOrDefault(l => l.Id == id);
            if (item == null)
            {
                return 0;
            }

            _context.Lessons.Remove(item);
            _context.SaveChanges();
            return id;
        }

        public Lesson Post(LessonDTO lesson, Schedule schedule, List<dynamic> teachers)
        {
            LessonGroup lessonGroup;
            if (_context.LessonGroups
                .Include(lg => lg.Group)
                .Include(lg => lg.Subject)
                .Any(lg => lg.SubjectId == lesson.Subject.Id && lg.Group.Id == lesson.Group.Id && lg.ScheduleType == "1"))
            {
                lessonGroup = _context.LessonGroups
                .Include(lg => lg.Group)
                .Include(lg => lg.Subject)
                .FirstOrDefault(lg => lg.SubjectId == lesson.Subject.Id && lg.Group.Id == lesson.Group.Id && lg.ScheduleType == "1");
            }
            else
            {
                lessonGroup = new LessonGroup()
                {
                    GroupId = lesson.Group.Id,
                    SubjectId = lesson.Subject.Id,
                    ScheduleType = "1"
                };
                _context.LessonGroups.Add(lessonGroup);
                _context.SaveChanges();
            }

            var newLesson = new Lesson
            {
                LessonNumber = lesson.LessonNumber,
                DayOfWeek = lesson.Weekday,
                IsRemote = lesson.isDistantce,
                WeekOrderNumber = lesson.WeekNumber,
                ClassroomId = lesson.Audience != null ? lesson.Audience.Id : null,
                ScheduleId = schedule.Id,
                LessonGroup = lessonGroup
            };

            _context.Lessons.Add(newLesson);

            foreach (var teacher in teachers)
            {
                var lessonGroupTeacher = new LessonGroupTeacher()
                {
                    TeacherId = teacher.Id,
                    LessonGroup = lessonGroup,
                    Subgroup = (teachers.IndexOf(teacher) == 2 && teacher.IsMain) ? 2 : 1,
                    IsMain = teacher.IsMain
                };
                if (!_context.LessonGroupTeachers
                    .Any(lgt => lgt.TeacherId == lessonGroupTeacher.TeacherId
                    && lgt.LessonGroup == lessonGroupTeacher.LessonGroup
                    && lgt.Subgroup == lessonGroupTeacher.Subgroup
                    && lgt.IsMain == lessonGroupTeacher.IsMain))
                {
                    _context.LessonGroupTeachers.Add(lessonGroupTeacher);
                }
            }

            _context.SaveChanges();

            return newLesson;
        }

        public Lesson Put(LessonDTO lesson, List<dynamic> teachers)
        {
            var updatedLesson = GetAllWithIncludes().FirstOrDefault(l => l.Id == lesson.Id);

            if (updatedLesson == null)
            {
                throw new Exception("Lesson not found");
            }

            updatedLesson.LessonNumber = lesson.LessonNumber;
            updatedLesson.IsRemote = lesson.isDistantce;
            updatedLesson.WeekOrderNumber = lesson.WeekNumber;
            updatedLesson.ClassroomId = lesson.Audience.Id;

            var lessonGroup = updatedLesson.LessonGroup;
            lessonGroup.SubjectId = lesson.Subject.Id;

            _context.LessonGroupTeachers.RemoveRange(lessonGroup.LessonGroupTeachers);

            foreach (var teacher in teachers)
            {
                var lessonGroupTeacher = new LessonGroupTeacher()
                {
                    TeacherId = teacher.Id,
                    LessonGroup = lessonGroup,
                    Subgroup = (teachers.IndexOf(teacher) == 2 && teacher.IsMain) ? 2 : 1,
                    IsMain = teacher.IsMain
                };

                _context.LessonGroupTeachers.Add(lessonGroupTeacher);
            }

            _context.LessonGroups.Update(lessonGroup);
            _context.Lessons.Update(updatedLesson);
            _context.SaveChanges();

            // Создание сообщения об изменениях
            string changeMessage = _notificationService.GetScheduleChangeMessage(lesson.WeekNumber, lesson.Weekday);

            // Отправка уведомлений для группы
            _notificationService.SendNotificationAsync(changeMessage, "group", lessonGroup.Id.ToString());

            // Отправка уведомлений для каждого преподавателя
            foreach (var teacher in teachers)
            {
                _notificationService.SendNotificationAsync(changeMessage, "teacher", teacher.Id.ToString());
            }

            return updatedLesson;
        }
    
    public string GetPDF(int? teacherId, int? groupId)
        {

            iTextSharp.text.Document document = new Document();

            // Используем относительный путь для выходного PDF

            string desktopPath = AppDomain.CurrentDomain.BaseDirectory;
            string outputPath = Path.Combine(desktopPath, "example_table.pdf");

            PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));
            document.Open();

            // Register the code pages encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            // Use the abstract path for the font file
            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Fonts", "arial.ttf");
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font font = new (baseFont);



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

            string ss = "";

            switch (teacherId, groupId)
            {
                case (null, not null):
                    var group = _context.Groups
                        .FirstOrDefault(x => x.Id == groupId);
                    ss = $"{group.GroupCode}";
                    break;

                case (not null, null):
                    var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
                    ss = $"{teacher.FirstName} {teacher.MiddleName[0]}. {teacher.LastName[0]}.";
                    break;
            }


            Paragraph title = new Paragraph(ss, font);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20; // Отступ после заголовка
            document.Add(title);

            table.AddCell(new Phrase("Дни недели", font));
            table.AddCell(new Phrase("Часы", font));
            table.AddCell(new Phrase("Предметы", font));

            // Добавление нескольких строк с данными
            for (int i = 0; i < 6; i++)
            {
                //ячейка с днем недели
                PdfPCell cell = new PdfPCell(new Phrase(week[i], font));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Rowspan = 12;
                cell.Rotation = 90;
                table.AddCell(cell);
                for (int j = 0; j < 6; j++)
                {
                    //ячейка с часами
                    PdfPCell hoursCell = new PdfPCell(new Phrase(paras[j], font));
                    hoursCell.Rowspan = 2;
                    hoursCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(hoursCell);

                    Lesson lesson = new Lesson();
                    Lesson lesson1 = new Lesson();

                    switch (teacherId, groupId)
                    {
                        case (null, not null):
                            lesson = Search(null, groupId, null, null, null)
                                .FirstOrDefault(lp => lp.DayOfWeek == i + 1 && lp.LessonNumber == j + 1 && lp.WeekOrderNumber == 0);
                            lesson1 = Search(null, groupId, null, null, null)
                                .FirstOrDefault(lp => lp.DayOfWeek == i + 1 && lp.LessonNumber == j + 1 && lp.WeekOrderNumber == 1);
                            break;
                        case (not null, null):
                            lesson = Search(teacherId, null, null,null, null)
                                .FirstOrDefault(lp => lp.DayOfWeek == i + 1 && lp.LessonNumber == j + 1 && lp.WeekOrderNumber == 0);
                            lesson1 = Search(teacherId, null, null, null, null)
                                .FirstOrDefault(lp => lp.DayOfWeek == i + 1 && lp.LessonNumber == j + 1 && lp.WeekOrderNumber == 1);
                            break;
                    }

                    switch ((lesson, lesson1))
                    {
                        case (not null, not null):
                            table.AddCell(new PdfPCell(new Phrase(groupId != null ? lesson.LessonGroup.Subject.Name : $"{lesson.LessonGroup.Group.GroupCode} {lesson.LessonGroup.Subject.Name}", font)));
                            table.AddCell(new PdfPCell(new Phrase(groupId != null ? lesson.LessonGroup.Subject.Name : $"{lesson1.LessonGroup.Group.GroupCode}   {lesson1.LessonGroup.Subject.Name}", font)));
                            break;

                        case (not null, null):
                            PdfPCell cell1 = new PdfPCell(new Phrase(groupId != null ? lesson.LessonGroup.Subject.Name : $"{lesson.LessonGroup.Group.GroupCode} {lesson.LessonGroup.Subject.Name}", font));
                            cell1.Rowspan = 2;
                            table.AddCell(cell1);
                            break;

                        case (null, not null):
                            table.AddCell("  ");
                            table.AddCell(new PdfPCell(new Phrase(groupId != null ? lesson1.LessonGroup.Subject.Name : $"{lesson1.LessonGroup.Group.GroupCode} {lesson1.LessonGroup.Subject.Name}", font)));
                            break;

                        case (null, null):
                            PdfPCell cellEmpty = new PdfPCell(new Phrase("  ", font));
                            cellEmpty.Rowspan = 2;
                            table.AddCell(cellEmpty);
                            break;
                    }
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
