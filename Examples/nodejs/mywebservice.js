var http = require('http');

var server = http.createServer((request, response) => {
  response.writeHead(200, {'content-type': 'text/plain'});
  response.write('Hallo\n');
  setTimeout(() => {
    response.end('This is a fake message from a fake database');
  }, 2000);
});

server.listen(7000);
