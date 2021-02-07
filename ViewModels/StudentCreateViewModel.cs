using Microsoft.AspNetCore.Http;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請輸入名字"), MaxLength(50, ErrorMessage = "名字長度不能超過50字符")]
        public string Name { get; set; }

        [Display(Name = "班級信息")]
        public ClassNameEnum ClassName { get; set; }
        [Required]
        [Display(Name = "郵件")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "郵件格式不正確")]

        public string Email { get; set; }

        //添加頭像
        [Display(Name="圖片")]
       
        //一次單圖
        //public IFormFile Photo { get; set; }
        //一次多圖上傳
        public List<IFormFile> Photos { get; set; }
    }
}
