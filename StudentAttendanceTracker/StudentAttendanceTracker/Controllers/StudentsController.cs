using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentAttendanceTracker.Models;

namespace StudentAttendanceTracker.Controllers
{
    public class StudentsController : Controller
    {
        private StudentAttendanceTrackerContext db = new StudentAttendanceTrackerContext();

        // GET: /Students/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: /Students/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Student student = db.Students.Find(id);
            StudentViewModel student = GetCourses(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: /Students/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StudentID,StudentNumber,FirstName,LastName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: /Students/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StudentID,StudentNumber,FirstName,LastName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: /Students/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Students/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        private StudentViewModel GetCourses(int? studentId)
        {
            
            List<Course> courses = (from course in db.Courses.Include("Students")
                                    select course).ToList<Course>();

            List<Course> matchingCourses = new List<Course>();
            
            foreach (Course c in courses) 
            {
                foreach (Student s in c.Students)
                {
                    if (s.StudentID == studentId)
                        matchingCourses.Add(c);
                }
            }
            
            StudentViewModel studentVM = new StudentViewModel();
            
            studentVM = (from s in db.Students
                        where s.StudentID == studentId
                        select new StudentViewModel
                        {
                            StudentID = s.StudentID,
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            StudentNumber = s.StudentNumber
                        }).FirstOrDefault();
            
            foreach (Course c in matchingCourses) {
                studentVM.StudentCourses.Add(c);
            }
                                
            return studentVM;
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
