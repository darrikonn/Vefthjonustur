namespace CourseAPI.Services.Exceptions {
    using System;

    public class CustomForbiddenException : Exception {
        public CustomForbiddenException() {}
        public CustomForbiddenException(string message) : base(message) {}
    }
}
