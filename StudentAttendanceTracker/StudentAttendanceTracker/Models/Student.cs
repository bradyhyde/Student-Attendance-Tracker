using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentAttendanceTracker.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; }
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name="Student Name")]
        public string DisplayName { get { return FirstName + " " + LastName; } }
    }
}