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
            databaseManager.OpenConnection();

            SqlDataReader reader = databaseManager.command.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student()
                {
                    ID = (int)reader["ID"],
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
            databaseManager.CloseConnection();
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
/*            if (ModelState.IsValid)
            {
                db.Movies.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            if (student != null)
            {
                String query = "INSERT INTO Students VALUES("
                    + "'" + student.studentId + "',"
                    + "'" + student.studentName + "',"
                    + "'" + student.studentEmail + "',"
                    + "'" + student.studentContactNo + "',"
                    + "'" + student.enrolledDate.ToString() + "',"
                    + "'" + student.studentAddress + "',"
                    + "'" + student.studentDeptName + "')";

                databaseManager.command.CommandText = query;
                databaseManager.OpenConnection();
                int rowAffected = databaseManager.command.ExecuteNonQuery();
                databaseManager.CloseConnection();
                if (rowAffected != 0)
                {
                    return RedirectToAction("Index");
                }
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
            //Student student = db.Movies.Find(id);
            Student student = GetStudent(id);
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
            /*            if (ModelState.IsValid)
                        {
                            db.Entry(student).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }*/
            if (student != null)
            {
                String query = "UPDATE Students SET"
                        + " studentId = '" + student.studentId + "',"
                        + " studentName = '" + student.studentName + "',"
                        + " studentEmail = '" + student.studentEmail + "',"
                        + " studentContactNo = '" + student.studentContactNo + "',"
                        + " enrolledDate = '" + student.enrolledDate.ToString() + "',"
                        + " studentAddress = '" + student.studentAddress + "',"
                        + " studentDeptName = '" + student.studentDeptName + "'"
                        + "WHERE ID = " + student.ID;

                databaseManager.command.CommandText = query;
                databaseManager.OpenConnection();
                databaseManager.command.ExecuteNonQuery();
                databaseManager.CloseConnection();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public Student GetStudent(int? id)
        {
            String query = "SELECT * FROM Students WHERE ID = " + id;
            databaseManager.command.CommandText = query;
            databaseManager.OpenConnection();
            SqlDataReader reader = databaseManager.command.ExecuteReader();

            Student student = null;
            while (reader.Read())
            {
                student = new Student()
                {
                    ID = (int)reader["ID"],
                    studentId = reader["studentId"].ToString(),
                    studentName = reader["studentName"].ToString(),
                    studentEmail = reader["studentEmail"].ToString(),
                    studentContactNo = reader["studentContactNo"].ToString(),
                    enrolledDate = (DateTime)reader["enrolledDate"],
                    studentAddress = reader["studentAddress"].ToString(),
                    studentDeptName = reader["studentDeptName"].ToString()
                };
            }
            databaseManager.CloseConnection();
            return student;
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Student student = db.Movies.Find(id);
            Student student = GetStudent(id);
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
            /*            Student student = db.Movies.Find(id);
                        db.Movies.Remove(student);
                        db.SaveChanges();*/

            String query = "DELETE FROM Students WHERE ID = " + id;
            databaseManager.command.CommandText = query;
            databaseManager.OpenConnection();
            databaseManager.command.ExecuteNonQuery();
            databaseManager.CloseConnection();

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
