namespace CourseAPI.API.Helpers {
    /// <summary>
    /// A static class that contains all constant variables 
    /// that can be shared across the application.
    /// </summary>
    public static class ConstantVariables {
        /// <summary>
        /// Indicates the current semester, which is currently fall 2016.
        /// {year:int}{semester:int}
        /// 1: spring
        /// 2: summer
        /// 3: fall
        /// Example:
        ///     1) "20153"    ->    fall 2015
        ///     2) "20142"    ->    summer 2014
        /// </summary>
        public static string CurrentSemester = "20163";
    }
}
