namespace CoursesAPI.Models {
    using System.Collections.Generic;

    public class CourseEnvelopeDTO {
        public List<CourseInstanceDTO> Items { get; set; }
        public PagingDTO Paging { get; set; }
    }
}
