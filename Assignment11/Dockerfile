FROM ubuntu:14.04

RUN apt-get update && apt-get upgrade -y
RUN apt-get install curl -y
RUN curl -SL https://deb.nodesource.com/setup_4.x | sudo -E bash -
RUN apt-get install -y nodejs && apt-get clean

WORKDIR /app
ADD package.json .
ADD src src/.

RUN npm install
CMD ["node", "src/index.js"]
