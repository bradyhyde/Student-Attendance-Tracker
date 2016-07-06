using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAttendanceTracker.Models
{
    public class StudentViewModel
    {
        private List<Course> studentCourses = new List<Course>();

        public int StudentID { get; set; }
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Student Courses")]
        public List<Course> StudentCourses { get { return studentCourses; } set { studentCourses = value; } }
    }
}