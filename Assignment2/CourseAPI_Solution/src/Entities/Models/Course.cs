namespace CourseAPI.Entities.Models {
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// This class represents the entity class for Course.
    /// This class gets mapped to a table in the database. 
    /// </summary>
    public class Course {
        /// <summary>
        /// The id of a course. It's the key of the table.
        /// Example:
        ///     1) 1
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The string id of a course.
        /// Example: 
        ///     1) T-514-VEFT
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// The semester when this course is tought. 
        /// {year}{semester}
        /// 1: spring
        /// 2: summer
        /// 3: fall
        /// Example:
        ///     1) "20153"    ->    fall 2015
        ///     2) "20142"    ->    summer 2014
        /// </summary>
        public string Semester { get; set; }

        /// <summary>
        /// The date when the course started. 
        /// The data type is DateTime.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The date when the course ended. 
        /// The data type is DateTime.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
