using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CourseAPI.Entities.Data;

namespace Entities.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("CourseAPI.Entities.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("MaxStudents");

                    b.Property<string>("Semester");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.CourseStudentLinker", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("SSN");

                    b.Property<bool>("IsActive");

                    b.HasKey("Id", "SSN");

                    b.HasIndex("Id");

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

            modelBuilder.Entity("CourseAPI.Entities.Models.WaitingListLinker", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("SSN");

                    b.HasKey("Id", "SSN");

                    b.HasIndex("Id");

                    b.HasIndex("SSN");

                    b.ToTable("WaitingListLinkers");
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.Course", b =>
                {
                    b.HasOne("CourseAPI.Entities.Models.CourseTemplate", "CourseTemplate")
                        .WithMany()
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.CourseStudentLinker", b =>
                {
                    b.HasOne("CourseAPI.Entities.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseAPI.Entities.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("SSN")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CourseAPI.Entities.Models.WaitingListLinker", b =>
                {
                    b.HasOne("CourseAPI.Entities.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CourseAPI.Entities.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("SSN")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
