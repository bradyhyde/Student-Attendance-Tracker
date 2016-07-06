using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentAttendanceTracker.Models
{
    public class StudentAttendanceTrackerContext : IdentityDbContext<AppUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public StudentAttendanceTrackerContext() : base("name=StudentAttendanceTrackerContext")
        {
            Database.SetInitializer<StudentAttendanceTrackerContext>(new MigrateDatabaseToLatestVersion<StudentAttendanceTrackerContext, StudentAttendanceTracker.Migrations.Configuration>("StudentAttendanceTrackerContext"));
        }

        public DbSet<StudentAttendanceTracker.Models.Course> Courses { get; set; }

        public DbSet<StudentAttendanceTracker.Models.Student> Students { get; set; }

    
    }
}
