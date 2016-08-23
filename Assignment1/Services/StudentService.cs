namespace WebApplication.Services {
    using System;
    using System.Collections.Generic;
    using WebApplication.Models.EntityModels;
    using System.Linq;

    public class StudentService {
        private static Random _rnd = new Random();
        private readonly string _alphas = "ABCDEFGHIJKL MNOPQRSTUVWXYZ abcdefghijkl mnopqrstuvwxyz";
        private readonly string _numbers = "1234567890";

        public List<Student> CreateStudents(int cap) {
            var students = new List<Student>();

            for (var i = 0; i < cap; i++) {
                students.Add(new Student {
                    Name = new string(Enumerable.Repeat(_alphas, _rnd.Next(10, 20))
                            .Select(s => s[_rnd.Next(s.Length)]).ToArray()),
                    SSN = new string(Enumerable.Repeat(_numbers, 10)
                            .Select(n => n[_rnd.Next(n.Length)]).ToArray())
                });
            }

            return students;
        }
    }
}
