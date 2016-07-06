using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAttendanceTracker.Models
{
    public class CourseViewModel
    {
        List<Student> students = new List<Student>();

        public int CourseID { get; set; }
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        public string Location { get; set; }
        [Display(Name="Instructor Name")]
        public string Instructor { get; set; }
        public List<Student> Students { get { return students; } set { students = value; } }
    }
}