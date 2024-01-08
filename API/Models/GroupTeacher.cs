namespace API.Models
{
<<<<<<<< HEAD:API/Models/Teacher_Discipline.cs
    public class Teacher_Discipline
========
    public class GroupTeacher
>>>>>>>> main:API/Models/GroupTeacher.cs
    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; }
<<<<<<<< HEAD:API/Models/Teacher_Discipline.cs
        public int SubjectId { get; set; }
        public Discipline Subject { get; set; }
========
        public Subject Subject { get; set; }
        public Group Group { get; set; }
        public bool IsGeneral { get; set; }
>>>>>>>> main:API/Models/GroupTeacher.cs
    }
}
