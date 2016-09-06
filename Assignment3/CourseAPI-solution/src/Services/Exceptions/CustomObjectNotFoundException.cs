namespace CourseAPI.Services.Exceptions {
    using System;

    /// <summary>
    /// This class represents a custom exception when an object was not found.
    /// </summary>
    public class CustomObjectNotFoundException : Exception {
        public CustomObjectNotFoundException() {}
        public CustomObjectNotFoundException(string message) : base(message) {}
    }
}
