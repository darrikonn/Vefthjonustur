# Assignment 1
The first assignment is to design and create a REST service using .NET Core and ASP.NET Web API. It should model courses and their students.

## Operations
It should support the following operations:

### Get a list of courses (10%)
> curl -i -X GET localhost:5000/api/courses

### Add a course (10%)
> curl -i -d "Name=Darri%20Steinn&TemplateId=T-55-Dar&startDate=2016-11-22&endDate=2016-12-22" -X POST localhost:5000/api/courses

### Update a course (10%)
> curl -i -d "Name=Darri%20Steinn&TemplateId=T-55-Dar&startDate=2016-11-22&endDate=2016-12-22" -X PUT localhost:5000/api/courses/0

### Delete a course (10%)
> curl -i -X DELETE localhost:5000/api/courses/1

### Get a course with a given ID (10%)
> curl -i -X GET localhost:5000/api/courses/0

### Get a list of students in a course (10%)
> curl -i -X GET localhost:5000/api/courses/0/students

### Add a student to a course (10%)
> curl -i -X POST -d "SSN=1501933119&Name=Darri%20Steinn%20Konradsson" localhost:5000/api/courses/0/students
