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
            return _context.Teacher.ToList();
        }
        public Teacher Get(int id)
        {
            return _context.Teacher.Where(t => t.Id == id).FirstOrDefault();
        }
        public List<Teacher> Get(string? query)
        {
            if (query.IsNullOrEmpty())
            {
                return _context.Teacher.ToList();
            }

            return _context.Teacher.Where(t => t.Surname.ToLower().Contains(query.ToLower())
            || t.Name.ToLower().Contains(query.ToLower())
            || t.Patronymic.ToLower().Contains(query.ToLower())).ToList();
        }

        public Teacher Put(Teacher newTeacher)
        {
            Teacher teacher = _context.Teacher.Where(t=> t.Id == newTeacher.Id).FirstOrDefault();
            teacher.Surname = newTeacher.Surname;
            teacher.Name = newTeacher.Name;
            teacher.Patronymic = newTeacher.Patronymic;
            _context.Teacher.Update(teacher);
            _context.SaveChanges();
            return teacher;
        }
    }
}
