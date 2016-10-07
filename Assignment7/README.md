# Assignment 7
Web API written in Node/Express.

## Introduction
The API will be used as a foundation for a new service called "punchcard.com", where both companies and their users can register, and users can then issue a "punch". When they've collected enough punches (usually 10), they will get a discount at the company.

## Setup
Run the following command to setup:
> npm install

## Run
In order to run the application, run the following command from the root of the directory:
> node src/index.js

## Possible commands
**Note**: these commands are both for precondition and error handling. <br/>
> curl -i -X POST -d "name=prump&punchCount=10" localhost:5000/api/companies<br/>
  curl -i -X POST -d "name=skitur&punchCount=5" localhost:5000/api/companies<br/>
  curl -i -X POST -d "name=skitur" localhost:5000/api/companies<br/>
  curl -i -X POST -d "punchCount=5" localhost:5000/api/companies<br/>
  curl -i -X POST localhost:5000/api/companies<br/>
  curl -i -X POST -d "name=Darri Steinn Konradsson&email=darrik13@ru.is" localhost:5000/api/users<br/>
  curl -i -X POST -d "name=dabbeg&email=davidgh13@ru.is" localhost:5000/api/users<br/>
  curl -i -X POST -d "name=Darri Steinn Konradsson&email=darrik13ru.is" localhost:5000/api/users<br/>
  curl -i -X POST -d "name=dabbeg" localhost:5000/api/users<br/>
  curl -i -X POST localhost:5000/api/users<br/>
  curl -i -X POST -d "email=davidgh13@ru.is" localhost:5000/api/users<br/>
  curl -i -X POST -d "company=0" localhost:5000/api/users/0/punches<br/>
  curl -i -X POST -d "company=1" localhost:5000/api/users/0/punches<br/>
  curl -i -X POST -d "company=2" localhost:5000/api/users/0/punches<br/>
  curl -i -X POST -d "company=1" localhost:5000/api/users/2/punches<br/>
  curl -i -X POST localhost:5000/api/users/1/punches
