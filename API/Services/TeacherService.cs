﻿using API.Database;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TeacherService
    {
        Database.Context _context;

        public TeacherService(Context context)
        {
            _context = context;
        }
        public List<Teacher> GetAll()
        {
            return _context.Teachers.ToList();
        }

        public Teacher Get(int id)
        {
            return _context.Teachers.Where(t => t.Id == id).FirstOrDefault();
        }
        public List<Teacher> Get(string? query)
        {
            if (query.IsNullOrEmpty())
            {
                return _context.Teachers.OrderBy(t => t.LastName).ToList();
            }

            return _context.Teachers.Where(t => t.LastName.ToLower().Contains(query.ToLower())
            || t.FirstName.ToLower().Contains(query.ToLower())
            || t.MiddleName.ToLower().Contains(query.ToLower())).OrderBy(t => t.LastName).ToList();
        }

        public Teacher Put(Teacher newTeacher)
        {
            Teacher teacher = _context.Teachers.Where(t => t.Id == newTeacher.Id).FirstOrDefault();
            teacher.LastName = newTeacher.LastName;
            teacher.FirstName = newTeacher.FirstName;
            teacher.MiddleName = newTeacher.MiddleName;
            _context.Teachers.Update(teacher);
            _context.SaveChanges();
            return teacher;
        }

        public Teacher Post(Teacher teacher)
        {

            var newTeacher = new Teacher
            {
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName
            };
            _context.Add(newTeacher);
            _context.SaveChanges();
            return newTeacher;
        }

        public bool Delete(int id)
        {
            _context.Teachers.Remove(_context.Teachers.FirstOrDefault(t => t.Id == id));
            _context.SaveChanges();
            return true;
        }
        public Teacher Get(int id)
        {
            return _context.Teacher.Where(t => t.Id == id).FirstOrDefault();
        }
        public List<Teacher> Get(string? query)
        {
            if (query.IsNullOrEmpty())
            {
                return _context.Teacher.OrderBy(t => t.Surname).ToList();
            }

            return _context.Teacher.Where(t => t.Surname.ToLower().Contains(query.ToLower())
            || t.Name.ToLower().Contains(query.ToLower())
            || t.Patronymic.ToLower().Contains(query.ToLower())).OrderBy(t => t.Surname).ToList();
        }

        public Teacher Put(Teacher newTeacher)
        {
            Teacher teacher = _context.Teacher.Where(t => t.Id == newTeacher.Id).FirstOrDefault();
            teacher.Surname = newTeacher.Surname;
            teacher.Name = newTeacher.Name;
            teacher.Patronymic = newTeacher.Patronymic;
            _context.Teacher.Update(teacher);
            _context.SaveChanges();
            return teacher;
        }

        public Teacher Post(Teacher teacher)
        {

            var newTeacher = new Teacher
            {
                Name = teacher.Name,
                Surname = teacher.Surname,
                Patronymic = teacher.Patronymic
            };
            _context.Add(newTeacher);
            _context.SaveChanges();
            return newTeacher;
        }

        public bool Delete(int id)
        {
            _context.Teacher.Remove(_context.Teacher.FirstOrDefault(t => t.Id == id));
            _context.SaveChanges();
            return true;
        }
    }
}
