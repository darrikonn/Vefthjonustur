namespace CourseAPI.Services.Services.Interface {
    using System.Collections.Generic;
    using CourseAPI.Models.ViewModels;
    using CourseAPI.Models.DTO;

    /// <summary>
    /// This is an interface for the course service.
    /// The service "fetches" the requested data from the database.
    /// </summary>
    public interface ICourseService {
        /// <summary>
        /// Get all courses of a semester by passing the semester as a parameter.
        /// Example:
        ///     1) GetCoursesOfSemester(20153);
        /// </summary>
        /// <returns>
        /// Returns a list of CourseDTOs
        /// </returns>
        List<CourseDTO> GetCoursesOfSemester(int semester);
        
        /// <summary>
        /// Get a course by id by passing the id as a paramter.
        /// Example:
        ///     1) GetCourseById(1);
        /// </summary>
        /// <returns>
        /// Returns CourseDetailsDTO
        /// </returns>
        CourseDetailsDTO GetCourseById(int id);

        /// <summary>
        /// Update a course by passing the id of the required course as 
        /// the model required to update
        /// Example:
        ///     1) UpdateCourseById(1, model);
        /// </summary>
        /// <returns>
        /// Returns CourseDetailsDTO
        /// </returns>
        CourseDetailsDTO UpdateCourseById(int id, CourseViewModel model);

        /// <summary>
        /// Delete a course by passing its id as a parameter.
        /// Example:
        ///     1) DeleteCourseById(1);
        /// </summary>
        /// <returns>
        /// Returns a boolean if the deletion was a success or not.
        /// </returns>
        bool DeleteCourseById(int id);

        /// <summary>
        /// Add a course to the courses by passing the the CourseViewModel as a parameter.
        /// Example:
        ///     1) AddCourse(model);
        /// </summary>
        /// <returns>
        /// Returns the id of the newly created course if successful, else -1.
        /// </returns>
        int AddCourse(CourseViewModel model);
    }
}
