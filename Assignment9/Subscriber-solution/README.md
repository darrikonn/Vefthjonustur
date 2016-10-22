# Assignment 9
The subscriber is a simple Node.js program which listens to events from the message queue, and handles 3 cases by printing out to the console one of the messages below:
* "User was added"
* "User got a punch"
* "User got discount"

## Setup
Run `npm install` (or `yarn install` if you're cool) from the source.

## Run
Run from a terminal:
> sudo rammitmq-server

In another terminal run the application with:
> node src/subscriber.js
