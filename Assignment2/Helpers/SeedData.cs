namespace WebApplication.Helpers {
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using WebApplication.Models.EntityModels;
    using WebApplication.Data;
    using System;
    using System.Linq;

    public static class SeedData {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var db = new ApplicationDbContext (
                    serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>())) {

                // check if courses have been seeded
                if (!db.Courses.Any()) {
                    db.Courses.AddRange(   
                        new Course {
                            Id = 1,
                            CourseId = "T-514-VEFT",
                            Semester = 20153
                        },
                        new Course {
                            Id = 2,
                            CourseId = "T-514-VEFT",
                            Semester = 20163
                        },
                        new Course {
                            Id = 3,
                            CourseId = "T-111-PROG",
                            Semester = 20153
                        }
                    );
                }

                // check for template seeds
                if (!db.CourseTemplates.Any()) {
                    db.CourseTemplates.AddRange(
                        new CourseTemplate {
                            CourseId = "T-514-VEFT",
                            Name = "Web services"
                        },
                        new CourseTemplate {
                            CourseId = "T-111-PROG",
                            Name = "Programming"
                        }
                    );
                }

                // check if students have been seeded
                if (!db.Students.Any()) {
                    db.Students.AddRange(
                        new Student {
                            SSN = "1234567890",
                            Name = "Jón Jónsson"
                        },
                        new Student {
                            SSN = "9876543210",
                            Name = "Guðrún Jónsdóttir"
                        },
                        new Student {
                            SSN = "6543219870",
                            Name = "Gunnar Sigurðsson"
                            },
                        new Student {
                            SSN = "4567891230",
                            Name = "Jóna Halldórsdóttir"
                        }
                    );
                }

                // check if there are any existing links
                if (!db.CourseStudentLinkers.Any()) {
                    db.CourseStudentLinkers.AddRange(
                        new CourseStudentLinker {
                            Id = 1,
                            SSN = "1234567890"
                        },
                        new CourseStudentLinker {
                            Id = 2,
                            SSN = "1234567890"
                        },
                        new CourseStudentLinker {
                            Id = 3,
                            SSN = "1234567890"
                        },
                        new CourseStudentLinker {
                            Id = 1,
                            SSN = "9876543210"
                        },
                        new CourseStudentLinker {
                            Id = 2,
                            SSN = "9876543210"
                        },
                        new CourseStudentLinker {
                            Id = 1,
                            SSN = "6543219870"
                        },
                        new CourseStudentLinker {
                            Id = 2,
                            SSN = "6543219870"
                        },
                        new CourseStudentLinker {
                            Id = 3,
                            SSN = "6543219870"
                        },
                        new CourseStudentLinker {
                            Id = 3,
                            SSN = "4567891230"
                        }
                    );
                }

                db.SaveChanges();
            }
        }
    }
}
