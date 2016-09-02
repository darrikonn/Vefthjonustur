using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CourseAPI.Services.Data;

namespace Services.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160902112815_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("CourseAPI.Entities.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseId");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Semester");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.CourseStudentLinker", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("SSN");

                    b.Property<int?>("CourseId");

                    b.HasKey("Id", "SSN");

                    b.HasIndex("CourseId");

                    b.HasIndex("SSN");

                    b.ToTable("CourseStudentLinkers");
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.CourseTemplate", b =>
                {
                    b.Property<string>("CourseId");

                    b.Property<string>("Name");

                    b.HasKey("CourseId");

                    b.ToTable("CourseTemplates");
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.Student", b =>
                {
                    b.Property<string>("SSN");

                    b.Property<string>("Name");

                    b.HasKey("SSN");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.CourseStudentLinker", b =>
                {
                    b.HasOne("CourseAPI.Entities.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.HasOne("CourseAPI.Entities.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("SSN")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
