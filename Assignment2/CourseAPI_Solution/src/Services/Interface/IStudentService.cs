namespace CourseAPI.Services.Interface {
    using System.Collections.Generic;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTO;

    /// <summary>
    /// This is an interface for the student service.
    /// The service "fetches" the requested data from the database.
    /// </summary>
    public interface IStudentService {
        /// <summary>
        /// Get all students of a course by passing the required course id as a parameter.
        /// Example:
        ///     1) GetStudentsOfCourse(1);
        /// </summary>
        /// <returns>
        /// A list of StudentDTO.
        /// </returns>
        List<StudentDTO> GetStudentsOfCourse(int id); 

        /// <summary>
        /// Add a student to a course by passing the course id and 
        /// the LinkerViewModel as a parameter
        /// Example:
        ///     1) AddStudentToCourse(1, model);
        /// </summary>
        /// <returns>
        /// A list of StudentDTO.
        /// </returns>
        List<StudentDTO> AddStudentToCourse(int id, LinkerViewModel model); 
    }
}
