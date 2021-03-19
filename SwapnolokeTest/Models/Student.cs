using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace SwapnolokeTest.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public String studentId { get; set; }
        public String studentName { get; set; }
        public String studentEmail { get; set; }
        public String studentContactNo { get; set; }
        public DateTime enrolledDate { get; set; }
        public String studentAddress { get; set; }
        public String studentDeptName { get; set; }
    }

    public class StudentDBContext : DbContext
    {
        public DbSet<Student> Movies { get; set; }
    }
}