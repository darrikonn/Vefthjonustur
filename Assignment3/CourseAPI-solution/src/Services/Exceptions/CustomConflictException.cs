namespace CourseAPI.Services.Exceptions {
    using System;

    public class CustomConflictException : Exception {
        public CustomConflictException() {}
        public CustomConflictException(string message) : base(message) {}
    }
}
