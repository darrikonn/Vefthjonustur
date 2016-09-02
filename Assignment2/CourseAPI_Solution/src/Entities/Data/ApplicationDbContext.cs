namespace CourseAPI.Entities.Data {
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using CourseAPI.Entities.Models;

    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) {}

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseTemplate> CourseTemplates { get; set; }
        public DbSet<CourseStudentLinker> CourseStudentLinkers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            
            // using fluent API for double key attributes in a single table
            builder.Entity<CourseStudentLinker>()
                .HasKey(c => new {c.Id, c.SSN});
        }
    }
}
