using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseTemplates",
                columns: table => new
                {
                    TemplateId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTemplates", x => x.TemplateId);
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
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MaxStudents = table.Column<int>(nullable: false),
                    Semester = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TemplateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "CourseTemplates",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseStudentLinkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SSN = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudentLinkers", x => new { x.Id, x.SSN });
                    table.ForeignKey(
                        name: "FK_CourseStudentLinkers_Courses_Id",
                        column: x => x.Id,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudentLinkers_Students_SSN",
                        column: x => x.SSN,
                        principalTable: "Students",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaitingListLinkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    SSN = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitingListLinkers", x => new { x.Id, x.SSN });
                    table.ForeignKey(
                        name: "FK_WaitingListLinkers_Courses_Id",
                        column: x => x.Id,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaitingListLinkers_Students_SSN",
                        column: x => x.SSN,
                        principalTable: "Students",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TemplateId",
                table: "Courses",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentLinkers_Id",
                table: "CourseStudentLinkers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudentLinkers_SSN",
                table: "CourseStudentLinkers",
                column: "SSN");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingListLinkers_Id",
                table: "WaitingListLinkers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingListLinkers_SSN",
                table: "WaitingListLinkers",
                column: "SSN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseStudentLinkers");

            migrationBuilder.DropTable(
                name: "WaitingListLinkers");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CourseTemplates");
        }
    }
}
