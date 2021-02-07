using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StudentManagement.Models
{
    //MockStudentRepository 繼承IStudentRepository這個接口
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _studentsList;

        //建立構造函數MockStudentRepository()
        public MockStudentRepository()
        {
            _studentsList = new List<Student>()
            {
                new Student(){Id = 1,Name="大寶", ClassName= ClassNameEnum.FirstGrade , Email="dabao@gmail.com"},
                new Student(){Id = 2,Name="二寶", ClassName=ClassNameEnum.SecondGrade , Email="erbao@gmail.com"},
                new Student(){Id = 3,Name="小寶", ClassName=ClassNameEnum.GradeThree , Email="shabao@gmail.com"},
            };
        }

        public IEnumerable<Student> GetAllStudents()
        {
            //throw new NotImplementedException();
            return _studentsList;
        }

        public Student GetStudent(int id)
        {
            //throw new NotImplementedException();
            return _studentsList.FirstOrDefault(g => g.Id == id);
        }

        public Student Add(Student student)
        {
            student.Id = _studentsList.Max(s => s.Id) + 1;
            _studentsList.Add(student);
            return student;
        }

        public Student Update(Student updateStudent)
        {
            //沒有實現拋出異常，讓程序不報錯而往下執行
            //throw new NotImplementedException();
            Student student = _studentsList.FirstOrDefault(s => s.Id == updateStudent.Id);
            if(student != null)
            {
                student.Name = updateStudent.Name;
                student.Email = updateStudent.Email;
                student.ClassName = updateStudent.ClassName;
            }
            return student;
        }

        public Student Delete(int id)
        {
            //沒有實現拋出異常，讓程序不報錯而往下執行
            //throw new NotImplementedException();
            Student student = _studentsList.FirstOrDefault(d => d.Id == id);
            if(student != null)
            {
                _studentsList.Remove(student);
            }
            return student;
        }
    }
}
