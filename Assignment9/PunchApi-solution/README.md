# Assignment 9
In this project we add a publisher and a subscriber to the previous assignment.

## Setup
Make sure to run first the subscriber in the Subscriber-solution, read the markdown file. <br/>
Run `npm install` (or `yarn install` if you're cool) from the source.

## Run
In two separate terminals run:
> TERM1: mkdir /tmp/data && mongod --dbpath /tmp/data <br/>
  TERM2: mongo<br/>
         > use app

In yet another terminal run the application with:
> node src/index.js

## Possible route commands
curl -i -X GET localhost:5000/api/companies
curl -i -X GET localhost:5000/api/companies/1
curl -i -X GET localhost:5000/api/users
curl -i -H "Authorization: WubbaLubbaDubDub" -d "name=TeOgKaffi&punchCount=2" -X POST localhost:5000/api/companies
curl -i -H "Authorization: WubbaLubbaDubDub" -d "name=Darri Steinn&gender=m" -X POST localhost:5000/api/users
curl -i -H "Authorization: 1" -d "id=1" -X POST localhost:5000/api/my/punches
