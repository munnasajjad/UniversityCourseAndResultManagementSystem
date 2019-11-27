using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class AllocateClassRoomController : Controller
    {
        private DepartmentManager departmentManager;
        private CourseManager courseManager;
        private AllocateClassRoomManager allocateClassRoomManager;

        public AllocateClassRoomController()
        {
            departmentManager = new DepartmentManager();
            courseManager = new CourseManager();
            allocateClassRoomManager = new AllocateClassRoomManager();
        }
        //
        // GET: /AllocateClassRoom/
        public ActionResult AllocateClassRoom()
        {
            ViewBag.Rooms = allocateClassRoomManager.GetAllRooms();
            ViewBag.Days = allocateClassRoomManager.GetAllDays();
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult AllocateClassRoom(AllocateClassRoom allocateClassRoom)
        {
            if (ModelState.IsValid)
            {
                string message;
                DateTime fromTime = DateTime.Parse(allocateClassRoom.FromTime);
                DateTime toTime = DateTime.Parse(allocateClassRoom.ToTime);
                if (fromTime > toTime)
                {
                    message = "Invalid time range!";
                    ViewBag.Message = message;
                }
                else
                {
                    message = allocateClassRoomManager.Save(allocateClassRoom);
                    ViewBag.Message = message;
                }
                ModelState.Clear();
            }
            else
            {
                ViewBag.Message = "Model state is invalid!";
            }
            ViewBag.Rooms = allocateClassRoomManager.GetAllRooms();
            ViewBag.Days = allocateClassRoomManager.GetAllDays();
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        [HttpGet]
        public ActionResult UnallocateClassRoom()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnallocateClassRoom(int? i)
        {
            string message = allocateClassRoomManager.UnAllocateClassRoom();
            ViewBag.Message = message;
            return View();
        }


        public ActionResult ViewClassRoomAllocation()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }
        public JsonResult GetAllCoursesByDepartmentId(int? departmentId)
        {
            var courses = courseManager.GetAllCoursesByDepartmentId(departmentId);
            return Json(courses);
        }
        public JsonResult GetAllocateInfoByDepartmentId(int? departmentId)
        {
            var allocateClassRooms = allocateClassRoomManager.GetAllocateInfoByDepartmentId(departmentId);
            return Json(allocateClassRooms);
        }
    }
}