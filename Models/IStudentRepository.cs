using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IStudentRepository  //目前介面有三個方法
    {
        //返回「Student」這個類，建立GetStudent方法
        /// <summary>
        /// 通過id獲取學生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);

        //增加一個方法(28集MVC呈現學生列表信息用
        /// <summary>
        /// 獲取所有學生信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetAllStudents();

        //增加一個方法(41集模型綁定
        /// <summary>
        /// 添加學生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Add(Student student);

        /// <summary>
        /// 更新一個學生信息
        /// </summary>
        /// <param name="updateStudent"></param>
        /// <returns></returns>
        Student Update(Student updateStudent);

       /// <summary>
       /// 刪除一個學生信億
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        Student Delete(int id);
    }
}
