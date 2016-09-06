namespace CourseAPI.Services.Exceptions {
    using System;

    /// <summary>
    /// This class represents a custom exception when there is a conflict.
    /// </summary>
    public class CustomConflictException : Exception {
        public CustomConflictException() {}
        public CustomConflictException(string message) : base(message) {}
    }
}
