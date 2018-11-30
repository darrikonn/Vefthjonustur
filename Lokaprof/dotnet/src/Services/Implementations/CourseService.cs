namespace WebApplication.Services.Implemenations {
  using System.Linq;
  using System.Collections.Generic;
  using WebApplication.Entities.Data;
  using WebApplication.Services.Interfaces;
  using WebApplication.Entities.Models;
  using WebApplication.Models.DTOs;
  using WebApplication.Models.ViewModels;

  public class CourseService : ICourseService {
    private readonly ApplicationDbContext _db;
    public CourseService(ApplicationDbContext db) {
      _db = db;
    }


    public List<CourseInstance> getCourses() {
      var courseInstances = new List<CourseInstance> {
        new CourseInstance {
          ID = 1,
          Name = "Vefthjonustur",
          Semester = "20153",
          TeacherSSN = "1234567890"
        },
        new CourseInstance {
          ID = 2,
          Name = "Prump",
          Semester = "20153",
          TeacherSSN = "1234567891"
        }
      };
      var persons = new List<Person> {
        new Person {
          SSN = "1234567890",
          Name = "Daniel Brandur"
        },
        new Person {
          SSN = "1234567891"
        }
      };
      var studentRegistrations = new List<StudentRegistration> {
        new StudentRegistration {
          SSN = "1234567890",
          CourseInstanceId = 1,
          Status = 1
        }
      };
      return (from c in courseInstances
              select c).Skip(10 * 0).Take(10).ToList();
      /*return (from c in courseInstances.Where(i => i.ID == 2)
              join p in persons on c.TeacherSSN equals p.SSN into k
              from kk in k
              select kk.Name).SingleOrDefault();*/
      /*return (from r in studentRegistrations.Where(s => s.CourseInstanceId == 1)
              join p in persons on r.SSN equals p.SSN
              where r.Status == 1
              select p).ToList();
      /*return (from c in _db.Courses
              select new CourseDTO {Name=c.Name}).ToList();*/
    }

    public CourseDTO getCourse(int id) {
      var course = (from c in _db.Courses
              where c.Id == id
              select c).SingleOrDefault();
      return new CourseDTO {
        Name = course.Name,
        TeacherId = course.TeacherId
      };
    }

    public int addCourse(CourseViewModel model) {
      var teacher = new Teacher {
        Name = model.Teacher
      };
      _db.Teachers.Add(teacher);
      var course = new Course {
        Name = model.Course,
        TeacherId = teacher.Id
      };
      _db.Courses.Add(course);
      _db.SaveChanges();
      
      return course.Id;
    }

    public TeacherDTO getTeacher(int id) {
      var course = getCourse(id);
      if (course != null) {
        var teacher = (from t in _db.Teachers
                where t.Id == course.TeacherId
                select t).SingleOrDefault();
        return new TeacherDTO {
          Name = teacher.Name
        };
      }
      return null;
    }
  }
}
