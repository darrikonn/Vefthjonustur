# Assignment 10
In this project we continue on the "Punch card" project, which we now refer to as "Punchy". In this final assignment we will work solely with the company part and augment it with ElasticSearch. 

## Setup
Run `npm install` (or `yarn install` if you're cool) from the source.

## Run
Run the following in a terminal to start the mongo server:
> mkdir /tmp/data && mongod --dbpath /tmp/data <br/>

In another terminal, start the elasticsearch server with:
> elasticsearch

Finally run the application with:
> node src/index.js

## Possible route commands
> curl -i -X GET localhost:5000/api/companies

> curl -i -X GET localhost:5000/api/companies?page=0&max=2

> curl -i -X GET localhost:5000/api/companies?search=TeOgKaffi

> curl -i -X GET localhost:5000/api/companies/1

> curl -i -H "Authorization: WubbaLubbaDubDub" -H "Content-Type: application/json" -d '{"name":"TeOgKaffi","punchCount":"2", "description": "Coffee company"}' -X POST localhost:5000/api/companies
