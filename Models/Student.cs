using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    /// <summary>
    /// 學生模型、領域類、object-relation-mapper  ORM、entitly......
    /// </summary>
    public class Student
    {

        //已移至StudentCreateViewModel.cs

        //public int deleteProperty { get; set; }

        //public int SomeProperty { get; set; }
        public string Name { get; internal set; }
        public int Id { get; internal set; }
        public ClassNameEnum ClassName { get; internal set; }
        public string Email { get; internal set; }

        public string PhotoPath { get; set; }
    }
}

