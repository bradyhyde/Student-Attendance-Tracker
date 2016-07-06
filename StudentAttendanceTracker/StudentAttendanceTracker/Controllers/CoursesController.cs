using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentAttendanceTracker.Models;
using Microsoft.AspNet.Identity;

namespace StudentAttendanceTracker.Controllers
{
    public class CoursesController : Controller
    {
        private StudentAttendanceTrackerContext db = new StudentAttendanceTrackerContext();

        // GET: /Courses/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        [Authorize]
        // GET: /Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseViewModel course = GetStudents(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [Authorize]
        // GET: /Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CourseID,CourseName,Location,Instructor,InstructorSelect")] CourseViewModel courseVM, string instructorSelect)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course() { 
                    CourseName = courseVM.CourseName,
                    Location = courseVM.Location,
                    Instructor = instructorSelect
                }; 

                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseVM);
        }

        [Authorize]
        // GET: /Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseViewModel courseVM = GetStudents(id);
            if (courseVM == null)
            {
                return HttpNotFound();
            }
            return View(courseVM);
        }

        // POST: /Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CourseID,CourseName,Location,Instructor,InstructorSelect")] CourseViewModel courseVM, string instructorSelect)
        {
            if (ModelState.IsValid)
            {   
                Course course = (from c in db.Courses
                                 where c.CourseID == courseVM.CourseID
                                 select c).FirstOrDefault();

                //set changes from MessVM
                course.CourseName = courseVM.CourseName;
                course.Location = course.Location;
                course.Instructor = courseVM.Instructor;

                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseVM);
        }

        private SelectList CreateStudentDropDown()
        {
            return new SelectList(db.Students.OrderBy(i => i.LastName), "StudentID", "DisplayName");
        }

        [Authorize]
        public ActionResult AddStudents(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseViewModel courseVM = GetStudents(id);
            if (courseVM == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentSelect = CreateStudentDropDown();
            return View(courseVM);
        }

        // POST: /Courses/AddStudents
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudents([Bind(Include = "CourseID,StudentSelect,Students")] CourseViewModel courseVM, int studentSelect)
        {
            if (ModelState.IsValid)
            {
                Course course = (from c in db.Courses.Include("Students")
                                 where c.CourseID == courseVM.CourseID
                                 select c).FirstOrDefault();

                Student student = (from s in db.Students
                                   where s.StudentID == studentSelect
                                   select s).FirstOrDefault();

                course.Students.Add(student);
                courseVM.Students = course.Students.OrderBy(s => s.LastName).ToList();
                
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details/" + courseVM.CourseID);
            }
            return View(courseVM);
        }

        [Authorize]
        // GET: /Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: /Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private CourseViewModel GetStudents(int? courseId)
        {
            CourseViewModel courseVM = new CourseViewModel();
            
            courseVM = (from course in db.Courses.Include("Students")
                        where course.CourseID == courseId
                        select new CourseViewModel
                        {
                            CourseID = course.CourseID,
                            CourseName = course.CourseName,
                            Location = course.Location,
                            Students = course.Students.OrderBy(s => s.LastName).ToList(),
                            Instructor = course.Instructor
                        }).FirstOrDefault();
            
            return courseVM;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
