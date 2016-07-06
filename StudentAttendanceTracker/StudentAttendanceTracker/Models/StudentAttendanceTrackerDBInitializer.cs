using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentAttendanceTracker.Models
{
    public class StudentAttendanceTrackerDBInitializer : DropCreateDatabaseIfModelChanges<StudentAttendanceTrackerContext>
    //public class StudentAttendanceTrackerDBInitializer : DropCreateDatabaseAlways<StudentAttendanceTrackerContext>
    {
        protected override void Seed(StudentAttendanceTrackerContext context)
        {
            Student molly = new Student { FirstName = "Molly", LastName = "Hyde", StudentNumber = "987543" };
            Student brady = new Student { FirstName = "Brady", LastName = "Hyde", StudentNumber = "987544" };
            Student charlie = new Student { FirstName = "Charlie", LastName = "TheDog", StudentNumber = "987545" };
            Student juno = new Student { FirstName = "Juno", LastName = "TheDog", StudentNumber = "987546" };
            Student nugget = new Student { FirstName = "Nugget", LastName = "TheCat", StudentNumber = "987547" };
            Student evelynn = new Student { FirstName = "Evelynn", LastName = "Hyde", StudentNumber = "987548" };

            Course martialArts = new Course { CourseName = "Martial Arts", Location = "South Gym", Instructor = "Opal Burdge" };
            Course cooking = new Course { CourseName = "Cooking", Location = "Home Ec Room", Instructor = "Sherry Hyde" };

            martialArts.Students.Add(molly);
            martialArts.Students.Add(brady);
            martialArts.Students.Add(charlie);

            cooking.Students.Add(juno);
            cooking.Students.Add(nugget);
            cooking.Students.Add(evelynn);

            context.Courses.Add(cooking);
            context.Courses.Add(martialArts);

            // Add a user
            UserManager<AppUser> userManager = new UserManager<AppUser>(
              new UserStore<AppUser>(context));

            var user = new AppUser { UserName = "b@m.edu", FullName = "Brady Hyde" };
            var result = userManager.Create(user, "123456");

            // Add a role
            context.Roles.Add(new IdentityRole() { Name = "Admin" });
            context.SaveChanges();

            // Add role to user
            userManager.AddToRole(user.Id, "Admin");

            base.Seed(context);
        }
    }
}