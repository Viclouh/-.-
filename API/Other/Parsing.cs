using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using API.Models;
using API.Database;

namespace API.Other
{
    public class Parsing
    {
        public string Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Other\raspisanie.xls");
        public List<Cabinet> Cabinets = new List<Cabinet>();
        public List<Course> Courses = new List<Course>();
        public List<Group> Groups = new List<Group>();
        public List<Discipline> Discipline = new List<Discipline>();
        public List<Teacher> Teachers = new List<Teacher>();
        public List<Teacher_Discipline> Teacher_Discipline = new List<Teacher_Discipline>();
        Excel.Range UsedRange = null;


        public void ParseAllData()
        {
            object rOnly = true;
            object SaveChanges = false;
            object MissingObj = System.Reflection.Missing.Value;

            Excel.Application app = new Excel.Application();
            Excel.Workbooks workbooks = app.Workbooks;
            Excel.Workbook workbook = workbooks.Open(Path, MissingObj, rOnly, MissingObj, MissingObj,
                                        MissingObj, MissingObj, MissingObj, MissingObj, MissingObj,
                                        MissingObj, MissingObj, MissingObj, MissingObj, MissingObj);

            // Получение всех страниц докуента
            Excel.Sheets sheets = workbook.Sheets;
            foreach (Excel.Worksheet worksheet in sheets)
            {
                UsedRange = worksheet.UsedRange;

                ParseCoursesAndGroups();
                ParseTeachers();
                ParseCabinets();

            }
        }
        //парсинг всей информации
        public List<Task> ParseAllDataAsync()
        {
            object rOnly = true;
            object SaveChanges = false;
            object MissingObj = System.Reflection.Missing.Value;

            Excel.Application app = new Excel.Application();
            Excel.Workbooks workbooks = app.Workbooks;
            Excel.Workbook workbook = workbooks.Open(Path, MissingObj, rOnly, MissingObj, MissingObj,
                                        MissingObj, MissingObj, MissingObj, MissingObj, MissingObj,
                                        MissingObj, MissingObj, MissingObj, MissingObj, MissingObj);

            // Получение всех страниц докуента
            Excel.Sheets sheets = workbook.Sheets;
            foreach (Excel.Worksheet worksheet in sheets)
            {
                UsedRange = worksheet.UsedRange;

                List<Task> tasks = new List<Task>()
                {
                    new Task(ParseCabinets),
                    new Task(ParseCoursesAndGroups),
                    new Task(ParseTeachers),
                    new Task(ParseCabinets)
                };
                foreach (Task task in tasks)
                {
                    task.Start();
                }
                return tasks;
            }
            return null;
        }

        //парсинг направлений и групп
        public void ParseCoursesAndGroups()
        {
                int CourseId = 1;
                int GroupId = 1;
                for (int i = 3; ; i++)
                {
                    Excel.Range CellRange = UsedRange[9, i];
                    string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                        (CellRange as Excel.Range).Value2.ToString();
                    if (CellText != null)
                    {
                        string[] group = CellText.Split(new char[] { '-' });
                        if (Courses.Where(x => x.Shortname == group[0]).FirstOrDefault() == null)
                        {
                            Courses.Add(new Course
                            {
                                Name = group[0],
                                Shortname = group[0],
                                SpecialityId = group[0] == "ИБ" ? 5 : 1,
                            });
                            CourseId++;
                        }
                        Course CurrentCourse = Courses.Where(x => x.Name == group[0]).FirstOrDefault();
                        Groups.Add(new Group
                        {
                            Id = GroupId,
                            CourseId = CurrentCourse.Id,
                            Course = CurrentCourse,
                            Code = group[1]
                        });
                        GroupId++;
                    }
                    else break;
                }
        }
        //парсинг преподов
        public void ParseTeachers()
        {
 
                int teacherId = 1;
                for (int x = 3; x < 36; x++)
                {
                    for (int y = 10; y < 159; y++)
                    {
                        Excel.Range CellRange = UsedRange.Cells[x, y];
                        string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                            (CellRange as Excel.Range).Value2.ToString();
                        if (CellText != null)
                        {
                            if (CellText.Contains(" ") && CellText.Contains("."))
                            {
                                string[] teacher = CellText.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
                                if (teacher.Length == 3 || teacher.Length == 5)
                                {
                                    if (Teachers.Where(t => t.Name == teacher[1] && t.Patronymic == teacher[2] && t.Surname == teacher[0]).FirstOrDefault() == null
                                        && teacher[1].Length == 1 && teacher[2].Length == 1 && teacher[0].Length > 2)
                                    {
                                        Teachers.Add(new Teacher
                                        {
                                            Id = teacherId,
                                            Name = teacher[1],
                                            Surname = teacher[0],
                                            Patronymic = teacher[2],
                                        });
                                        teacherId++;
                                    }
                                }
                            }
                        }
                    }
                }
        }
        //парсинг кабинетов
        public void ParseCabinets()
        {
            int cabinetId = 1;
            for (int x = 3; x < 36; x++)
            {
                for (int y = 10; y < 159; y++)
                {
                    Excel.Range CellRange = UsedRange.Cells[x, y];
                    string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                        (CellRange as Excel.Range).Value2.ToString();
                    if (CellText != null)
                    {
                        if (CellText.Contains("ауд."))
                        {
                            if (Cabinets.Where(c => c.Number == Convert.ToInt32(CellText.Remove(0, CellText.Length - 3))).FirstOrDefault() == null)
                            {
                                Cabinets.Add(new Cabinet { Id = cabinetId, Number = Convert.ToInt32(CellText.Remove(0, CellText.Length - 3)) });
                                cabinetId++;
                            }
                        }
                    }
                }
            }
        }
        public void ParseObjects()
        {
            int subjectId = 1;
            for (int x = 3; x < 36; x++)
            {
                for (int y = 10; y < 159; y++)
                {
                    Excel.Range CellRange = UsedRange.Cells[x, y];
                    string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
                        (CellRange as Excel.Range).Value2.ToString();
                    if (CellText != null)
                    {

                    }
                }
            }
        }
    }
}
