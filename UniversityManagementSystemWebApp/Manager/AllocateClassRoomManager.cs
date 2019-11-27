using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.View;

namespace UniversityManagementSystemWebApp.Manager
{
    public class AllocateClassRoomManager
    {
        private AllocateClassRoomGateway allocateClassRoomGateway;

        public AllocateClassRoomManager()
        {
            allocateClassRoomGateway = new AllocateClassRoomGateway();
        }
        public List<Day> GetAllDays()
        {
            return allocateClassRoomGateway.GetAllDays();
        }
        public List<Room> GetAllRooms()
        {
            return allocateClassRoomGateway.GetAllRooms();
        }
        public List<AllocateClassRoomView> GetAllAllocateInfoByDepartmentId(int? departmentId)
        {
            return allocateClassRoomGateway.GetAllAllocateInfoByDepartmentId(departmentId);
        }
        public List<ClassScheduleView> GetAllocateInfoByDepartmentId(int? departmentId)
        {
           var allocateInfo =  allocateClassRoomGateway.GetAllAllocateInfoByDepartmentId(departmentId);
            int count = -1;
            string code="";
            string scheduleInfo = "";
            List<ClassScheduleView> classScheduleViews = new List<ClassScheduleView>();
            foreach (AllocateClassRoomView value in allocateInfo)
            {
                ClassScheduleView classScheduleView = new ClassScheduleView();
                classScheduleView.Code = value.CourseCode;
                classScheduleView.Name = value.CourseTitle;
                string roomNumber = value.RoomNumber;

                if (roomNumber == "Not Scheduled Yet")
                {
                    scheduleInfo = value.RoomNumber;
                    classScheduleView.Schedule = scheduleInfo;
                    classScheduleViews.Add(classScheduleView);
                    count++;
                }
                else if (code == classScheduleView.Code)
                {
                    scheduleInfo = scheduleInfo+";<br/>R. No: " + value.RoomNumber + ", " + value.Day + ", " + value.FromTime + " - " +
                                  value.ToTime;
                    classScheduleView.Schedule = scheduleInfo;
                    classScheduleViews.RemoveAt(count);
                    classScheduleViews.Add(classScheduleView);
                }
                else
                {
                    scheduleInfo = "R. No: " + value.RoomNumber + ", " + value.Day + ", " + value.FromTime + " - " +
                                   value.ToTime;
                    classScheduleView.Schedule = scheduleInfo;
                    classScheduleViews.Add(classScheduleView);
                    count++;
                    code = value.CourseCode;
                }
            }
            return classScheduleViews;
        }

        public string Save(AllocateClassRoom allocateClassRoom)
        {
            var allocateClassRoomInfo = allocateClassRoomGateway.GetAllocateInfoByDayAndRoomId(allocateClassRoom.DayId,
                allocateClassRoom.RoomId);
            bool notAllowed = false;
            if (allocateClassRoomInfo != null)
            {
                foreach (AllocateClassRoom info in allocateClassRoomInfo)
                {

                    DateTime dbFromTime = DateTime.Parse(info.FromTime);
                    DateTime dbToTime = DateTime.Parse(info.ToTime);
                    DateTime fromTime = DateTime.Parse(allocateClassRoom.FromTime);
                    DateTime toTime = DateTime.Parse(allocateClassRoom.ToTime);
                    if ((fromTime >= dbFromTime && toTime <= dbToTime) || (fromTime < dbFromTime && toTime >= dbToTime) ||
                        (fromTime < dbFromTime && (toTime > dbFromTime && toTime <= dbToTime)) || (fromTime >= dbFromTime && fromTime < dbToTime) && toTime >= dbToTime)
                    {
                        notAllowed = true;
                    }
                }
            }
            if (notAllowed)
            {
                return "Classroom already scheduled in selected time! Choose another time period";
            }
            else
            {
                int rowAffected = allocateClassRoomGateway.Save(allocateClassRoom);
                if (rowAffected > 0)
                {
                    return "Class Room Allocated!";
                }
                return "Failed to allocated class room";
            }
        }
        public string UnAllocateClassRoom()
        {
            int rowAffected = allocateClassRoomGateway.UnAllocateClassRoom();
            if (rowAffected > 0)
            {
                return "Classroom unallocated successfully!";
            }
            return "Failed to unallocate classroom!";
        }
    }
}