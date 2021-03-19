using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SwapnolokeTest.Models;

namespace SwapnolokeTest.Controllers
{
    public class StudentsController : Controller
    {
        private StudentDBContext db = new StudentDBContext();
        private DatabaseManager databaseManager = DatabaseManager.getInstance();

        private List<Student> GetStudents()
        {
            List<Student> studentList = new List<Student>();
            String query = "SELECT * FROM Students";
            databaseManager.command.CommandText = query;
            databaseManager.connection.Open();

            SqlDataReader reader = databaseManager.command.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student()
                {
                    studentId = reader["studentId"].ToString(),
                    studentName = reader["studentName"].ToString(),
                    studentEmail = reader["studentEmail"].ToString(),
                    studentContactNo = reader["studentContactNo"].ToString(),
                    enrolledDate = (DateTime)reader["enrolledDate"],
                    studentAddress = reader["studentAddress"].ToString(),
                    studentDeptName = reader["studentDeptName"].ToString()
                };
                studentList.Add(student);
            }
            databaseManager.connection.Close();
            return studentList;
        }


        // GET: Students
        public ActionResult Index()
        {
            //return View(db.Movies.ToList());
            return View(GetStudents());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Movies.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,studentId,studentName,studentEmail,studentContactNo,enrolledDate,studentAddress,studentDeptName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Movies.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,studentId,studentName,studentEmail,studentContactNo,enrolledDate,studentAddress,studentDeptName")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Movies.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Movies.Find(id);
            db.Movies.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
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
