namespace StudentAttendanceTracker.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using StudentAttendanceTracker.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentAttendanceTracker.Models.StudentAttendanceTrackerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StudentAttendanceTracker.Models.StudentAttendanceTrackerContext";
        }

        protected override void Seed(StudentAttendanceTracker.Models.StudentAttendanceTrackerContext context)
        {
            // Add a user
            UserManager<AppUser> userManager = new UserManager<AppUser>(
              new UserStore<AppUser>(context));

            AppUser user = context.Users.Where(u => u.UserName == "b@test.com").FirstOrDefault();
            if (user == null)
            {
                user = new AppUser { UserName = "b@test.com", FullName = "Brady Hyde" };
                userManager.Create(user, "123456");
            }
            else
                userManager.AddPassword(user.Id, "123456");

            // Add a role
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Admin" }, new IdentityRole() { Name = "User" });
            context.SaveChanges();

            // Add role to user
            userManager.AddToRole(user.Id, "Admin");

            // create students
            Student molly = new Student { FirstName = "Molly", LastName = "Hyde", StudentNumber = "987543" };
            Student brady = new Student { FirstName = "Brady", LastName = "Hyde", StudentNumber = "987544" };
            Student charlie = new Student { FirstName = "Charlie", LastName = "TheDog", StudentNumber = "987545" };
            Student juno = new Student { FirstName = "Juno", LastName = "TheDog", StudentNumber = "987546" };
            Student nugget = new Student { FirstName = "Nugget", LastName = "TheCat", StudentNumber = "987547" };
            Student evelynn = new Student { FirstName = "Evelynn", LastName = "Hyde", StudentNumber = "987548" };

            // create courses
            Course martialArts = new Course { CourseName = "Martial Arts", Location = "South Gym", Instructor = "Opal Burdge" };
            Course cooking = new Course { CourseName = "Cooking", Location = "Home Ec Room", Instructor = "Sherry Hyde" };

            martialArts.Students.Add(molly);
            martialArts.Students.Add(brady);
            martialArts.Students.Add(charlie);

            cooking.Students.Add(juno);
            cooking.Students.Add(nugget);
            cooking.Students.Add(evelynn);

            context.Students.AddOrUpdate(s => s.StudentNumber, molly, brady, charlie, juno, nugget, evelynn);
            context.Courses.AddOrUpdate(c => c.CourseName, martialArts, cooking);

        }
    }
}
