using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment2.Migrations
{
    public partial class ass2database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    CourseId = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseTemplates",
                columns: table => new
                {
                    CourseId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTemplates", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    SSN = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.SSN);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudentLinkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SSN = table.Column<string>(nullable: false),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudentLinkers", x => new { x.Id, x.SSN });
                    table.ForeignKey(
                        name: "FK_CourseStudentLinkers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseStudentLinkers_Students_SSN",
                        column: x => x.SSN,
                        principalTable: "Students",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentLinkers_CourseId",
                table: "CourseStudentLinkers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentLinkers_SSN",
                table: "CourseStudentLinkers",
                column: "SSN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudentLinkers");

            migrationBuilder.DropTable(
                name: "CourseTemplates");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
