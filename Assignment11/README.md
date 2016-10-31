# Assignment 11
A very simple API using Express, with a single route: "/api/hello" which always responds with the text "Hello world". <br/>
Then a dockerfile which is based on some Linux docker image (Ubuntu should be just fine), and includes the code.

## Setup (Arch Linux)
First start the docker daemon with (you can run it in the background if your want):
> sudo dockerd

Then build the docker image with:
> sudo docker build -t helloapi:latest .

## Run
To run a container, run the following command:
> sudo docker run -p 4000:4000 --name helloapi_container helloapi:latest

## Usage
Now you can use the api by visiting the following url in a browser:
> http://localhost:4000/api/hello
