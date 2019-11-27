using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Models;
using UniversityManagementSystemWebApp.Models.View;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class AllocateClassRoomGateway : BaseGateway
    {
        public List<Day> GetAllDays()
        {
            string query = "SELECT * FROM Days";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Day> days = new List<Day>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Day day = new Day();
                day.Id = (int)Reader["Id"];
                day.DayName = Reader["Day"].ToString();
                days.Add(day);
            }
            Reader.Close();
            Connection.Close();
            return days;
        }
        public List<Room> GetAllRooms()
        {
            string query = "SELECT * FROM Rooms ORDER BY RoomNumber ASC";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<Room> rooms = new List<Room>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Room room = new Room();
                room.Id = (int)Reader["Id"];
                room.RoomNumber = Reader["RoomNumber"].ToString();
                rooms.Add(room);
            }
            Reader.Close();
            Connection.Close();
            return rooms;
        }

        public List<AllocateClassRoomView> GetAllAllocateInfoByDepartmentId(int? departmentId)
        {
            string query = "SELECT * FROM AllocateClassRoomView WHERE DepartmentId = '" + departmentId + "' ORDER BY CourseCode ASC";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<AllocateClassRoomView> allocateClassRoomViews = new List<AllocateClassRoomView>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                AllocateClassRoomView allocateClassRoomView = new AllocateClassRoomView();
                allocateClassRoomView.CourseCode = Reader["CourseCode"].ToString();
                allocateClassRoomView.CourseTitle = Reader["CourseTitle"].ToString();
                allocateClassRoomView.RoomNumber = Reader["RoomNumber"].ToString();
                if (allocateClassRoomView.RoomNumber != "Not Scheduled Yet")
                {
                    allocateClassRoomView.DepartmentId = (int)Reader["DepartmentId"];
                    allocateClassRoomView.CourseId = (int)Reader["CourseId"];
                    allocateClassRoomView.RoomId = (int)Reader["RoomId"];
                    allocateClassRoomView.DayId = (int)Reader["DayId"];
                    allocateClassRoomView.FromTime = Reader["FromTime"].ToString();
                    allocateClassRoomView.ToTime =Reader["ToTime"].ToString();
                    allocateClassRoomView.Day = Reader["Day"].ToString();
                    allocateClassRoomView.Action = Reader["Action"].ToString();
                }
                allocateClassRoomViews.Add(allocateClassRoomView);
            }
            Reader.Close();
            Connection.Close();
            return allocateClassRoomViews;
        }

        public int Save(AllocateClassRoom allocateClassRoom)
        {
            allocateClassRoom.Action = "Allocated";
            string query =
                "INSERT INTO AllocateClassroom(DepartmentId, CourseId, RoomId, DayId, FromTime, ToTime, Action) " +
                "VALUES('" + allocateClassRoom.DepartmentId + "','" + allocateClassRoom.CourseId + "','" + allocateClassRoom.RoomId + "','" + allocateClassRoom.DayId + "','" + allocateClassRoom.FromTime + "','" + allocateClassRoom.ToTime + "','" + allocateClassRoom.Action + "')";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<AllocateClassRoom> GetAllocateInfoByDayAndRoomId(int dayId, int roomId)
        {
            string query = "SELECT * FROM AllocateClassroom WHERE RoomId='" + roomId + "' AND DayId = '" + dayId + "' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            List<AllocateClassRoom> allocateClassRooms = new List<AllocateClassRoom>();
            Reader = Command.ExecuteReader();
            bool isExistRow = Reader.HasRows;
            if (isExistRow)
            {
                while (Reader.Read())
                {
                    AllocateClassRoom allocateClassRoom = new AllocateClassRoom();
                    allocateClassRoom.FromTime = Reader["FromTime"].ToString();
                    allocateClassRoom.ToTime = Reader["ToTime"].ToString();
                    allocateClassRooms.Add(allocateClassRoom);
                }
            }
            else
            {
                allocateClassRooms = null;
            }
          
            Reader.Close();
            Connection.Close();
            return allocateClassRooms;
        }
        public int UnAllocateClassRoom() 
        {
            string action = "Unallocated";
            string query =
                "UPDATE AllocateClassroom SET Action = '"+action+"' ";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}