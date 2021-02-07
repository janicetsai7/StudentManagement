using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class AppDbContext : DbContext
    {
        //初始化 option選擇數據庫並與appdbcontext連接，使用第二個重構方法base 將信息傳遞出去
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //領域類與數據庫連接dbset 屬性
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Student>().HasData(
            //    new Student
            //    {
            //        Id = 1,
            //        Name = "圓圓",
            //        ClassName = ClassNameEnum.FirstGrade,
            //        Email = "oo@gmail.com",
            //    },
            //    new Student
            //    {
            //    Id = 2,
            //        Name = "彌豆",
            //        ClassName = ClassNameEnum.SecondGrade,
            //        Email = "donet@gmail.com",
            //    }
            //    );
            modelBuilder.Seed();
        }

        }
    }
//要在pm 執行「add-migration <name>」, 要先安裝延伸模組Microsoft.EntityFrameworkCore.Tools和
//Microsoft.EntityFrameworkCore.Design