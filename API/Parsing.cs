using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Models;
using API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using System.Diagnostics;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace API
{
    public class ParseConfig
    {
        public int StartColumn { get; set; }
        public int StartRow { get; set; }
        public int[] SkipRows { get; set; }
        public int EndColumn { get; set; }
        public int CountRows { get; set; }
    }
    public class Parsing
    {
        private int Department;
        private Schedule Schedule;

        private ParseConfig _config;
        private Context _context;
        public Parsing(string path, int department, Schedule schedule, Context context)
        {
            Department = department;
            _context = context;
            Schedule = schedule;

            Classrooms = context.Classrooms.ToList();
            Groups = context.Groups.ToList();
            Subjects = context.Subjects.ToList();
            Teachers = context.Teachers.ToList();
            Teacher_Subjects = context.TeacherSubjects.Include(ts => ts.Teacher).Include(ts => ts.Subject).ToList();
            Lessons = context.Lessons.Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.LessonGroupTeachers)
                    .ThenInclude(lgt => lgt.Teacher)
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.Group)
                .Include(l => l.LessonGroup)
                    .ThenInclude(lg => lg.Subject)
                .Include(ls => ls.Classroom)
                    .ThenInclude(r => r.ClassroomType).ToList();
            LessonGroups = context.LessonGroups.Include(lg => lg.Group).Include(lg => lg.Subject).ToList();
            LessonGroupTeachers = context.LessonGroupTeachers
                .Include(lg => lg.Teacher)
                .Include(lg => lg.LessonGroup)
                .ThenInclude(lg => lg.Group)
                .Include(lg => lg.LessonGroup).ThenInclude(lg => lg.Subject).ToList();

            Path = path;
            _config = new ParseConfig
            {
                StartColumn = 3,
                StartRow = 10,
                EndColumn = 36,
                SkipRows = new[] { 34, 59, 84, 109, 134 },
                CountRows = 159
            };
        }

        public string Path { get; set; }
        public List<Classroom> Classrooms;
        public List<API.Models.Group> Groups;
        public List<Subject> Subjects;
        public List<Teacher> Teachers;
        public List<TeacherSubject> Teacher_Subjects;
        public List<Lesson> Lessons;
        public List<LessonGroup> LessonGroups;
        public List<LessonGroupTeacher> LessonGroupTeachers;
        Excel.Range UsedRange = null;
        Excel.Application Application = null;
        Excel.Workbook Workbook = null;

        public void CloseApp()
        {
            Process[] excelProcsOld = Process.GetProcessesByName("EXCEL");
            Excel.Application myExcelApp = null;
            Excel.Workbooks excelWorkbookTemplate = null;
            Excel.Workbook excelWorkbook = null;
            try
            {
                //DO sth using myExcelApp , excelWorkbookTemplate, excelWorkbook
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //Compare the EXCEL ID and Kill it 
                Process[] excelProcsNew = Process.GetProcessesByName("EXCEL.EXE");
                foreach (Process procNew in excelProcsNew)
                {
                    int exist = 0;
                    foreach (Process procOld in excelProcsOld)
                    {
                        if (procNew.Id == procOld.Id)
                        {
                            exist++;
                        }
                    }
                    if (exist == 0)
                    {
                        procNew.Kill();
                    }
                }
            }
            //Workbook.Close(true);
            //Application.Quit();
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(Application);
        }
        //парсинг всей информации
        public bool ParseAllDataAsync()
        {
            object rOnly = true;
            object SaveChanges = false;
            object MissingObj = System.Reflection.Missing.Value;

            Application = new Excel.Application();
            Excel.Workbooks workbooks = Application.Workbooks;
            Workbook = workbooks.Open(Path, MissingObj, rOnly, MissingObj, MissingObj,
                                        MissingObj, MissingObj, MissingObj, MissingObj, MissingObj,
                                        MissingObj, MissingObj, MissingObj, MissingObj, MissingObj);

            // Получение всех страниц докуента
            Excel.Sheets sheets = Workbook.Sheets;



            foreach (Excel.Worksheet worksheet in sheets)
            {
                UsedRange = worksheet.UsedRange;

                List<Task> tasks = new List<Task>()
                {
                    new Task(ParseGroups),
                    new Task(ParseTeachers),
                    new Task(ParseCabinets),
                    new Task(ParseSubjects),
                };
                foreach (Task task in tasks)
                {
                    task.Start();
                }
                Task.WaitAll(tasks.ToArray());
                ParseLessonGroupTeachers();
                ParseLessons();
                ParseTeacherSubjects();
                _context.Teachers.AddRange(Teachers.Where(t => t.Id == 0));
                _context.Groups.AddRange(Groups.Where(t => t.Id == 0));
                _context.Classrooms.AddRange(Classrooms.Where(t => t.Id == 0));
                _context.Subjects.AddRange(Subjects.Where(t => t.Id == 0));
                _context.SaveChanges();
                _context.LessonGroups.AddRange(LessonGroups.Where(t => t.Id == 0));
                _context.SaveChanges();
                _context.Lessons.AddRange(Lessons.Where(t => t.Id == 0));
                _context.SaveChanges();
                _context.LessonGroupTeachers.AddRange(LessonGroupTeachers.Where(t => t.Id == 0));
                _context.SaveChanges();
                _context.TeacherSubjects.AddRange(Teacher_Subjects.Where(t => t.Id == 0));
                _context.SaveChanges();
                return true;
            }

            return false;

        }

        public async void ParseGroups()
        {
            int CourseId = 1;
            int lastRow = _config.StartRow + _config.CountRows;
            for (int i = 3; i <= lastRow; i++)
            {
                Range CellRange = UsedRange[9, i] as Range;
                string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                    (CellRange as Excel.Range).Value2.ToString();
                if (CellText == null)
                {
                    continue;
                }

                if (!Groups.Any(g => g.Department == Department && g.GroupCode == CellText))
                {
                    Groups.Add(new API.Models.Group
                    {
                        Department = Department,
                        GroupCode = CellText
                    });
                }
            }
            Console.WriteLine("[Finished] парсинг направлений и групп");
        }
        //парсинг преподов
        public async void ParseTeachers()
        {
            int teacherId = 1;
            Regex regex = new Regex(@"[А-ЯЁа-яё\-]+ [А-ЯЁ]\.\s*[А-ЯЁ]\.*");


            for (int x = _config.StartColumn; x < _config.EndColumn; x++)
            {
                for (int y = _config.StartRow; y < _config.CountRows; y++)
                {
                    Range CellRange = UsedRange.Cells[y, x] as Range;
                    string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                        (CellRange).Value2.ToString();
                    if (CellText == null)
                    {
                        continue;
                    }

                    MatchCollection matches = regex.Matches(CellText);

                    if (matches.Count == 0)
                    {
                        continue;
                    }

                    foreach (Match match in matches)
                    {
                        string[] s = match.ToString().Trim().Split(' ', '.');
                        if (Teachers.Any(z => z.LastName == s[0] && z.FirstName == s[1] && z.MiddleName == s[2]))
                        {
                            continue;
                        }
                        Teachers.Add(new Teacher
                        {
                            LastName = s[0],
                            FirstName = s[1],
                            MiddleName = s[2]
                        });
                        teacherId++;
                    }
                }
            }
            Console.WriteLine("[Finished] парсинг преподов");

        }
        //парсинг кабинетов
        public async void ParseCabinets()
        {
            int cabinetId = 1;
            Regex regex = new Regex(@"ауд\.\s*\d+");
            for (int x = 3; x < 36; x++)
            {
                for (int y = 10; y < 159; y++)
                {
                    Range CellRange = UsedRange.Cells[y, x] as Range;
                    string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                        (CellRange as Excel.Range).Value2.ToString();
                    if (CellText == null)
                    {
                        continue;
                    }
                    MatchCollection matches = regex.Matches(CellText);
                    if (matches.Count == 0)
                    {
                        continue;
                    }
                    foreach (Match match in matches)
                    {
                        string[] s = match.ToString().Trim().Split(' ', '.').Where(m => m != "").ToArray();
                        if (Classrooms.Where(z => z.Number == s[1]).Count() == 0)
                        {
                            Classrooms.Add(new Classroom
                            {
                                Number = s[1],
                            });
                            cabinetId++;
                        }
                    }
                }
            }
            Console.WriteLine("[Finished] парсинг кабинет");

        }
        public void ParseSubjects()
        {
            Regex regCab = new Regex(@"ауд\.\s*\d+");
            Regex regTeacher = new Regex(@"[А-ЯЁа-яё\-]+ [А-ЯЁ]\.\s*[А-ЯЁ]\.*");
            for (int x = 3; x < 36; x++)
            {
                for (int y = 10; y < 159; y += 2)
                {
                    if (_config.SkipRows.Contains(y))
                        y++;
                    Range cellRange = UsedRange.Cells[y, x] as Range;
                    Range CellRange = UsedRange.Cells[y, x] as Range;
                    Range NextCellRange = UsedRange.Cells[y + 1, x] as Range;

                    string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                        (CellRange as Excel.Range).Value2.ToString();

                    string NextCellText = (NextCellRange == null || NextCellRange.Value2 == null) ? null :
                        (NextCellRange as Excel.Range).Value2.ToString();

                    string s = String.Concat(CellText, " ", NextCellText);

                    MatchCollection cabMatches = regCab.Matches(s);
                    MatchCollection teacherMatches = regTeacher.Matches(s);
                    if ((Int32)CellRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight == 2)
                    {
                        continue;
                    }

                    foreach (Match m in cabMatches)
                    {
                        s = s.Replace(m.ToString(), "");
                    }

                    foreach (Match m in teacherMatches)
                    {
                        s = s.Replace(m.ToString(), "");
                    }

                    s = s.Contains("Космонавта Комарова 55") ? s.Replace("Космонавта Комарова 55", "") : s;

                    foreach (Match m in new Regex(@"\d{3}").Matches(s))
                        s = s.Replace(m.ToString(), "");

                    s = s.Trim();
                    if (String.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    GetOrCreate(s);
                }
            }
            Console.WriteLine("[Finished] парсинг предметов");

        }

        public async void ParseLessonGroupTeachers()
        {
            for (int x = 3; x < 36; x++)
            {
                API.Models.Group group = GetGroup((UsedRange.Cells[9, x] as Range).Value2 == null ? "" : (UsedRange.Cells[9, x] as Range).Value2.ToString());
                for (int y = 10; y < 159;)
                {
                    string s = "";
                    string s1 = "";
                    bool isOne = true;
                    for (int i = 0; i < 4; i++)
                    {
                        isOne = true;
                        Range CellRange = UsedRange.Cells[y, x] as Range;
                        //-4138
                        s += CellRange.Value2 == null ? "" : CellRange.Value2.ToString() + " ";
                        if (i == 1)
                        {
                            if ((Int32)CellRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight == -4138)
                            {
                                isOne = false;
                                s1 += (UsedRange.Cells[y + 1, x] as Range).Value2 == null ? "" : (UsedRange.Cells[y + 1, x] as Range).Value2.ToString() + " ";
                                s1 += (UsedRange.Cells[y + 2, x] as Range).Value2 == null ? "" : (UsedRange.Cells[y + 2, x] as Range).Value2.ToString() + " ";
                                y += 3;
                                break;
                            }
                        }
                        y++;
                    }
                    if (!string.IsNullOrWhiteSpace(s) || !string.IsNullOrWhiteSpace(s1))
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            var subject = GetSubject(s);
                            if (!LessonGroups.Any(lg => lg.Group == group && lg.Subject == subject))
                            {
                                var lessonGroup = new LessonGroup
                                {
                                    Subject = subject,
                                    Group = group,
                                    ScheduleType = "1"
                                };

                                LessonGroups.Add(lessonGroup);

                                var teachers = GetTeacher(s);
                                foreach (var teacher in teachers)
                                {
                                    var lessonGroupTeacher = new LessonGroupTeacher
                                    {
                                        LessonGroup = lessonGroup,
                                        Teacher = teacher,
                                        Subgroup = 1,
                                        IsMain = teachers.IndexOf(teacher) == 0
                                    };
                                    if (!LessonGroupTeachers.Any(lgt => lgt.LessonGroup == lessonGroup && lgt.Teacher == teacher && lgt.IsMain == lessonGroupTeacher.IsMain))
                                    {
                                        LessonGroupTeachers.Add(lessonGroupTeacher);
                                    }
                                }

                            }
                        }
                        if (!string.IsNullOrEmpty(s1))
                        {
                            Subject subject = GetSubject(s1);
                            if (!LessonGroups.Any(lg => lg.Group == group && lg.Subject == subject))
                            {
                                var lessonGroup = new LessonGroup
                                {
                                    Subject = subject,
                                    Group = group,
                                    ScheduleType = "1"
                                };

                                LessonGroups.Add(lessonGroup);

                                var teachers = GetTeacher(s1);
                                foreach(var teacher in teachers)
                                {
                                    var lessonGroupTeacher = new LessonGroupTeacher
                                    {
                                        LessonGroup = lessonGroup,
                                        Teacher = teacher,
                                        IsMain = teachers.IndexOf(teacher) == 0,
                                        Subgroup = 1,
                                    };
                                    if (!LessonGroupTeachers.Any(lgt => lgt.LessonGroup == lessonGroup && lgt.Teacher == teacher && lgt.IsMain == lessonGroupTeacher.IsMain))
                                    {
                                        LessonGroupTeachers.Add(lessonGroupTeacher);
                                    }
                                }

                            }

                        }
                    }
                }
            }
        }








        public void ParseLessons()
        {
            Regex regCab = new Regex(@"ауд\.\s*\d+");
            Regex regTeacher = new Regex(@"[А-ЯЁа-яё\-]+ [А-ЯЁ]\.\s*[А-ЯЁ]\.*");
            int id = 1;
            int lessonNumber = 1;
            for (int x = 3; x < 36; x++)
            {
                API.Models.Group group = GetGroup((UsedRange.Cells[9, x] as Range).Value2 == null ? "" : (UsedRange.Cells[9, x] as Range).Value2.ToString());
                int weekday = 1;
                for (int y = 10; y < 159;)
                {
                    string s = "";
                    string s1 = "";
                    bool isOne = true;
                    for (int i = 0; i < 4; i++)
                    {
                        isOne = true;
                        Range CellRange = UsedRange.Cells[y, x] as Range;
                        //-4138
                        s += CellRange.Value2 == null ? "" : CellRange.Value2.ToString() + " ";
                        if (i == 1)
                        {
                            if ((Int32)CellRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight == -4138)
                            {
                                isOne = false;
                                s1 += (UsedRange.Cells[y + 1, x] as Range).Value2 == null ? "" : (UsedRange.Cells[y + 1, x] as Range).Value2.ToString() + " ";
                                s1 += (UsedRange.Cells[y + 2, x] as Range).Value2 == null ? "" : (UsedRange.Cells[y + 2, x] as Range).Value2.ToString() + " ";
                                y += 3;
                                break;
                            }
                        }
                        y++;
                    }
                    if (!string.IsNullOrWhiteSpace(s) || !string.IsNullOrWhiteSpace(s1))
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            Console.WriteLine($"{weekday} {lessonNumber} {s}");
                            Classroom cab = GetClassroom(s);
                            Subject subject = GetSubject(s);
                            var lessonGroup = LessonGroups.FirstOrDefault(lg => lg.Group == group && lg.ScheduleType == "1" && lg.Subject == subject);
                            var weekNumber = 0;
                            if (LessonGroups.Any(lg => lg.Group == group && lg.ScheduleType == "1" && lg.Subject == subject) 
                                && !Lessons.Any(l => l.Schedule == Schedule && l.DayOfWeek == weekday 
                                    && l.WeekOrderNumber == weekNumber && l.LessonNumber == lessonNumber && l.LessonGroup == lessonGroup))
                            {
                                Lessons.Add(new Lesson
                                {
                                    Classroom = cab,
                                    IsRemote = false,
                                    Schedule = Schedule,
                                    LessonGroup = lessonGroup,
                                    LessonNumber = lessonNumber,
                                    DayOfWeek = weekday,
                                    WeekOrderNumber = weekNumber,
                                });
                            }

                        }
                        if (!string.IsNullOrEmpty(s1))
                        {
                            Console.WriteLine($"{weekday} {lessonNumber} {s1}");
                            Classroom cab = GetClassroom(s1);
                            Subject subject = GetSubject(s1);
                            var lessonGroup = LessonGroups.FirstOrDefault(lg => lg.Group == group && lg.ScheduleType == "1" && lg.Subject == subject);
                            if (LessonGroups.Any(lg => lg.Group == group && lg.ScheduleType == "1" && lg.Subject == subject)
                                && !Lessons.Any(l => l.Schedule == Schedule && l.DayOfWeek == weekday
                                    && l.WeekOrderNumber == 1 && l.LessonNumber == lessonNumber && l.LessonGroup == lessonGroup))
                            {
                                Lessons.Add(new Lesson
                                {
                                    Classroom = cab,
                                    IsRemote = false,
                                    LessonNumber = lessonNumber,
                                    DayOfWeek = weekday,
                                    WeekOrderNumber = 1,
                                    Schedule = Schedule,
                                    LessonGroup = lessonGroup
                                });
                            }
                        }
                    }
                    lessonNumber++;
                    if (lessonNumber > 6)
                    {
                        y++;
                        weekday++;
                        lessonNumber = 1;
                    }
                }
            }
            Console.WriteLine("[Finished] парсинг уроков");

        }

        public List<Teacher> GetTeacher(string CellText)
        {

            Regex regTeacher = new Regex(@"[А-ЯЁа-яё\-]+ [А-ЯЁ]\.\s*[А-ЯЁ]\.*");
            MatchCollection teacherMatches = regTeacher.Matches(CellText);
            List<Teacher> teachers = new List<Teacher>();
            foreach (var item in teacherMatches)
            {
                string[] str = item.ToString().Split(' ');
                str[1] = str[1].Trim().Replace(".", "").Replace(" ", "");
                teachers.Add(Teachers.First(t => t.LastName == str[0] && t.FirstName + t.MiddleName == str[1]));
            }
            return teachers;
        }
        public API.Models.Group GetGroup(string CellText)
        {
            return Groups.Where(x => x.GroupCode == CellText && x.Department == Department).FirstOrDefault();
        }
        public Subject GetSubject(string s)
        {
            Regex regCab = new Regex(@"ауд\.\s*\d+");
            Regex regTeacher = new Regex(@"[А-ЯЁа-яё\-]+ [А-ЯЁ]\.\s*[А-ЯЁ]\.*");
            MatchCollection cabMatches = regCab.Matches(s);
            MatchCollection teacherMatches = regTeacher.Matches(s);
            foreach (Match m in cabMatches)
            {
                s = s.Replace(m.ToString(), "");
            }

            foreach (Match m in teacherMatches)
            {
                s = s.Replace(m.ToString(), "");
            }
            s = s.Contains("Космонавта Комарова 55") ? s.Replace("Космонавта Комарова 55", "") : s;

            foreach (Match m in new Regex(@"\d{3}").Matches(s))
                s = s.Replace(m.ToString(), "");

            s = s.Trim();
            return GetOrCreate(s);
        }
        public Classroom GetClassroom(string CellText)
        {
            Regex regex = new Regex(@"ауд\.\s*\d+");
            MatchCollection matches = regex.Matches(CellText);
            foreach (Match match in matches)
            {
                string[] s = match.ToString().Trim().Split(' ', '.').Where(m => m != "").ToArray();
                return Classrooms.Where(x => x.Number == s[1]).FirstOrDefault();
            }
            return null;
        }

        private Subject GetOrCreate(string word)
        {
            foreach (var item in Subjects)
            {
                if (word.Contains(item.Name))
                {
                    return item;
                }
                if (IsMatch(item.Name, word))
                {
                    return item;
                }
            }
            Subject sbj = new Subject()
            {
                Name = word,
                ShortName = word
            };
            Subjects.Add(sbj);
            sbj.Id = Subjects.Max(x => x.Id) + 1;
            return sbj;
        }
        private bool IsMatch(string source, string target)
        {
            return LevenshteinDistance(source.ToLower(), target.ToLower()) < source.Length * 0.2;
        }
        public int LevenshteinDistance(string source, string target)
        {
            if (String.IsNullOrEmpty(source))
            {
                if (String.IsNullOrEmpty(target)) return 0;
                return target.Length;
            }
            if (String.IsNullOrEmpty(target)) return source.Length;

            var m = target.Length;
            var n = source.Length;
            var distance = new int[2, m + 1];
            // Initialize the distance 'matrix'
            for (var j = 1; j <= m; j++) distance[0, j] = j;

            var currentRow = 0;
            for (var i = 1; i <= n; ++i)
            {
                currentRow = i & 1;
                distance[currentRow, 0] = i;
                var previousRow = currentRow ^ 1;
                for (var j = 1; j <= m; j++)
                {
                    var cost = (target[j - 1] == source[i - 1] ? 0 : 1);
                    distance[currentRow, j] = Math.Min(Math.Min(
                                distance[previousRow, j] + 1,
                                distance[currentRow, j - 1] + 1),
                                distance[previousRow, j - 1] + cost);
                }
            }
            return distance[currentRow, m];
        }

        private void ParseTeacherSubjects()
        {
            foreach (var lgt in LessonGroupTeachers)
            {
                if (!Teacher_Subjects.Any(ts => ts.TeacherId == lgt.Teacher.Id && ts.Subject.Id == lgt.LessonGroup.Subject.Id))
                {
                    Teacher_Subjects.Add(new TeacherSubject
                    {
                        Teacher = lgt.Teacher,
                        Subject = lgt.LessonGroup.Subject
                    });
                }
            }
        }

    }
}
