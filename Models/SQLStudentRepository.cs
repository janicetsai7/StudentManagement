using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly AppDbContext context;

        //使用ef core使用依賴注入，建立建構方法
        public SQLStudentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Student Add(Student student)
        {
            //throw new NotImplementedException();
            context.Students.Add(student);
            context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            //throw new NotImplementedException();
            Student student = context.Students.Find(id);
            if(student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            //throw new NotImplementedException();
            return context.Students;
        }

        public Student GetStudent(int id)
        {
            //throw new NotImplementedException();
            return context.Students.Find(id);
        }

        public Student Update(Student updateStudent)
        {
            // throw new NotImplementedException();
            var student = context.Students.Attach(updateStudent);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updateStudent;

        }
    }
}
