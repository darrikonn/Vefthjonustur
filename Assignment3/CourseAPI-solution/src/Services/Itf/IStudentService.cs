namespace CourseAPI.Services.Itf {
    using System.Collections.Generic;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTOModels;

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
        /// StudentDTO instance.
        /// </returns>
        StudentDTO AddStudentToCourse(int id, LinkerViewModel model, int capacity); 

        /// <summary>
        /// Get all students of a courses waiting list by passing the required course id as a parameter.
        /// Example:
        ///     1) GetStudentsOfCourseWaitingList(1);
        /// </summary>
        /// <returns>
        /// A list of StudentDTO.
        /// </returns>
        List<StudentDTO> GetStudentsOfCourseWaitingList(int id);

        /// <summary>
        /// Add a student to a course waiting list by passing the course id and 
        /// the LinkerViewModel as a parameter
        /// Example:
        ///     1) AddStudentToCourseWaitingList(1, model);
        /// </summary>
        /// <returns>
        /// StudentDTO instance.
        /// </returns>
        StudentDTO AddStudentToCourseWaitingList(int id, LinkerViewModel model);

        /// <summary>
        /// Delete a student from the course by passing the required course id and the
        /// LinkerViewModel as a parameter.
        /// Example:
        ///     1) DeleteStudentFromCourse(1, model);
        /// </summary>
        /// <returns>
        /// Returns a boolean if the deletion was a success or not.
        /// </returns>
        bool DeleteStudentFromCourse(int id, LinkerViewModel model);
    }
}
