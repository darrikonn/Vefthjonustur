using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApplication.Data;

namespace Assignment2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160830142034_Assignment2Database")]
    partial class Assignment2Database
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("WebApplication.Models.EntityModels.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseId");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfStudents");

                    b.Property<int>("Semester");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("WebApplication.Models.EntityModels.Student", b =>
                {
                    b.Property<string>("SSN");

                    b.Property<string>("Name");

                    b.HasKey("SSN");

                    b.ToTable("Students");
                });
        }
    }
}
