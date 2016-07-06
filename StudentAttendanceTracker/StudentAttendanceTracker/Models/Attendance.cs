using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAttendanceTracker.Models
{
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public int StudentID { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; } // will be true if the student is there on that day and false if absent
    }
}