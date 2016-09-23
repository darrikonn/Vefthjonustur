namespace CoursesAPI.Services.Utilities {
    using System.Collections.Generic;

    public class PageInfo {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalNumberOfItems { get; set; }
    }

    public class PageResult<T> {
        public List<T> Items { get; set; }
        public PageInfo Paging { get; set; }
    }
}
