using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    //C#中添加ststic變成擴展方法
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "圓圓",
                    ClassName = ClassNameEnum.FirstGrade,
                    Email = "oo@gmail.com",
                },
                new Student
                {
                    Id = 2,
                    Name = "彌豆",
                    ClassName = ClassNameEnum.SecondGrade,
                    Email = "donet@gmail.com",
                }
            );
        }
    }
}
