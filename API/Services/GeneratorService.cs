using API.Database;
using API.Generator;
using API.Generator.Entities;
using API.Models;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;

using NPOI.Util.ArrayExtensions;

using System.Threading;

namespace API.Services
{
    public class GeneratorService
    {
        private ScheduleService _scheduleService { get; set; }
        private Context _context { get; set; }
        public GeneratorService(ScheduleService scheduleService, Context context) {
            _scheduleService = scheduleService;
            _context = context;
        }
        public void RunGenerating(int population, int generations, int year, int semestr)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Generator", "message.json");
            string json = File.ReadAllText(filePath);
            Console.WriteLine("Подготовка данных");
            var workloadData = PrepareData(json, semestr == 1 ? "oneSem" : "twoSem");

            Schedule schedule = new Schedule()
            {
                AcademicYear = year,
                ScheduleStatusId = 4,
                Semester = semestr,
                LastChange = DateTime.Now,
            };

            GeneticGenerator generator = new GeneticGenerator(workloadData, population, generations, 80, schedule);

            var result = generator.RunGeneticAlgorithm(1);
            _context.Lessons.AddRange(result);
            _context.SaveChanges();
        }


        private Dictionary<Group, List<WorkloadTeachers>> PrepareData(string json, string semesterName)
        {
            var originalArray = JArray.Parse(json);
            var transformedArray = new Dictionary<Group, List<WorkloadTeachers>>();
            var teacherCache = new Dictionary<string, Teacher>();
            var subjectCache = new Dictionary<int, Subject>();

            // Кэшируем всех учителей и предметы из базы данных заранее
            var allTeachers = _context.Teachers.ToList();
            foreach (var teacher in allTeachers)
            {
                teacherCache[teacher.FirstName] = teacher;
            }

            var allSubjects = _context.Subjects.ToList();
            foreach (var subject in allSubjects)
            {
                subjectCache[subject.Id] = subject;
            }

            // Списки для накопления новых объектов
            var newTeachers = new List<Teacher>();
            var newSubjects = new List<Subject>();

            foreach (var item in originalArray)
            {
                var group = new Group() { GroupCode = item["group"]["name"].Value<string>(), Department = 1};
                var workloadTeachersList = new List<WorkloadTeachers>();
                var preloadArray = item["preload"];

                var semester = item["group"][semesterName].ToObject<int>();
                
                var semWeeks = item["parserData"][GetSemestrFukingName(semester)].ToObject<int?>();
                if (semWeeks == null)
                {
                    semWeeks = 0;
                }

                foreach (var preloadItem in preloadArray)
                {
                    if (!preloadItem["isShowPerWeek"].Value<bool>())
                        continue;

                    var discipline = preloadItem["Discipline"];
                    var subjectId = discipline["Id"].Value<int>();
                    var subjectName = discipline["Name"].Value<string>();

                    if (!subjectCache.TryGetValue(subjectId, out var subject))
                    {
                        subject = _context.Subjects.Where(sub => sub.Name == subjectName || sub.ShortName == subjectName).FirstOrDefault();
                        if (subject==null)
                        {
                            subject = new Subject
                            {
                                Id = subjectId,
                                Name = subjectName,
                                ShortName = subjectName,
                            };
                            newSubjects.Add(subject);
                        }
                        subjectCache[subjectId] = subject;
                    }

                    var courseSummary = preloadItem["PedagogicalHours"]["CourseSummary"].ToObject<int?[]>();
                    double? hoursBySemestr = (courseSummary != null && courseSummary.Length >= semester) ? courseSummary[semester] : null;
                    double calculatedHoursPerWeek = hoursBySemestr.HasValue ? hoursBySemestr.Value / semWeeks.Value : 0;

                    var workloadTeacher = new WorkloadTeachers
                    {
                        Subject = subject,
                        HoursPerWeek = calculatedHoursPerWeek
                    };

                    var appointments = preloadItem["appointments"]
                        .Where(appt => !string.IsNullOrWhiteSpace((string)appt["FIO"]))
                        .Select(appt => (string)appt["FIO"])
                        .Distinct();

                    var teachers = new List<Teacher>();
                    foreach (var teacherName in appointments)
                    {
                        if (!teacherCache.TryGetValue(teacherName, out var teacher))
                        {
                            string[] fio = new string[3] { "", "", "" };
                            string[] splited = teacherName.Split(' ');
                            for (int i = 0; i < splited.Length && i < 3; i++)
                            {
                                fio[i] = splited[i];
                            }
                            teacher = _context.Teachers.Where(t => t.LastName == fio[0] && t.FirstName.StartsWith(fio[1]) && t.MiddleName.StartsWith(fio[2])).FirstOrDefault();
                            if (teacher ==null)
                            {
                                teacher = new Teacher { FirstName = fio[1], LastName = fio[0], MiddleName = fio[2] };
                                newTeachers.Add(teacher);
                            }
                            teacherCache[teacherName] = teacher;
                        }
                        teachers.Add(teacher);
                    }

                    workloadTeacher.Teachers = teachers;

                    workloadTeachersList.Add(workloadTeacher);
                }

                transformedArray[group] = workloadTeachersList;
            }

            // Сохраняем все новые объекты за один вызов
            if (newSubjects.Any())
            {
                _context.AddRange(newSubjects);
            }

            if (newTeachers.Any())
            {
                _context.AddRange(newTeachers);
            }

            _context.SaveChanges();

            return transformedArray;
        }

        private static string GetSemestrFukingName(int num)
        {
            switch (num)
            {
                case 0:
                    return "OneSemWeeks";
                case 1:
                    return "TwoSemWeeks";
                case 2:
                    return "ThreeSemWeeks";
                case 3:
                    return "FourSemWeeks";
                case 4:
                    return "FiveSemWeeks";
                case 5:
                    return "SixSemWeeks";
                case 6:
                    return "SevenSemWeeks";
                case 7:
                    return "EightSemWeeks";
                case 8:
                    return "NineSemWeeks";
                case 9:
                    return "TeenSemWeeks";
                default:
                    return "OneSemWeeks";
            }
        }
    }
}
