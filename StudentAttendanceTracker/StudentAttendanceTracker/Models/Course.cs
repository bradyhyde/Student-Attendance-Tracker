using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAttendanceTracker.Models
{
    public class Course
    {
        private List<Student> students = new List<Student>();

        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Location { get; set; }
        public string Instructor { get; set; }
        public List<Student> Students { get { return students; } }
    }
}