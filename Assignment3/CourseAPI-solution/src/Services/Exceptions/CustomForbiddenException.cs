namespace CourseAPI.Services.Exceptions {
    using System;

    /// <summary>
    /// This class represents a custom exception when an action is forbidden.
    /// </summary>
    public class CustomForbiddenException : Exception {
        public CustomForbiddenException() {}
        public CustomForbiddenException(string message) : base(message) {}
    }
}
