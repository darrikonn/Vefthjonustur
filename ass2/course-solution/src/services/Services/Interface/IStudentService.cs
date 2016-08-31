namespace Services.Services.Interface {
    using System.Collections.Generic;
    using Models.EntityModels;

    public interface IStudentService {
        List<Student> GetStudentsOfCourse(int id);
        List<Student> AddStudentToCourse(int id, string ssn);
    }
}
