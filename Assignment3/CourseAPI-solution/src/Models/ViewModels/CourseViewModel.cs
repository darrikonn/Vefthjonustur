namespace CourseAPI.Models.ViewModels {
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This class represents the view model, or the data, of courses. 
    /// This gets passed to the controller in the Data of an API call.
    /// </summary>
    public class CourseViewModel {
        /// <summary>
        /// The string id of a course.
        /// Required when updating/creating a course.
        /// Example: 
        ///     1) "T-514-VEFT"
        /// </summary>
        [Required]
        public string TemplateId { get; set; }

        /// <summary>
        /// The semester when this course is tought. 
        /// Required when updating/creating a course.
        /// {year:int}{semester:int}
        /// 1: spring
        /// 2: summer
        /// 3: fall
        /// Example:
        ///     1) "20153"    ->    fall 2015
        ///     2) "20142"    ->    summer 2014
        /// </summary>
        [Required]
        public string Semester { get; set; }

        /// <summary>
        /// The maximum number of students in this course
        /// Example:
        ///     1) 10
        /// </summary>
        [Required]
        public int MaxStudents { get; set; }

        /// <summary>
        /// The date when the course started. 
        /// The data type is DateTime.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The date when the course ended.
        /// The data type is DateTime.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
    }
}
