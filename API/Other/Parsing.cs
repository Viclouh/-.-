using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using API.Models;
using API.Database;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace API.Other
{
    public class Parsing
    {
        public string Path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Other\raspisanie.xls");
        public List<Cabinet> Cabinets = new List<Cabinet>();
        public List<Course> Courses = new List<Course>();
        public List<Models.Group> Groups = new List<Models.Group>();
        public List<Discipline> Disciplines = new List<Discipline>();
        public List<Teacher> Teachers = new List<Teacher>();
        public List<Teacher_Discipline> Teacher_Discipline = new List<Teacher_Discipline>();
        Excel.Range UsedRange = null;

		public Parsing()
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
			}
		}


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
					new Task(ParseDisciplines),
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
                        Groups.Add(new Models.Group
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
			Regex regex = new Regex(@"[А-ЯЁа-яё\-]+ [А-ЯЁ]\.\s*[А-ЯЁ]\.*");

			for (int x = 3; x < 36; x++)
			{
				for (int y = 10; y < 159; y++)
				{
					Excel.Range CellRange = UsedRange.Cells[y, x];
					string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
						(CellRange as Excel.Range).Value2.ToString();
					if (CellText != null)
					{
						MatchCollection matches = regex.Matches(CellText);
						if (matches.Count > 0)
						{
							foreach (Match match in matches)
							{
								string[] s = match.ToString().Trim().Split(' ', '.');
								if (Teachers.Where(z => z.Surname == s[0] && z.Name == s[1] && z.Patronymic == s[2]).Count() == 0)
								{
									Teachers.Add(new Teacher
									{
										Id = teacherId,
										Surname = s[0],
										Name = s[1],
										Patronymic = s[2]
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
			Regex regex = new Regex(@"ауд\.\s*\d+");
			for (int x = 3; x < 36; x++)
			{
				for (int y = 10; y < 159; y++)
				{
					Excel.Range CellRange = UsedRange.Cells[y, x];
					string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
						(CellRange as Excel.Range).Value2.ToString();
					if (CellText != null)
					{
						MatchCollection matches = regex.Matches(CellText);
						if (matches.Count > 0)
						{
							foreach (Match match in matches)
							{
								string[] s = match.ToString().Trim().Split(' ');
								if (Cabinets.Where(z => z.Number.ToString() == s[1]).Count() == 0)
								{
									Cabinets.Add(new Cabinet
									{
										Id = cabinetId,
										Number = Convert.ToInt32(s[1]),
										CabinetTypeId = 1,
									}) ;
									cabinetId++;
								}
							}
						}
					}
				}
			}
		}
		public void ParseDisciplines()
		{
			int subjectId = 1;
			Regex regCab = new Regex(@"ауд\.\s*\d+");
			Regex regTeacher = new Regex(@"[А-ЯЁа-яё\-]+ [А-ЯЁ]\.\s*[А-ЯЁ]\.*");
			for (int x = 3; x < 36; x++)
			{
				for (int y = 10; y < 159; y += 2)
				{
					if (y == 34 || y == 59 || y == 84 || y == 109 || y == 134)
						y++;
					Excel.Range CellRange = UsedRange.Cells[y, x];
					Excel.Range NextCellRange = UsedRange.Cells[y + 1, x];
					string CellText = (CellRange == null || CellRange.Value2 == null) ? null :
						(CellRange as Excel.Range).Value2.ToString();
					string NextCellText = (NextCellRange == null || NextCellRange.Value2 == null) ? null :
						(NextCellRange as Excel.Range).Value2.ToString();

					string s = String.Concat(CellText, " ", NextCellText);
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
					if (!String.IsNullOrEmpty(s))
					{
						if (Disciplines.Where(z => z.Name == s && z.Shortname == s).Count() == 0)
						{
							Disciplines.Add(new Discipline
							{
								Id = subjectId,
								Name = s,
								Shortname = s,
							});
							subjectId++;
						}
					}
				}
			}
		}
	}
}
