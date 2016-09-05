# Assignment 2
The second assignment is to design and create a REST service using .NET Core and ASP.NET Web API. It should model courses and their students.<br/>
**NOTE**: I set the current semester to 20163 instead of 20153. Keep that in mind :).

## Operations
It should support the following operations:

### Get a list of courses
> curl -i -X GET localhost:5000/api/courses

### Get a list of courses by semester
> curl -i -X GET -d "semester=20153" localhost:5000/api/courses

### Get more detail of a course with a given ID
> curl -i -X GET localhost:5000/api/courses/0

### Update a course
> curl -i -d "CourseId=T-500-APIC&Semester=20163&StartDate=2016-11-22&EndDate=2016-12-22" -X PUT localhost:5000/api/courses/0

### Add a course 
> curl -i -d "CourseId=T-500-APIC&Semester=20163&StartDate=2016-11-22&EndDate=2016-12-22" -X POST localhost:5000/api/courses

### Delete a course 
> curl -i -X DELETE localhost:5000/api/courses/1

### Get a list of students in a course
> curl -i -X GET localhost:5000/api/courses/0/students

### Add a student to a course
> curl -i -X POST -d "SSN=1501933119&Id=0" localhost:5000/api/courses/0/students
