namespace WebApplication.Helpers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using WebApplication.Models.EntityModels;
    using WebApplication.Data;

    public class InitializeDatabase : CreateDatabaseIfNotExists<ApplicationDbContext> {
        // DropDatabaseAlways<ApplicationDbContext> {
        public override void Seed(ApplicationDbContext db) {
            var courses = new List<Course> {
                new Course {
                    Id = 1,
                    CourseId = "T-514-VEFT",
                    Semester = 20153
                },
                new Course {
                    Id = 2,
                    CourseId = "T-514-VEFT",
                    Semester = 20163
                },
                new Course {
                    Id = 3,
                    CourseId = "T-111-PROG",
                    Semester = 20153
                }
            };
            
            var students = new List<Student> {
                new Student {
                    SSN = "1234567890",
                    Name = "Jón Jónsson"
                },
                new Student {
                    SSN = "9876543210",
                    Name = "Guðrún Jónsdóttir"
                },
                new Student {
                    SSN = "6543219870",
                    Name = "Gunnar Sigurðsson"
                },
                new Student {
                    SSN = "4567891230",
                    Name = "Jóna Halldórsdóttir"
                }
            };

            // add to database
            courses.ForEach(c => db.Courses.Add(c));
            students.ForEach(s => db.Students.Add(s));
            db.SaveChanges();
        }
    }
}
