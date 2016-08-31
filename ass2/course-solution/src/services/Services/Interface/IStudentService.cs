namespace Services.Services.Interface {
    using System.Collections.Generic;
    using Models.Models.ViewModels;
    using Models.Models.DTO;

    public interface IStudentService {
        List<StudentDTO> GetStudentsOfCourse(int id); 
        List<StudentDTO> AddStudentToCourse(int id, LinkerViewModel model); 
    }
}
