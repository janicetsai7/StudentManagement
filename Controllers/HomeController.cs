using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;


namespace StudentManagement.Controllers
{
    //[Route("Home")]
    //如控制器有很多個，則使用標記
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// 使用構造函數注入的方式注入IStudentRepository
        /// </summary>
        /// <param name="studentRepository"></param>

        // 使用構造函數注入的方式注入IStudentRepository

        public HomeController(
            IStudentRepository studentRepository, IHostingEnvironment hostingEnvironment)
        {
            _studentRepository = studentRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        //public string Index()
        //public string Index()
        //{
        //return "Hello World From HomeControllers";
        //返回學生的名字
        //return _studentRepository.GetStudent(1).Name;
        //return Json(new { id = "1", name = "A Ruby"});
        //}
        //增加一個方法(28集MVC呈現學生列表信息用

        //屬性路由
        //[Route("")]
        //[Route("Home")] 放置於控制器前，則可省略
        //[Route("Index")]
        //使用標記
        //[Route("[action]")]
        //[Route("Home/Index")]
        //[Route("~/")]
        //[Route("~/Home")]

        public IActionResult Index()
        {
            //查詢所有學生信息
            IEnumerable<Student> students = _studentRepository.GetAllStudents();
            //將學生列表傳到視圖
            return View(students);
        }

        //使用屬性路由
        //[Route("Home/Details/{id?}")]
        //使用標記
        //[Route("[action]/{id?}")]
        //[Route("{id?}")]
        //public IActionResult Details(int id)
        //透過C#語法將若id為空則有預設值
        //public ViewResult Details(int? id)
        //{
        //    //返回用戶數據，使用ViewModel視圖
        //    //實例化HomeDetailsViewModel並儲存Student詳細信息和PageTitle
        //    HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        //    {
        //        //Student = _studentRepository.GetStudent(id),
        //        Student = _studentRepository.GetStudent(id ?? 1),
        //        PageTitle = "學生詳細信息"
        //    };
        //    return View(homeDetailsViewModel);
        //}

        //返回用戶數據，使用強類型視圖
        //Student model = _studentRepository.GetStudent(3);
        //return View(model);  //-->Details.cshtml

        ////返回用戶數據，使用ViewBag視圖
        //Student model = _studentRepository.GetStudent(2);
        //ViewBag.PageTitle = "學生信息(使用ViewBag)";
        //ViewBag.Student = model;
        //return View();

        //返回用戶數據ViewData視圖
        //Student model = _studentRepository.GetStudent(1);
        //ViewData["PageTitle"] = "學生詳情使用ViewData)";
        //ViewData["Student"] = model;
        //return View();

        //return View(model);
        //使用自訂義名稱
        //return View("Test");
        //使用自訂義名稱(絕對路徑需指定完整檔案副檔名)
        //return View("MyViews/Test.cshtml");
        //return View("~MyViews/Test.cshtml");
        //相對路徑寫法
        //return View("../../MyViews/Test");
        //return View("../Test/Details");
        //}

        //public JsonResult Details1()
        //{
        //    //返回用戶數據
        //    Student model = _studentRepository.GetStudent(2);
        //    return Json(model);
        //}

        //public ObjectResult Details2()
        //{
        //    //返回用戶數據
        //    Student model = _studentRepository.GetStudent(3);
        //    return new ObjectResult(model);
        //}



        //57集異常處理
        public IActionResult Details(int id)
        {
            throw new Exception("此異常發生在Details中");
            Student student = _studentRepository.GetStudent(id);
            if (student == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }
            //實例化HomeDetailsViewModel並儲存Student詳細信息和PageTitle
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                //Student = _studentRepository.GetStudent(id),
                Student = _studentRepository.GetStudent(id),
                PageTitle = "學生詳細信息"
            };
            return View(homeDetailsViewModel);
        }

        //40集  產生create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //41集 asp net core模型綁定
        //public String Details(int? id,string name)
        //
        //    return $"id=={id},並且名字為{name}";
        //}

        //[HttpPost]
        //public IActionResult Create(Student student)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Student newStudent = _studentRepository.Add(student);
        //        return RedirectToAction("Details", new { id = newStudent.Id });
        //    }
        //    return View();

        //}

        //第53集
        [HttpPost]

        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //一次單一檔案上傳
                //必須將圖像上傳到wwwroot中的images文件夾
                //而要獲取wwwroot文件夾的路徑，我們需要注入asp.net core提供的HostingEnvironment服務
                //通過HostingEnvironment服務去獲取wwwroot文件夾的路徑
                //string uniqueFileName = null;
                //if (model.Photo != null)
                //{
                //    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                //    string filePath = Path.Combine(uploadFolder , uniqueFileName);
                //    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));

                //}

                //一次多個檔案上傳
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count>0 )
                {
                    foreach(var photo in model.Photos)
                    {
                    //必須將圖像上傳到wwwroot中的images文件夾
                    //而要獲取wwwroot文件夾的路徑，我們需要注入asp.net core提供的HostingEnvironment服務
                    //通過HostingEnvironment服務去獲取wwwroot文件夾的路徑
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    //為了確保文件名是唯一的，我們在文件夾後附加一個新的GUID值和一個下滑線
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                }

                Student newStudent = new Student
                    {
                        Name = model.Name,
                        Email = model.Email,
                        ClassName = model.ClassName,
                        PhotoPath = uniqueFileName
                    };
                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { id = newStudent.Id });
            }
           return View();
        }
    
        
        //55集 編輯
        //1.視圖
        //視圖模型
        //2.對應的頁面調整
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Student student = _studentRepository.GetStudent(id);

            if (student != null)
            {
                StudentEditViewModel studentEditViewModel = new StudentEditViewModel
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    ClassName = student.ClassName,
                    ExistingPhotoPath = student.PhotoPath
                };
                return View(studentEditViewModel);
            }
            throw new Exception("查詢不到這個學生信息");            
;        }
    }
}