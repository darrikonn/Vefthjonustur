namespace CourseAPI.Services.Exceptions {
    using System;

    public class CustomObjectNotFoundException : Exception {
        public CustomObjectNotFoundException() {}
        public CustomObjectNotFoundException(string message) : base(message) {}
    }
}
