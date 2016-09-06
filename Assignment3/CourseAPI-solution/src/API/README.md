# Assignment 3
The third assignment is to design and create a REST service using .NET Core and ASP.NET Web API. It should model courses and their students.<br/>
**NOTE**: I set the current semester to 20163 instead of 20153. Keep that in mind :).

## Operations
It should support the following operations:

### 1. POST /api/courses
> curl -i -X POST -d "TemplateID=T-514-VEFT&StartDate=20160818T00:00:00&EndDate=2016-11-10T00:00:00&Semester=20163&MaxStudents=4" localhost:5000/api/courses

### 2. POST /api/courses/{ID}/students 
> curl -X POST -i -d "SSN=9876543210" localhost:5000/api/courses/1/students

### 3. POST /api/courses/{ID}/students
> curl -X POST -i -d "SSN=1234567890" localhost:5000/api/courses/1/students

### 4. POST /api/courses/{ID}/students 
> curl -X POST -i -d "SSN=1234567890" localhost:5000/api/courses/1/students

### 5. POST /api/courses/{ID}/students
> curl -X POST -i -d "SSN=1234567891" localhost:5000/api/courses/1/students

### 6. POST /api/courses/{ID}/students
> curl -X POST -i -d "SSN=1234567892" localhost:5000/api/courses/1/students

### 7. POST /api/courses/{ID}/students
> curl -X POST -i -d "SSN=1234567893" localhost:5000/api/courses/1/students

### 8. POST /api/courses/{ID}/students
> curl -X POST -i -d "SSN=1234567894" localhost:5000/api/courses/1/students

### 9. DELETE /api/courses/{ID}/students/1234567890
> curl -X DELETE -i -d "SSN=1234567890" localhost:5000/api/courses/1/students

### 10. GET /api/courses/{ID}/students
> curl -X GET -i localhost:5000/api/courses/1/students

### 11. POST /api/courses/{ID}/waitinglist
> curl -X POST -i -d "SSN=9876543210" localhost:5000/api/courses/1/waitinglist

### 12. POST /api/courses/{ID}/waitinglist
> curl -X POST -i -d "SSN=1234567890" localhost:5000/api/courses/1/waitinglist

### 13. GET /api/courses/{ID}/waitinglist
> curl -X GET -i localhost:5000/api/courses/1/waitinglist

### 14. POST /api/courses/{ID}/students 
> curl -X POST -i -d "SSN=1234567890" localhost:5000/api/courses/1/students

### 15. GET /api/courses/{ID}/waitinglist
> curl -X GET -i localhost:5000/api/courses/1/waitinglist

### 16. POST /api/courses/{ID}/waitinglist
> curl -X POST -i -d "SSN=1234567895" localhost:5000/api/courses/1/waitinglist

### 17. POST /api/courses/{ID}/waitinglist
> curl -X POST -i -d "SSN=1234567895" localhost:5000/api/courses/1/waitinglist

### 18. POST /api/courses/{ID}/waitinglist
> curl -X POST -i -d "SSN=1234567891" localhost:5000/api/courses/1/waitinglist
