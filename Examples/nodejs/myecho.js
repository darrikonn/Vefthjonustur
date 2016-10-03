var net = require('net'); // using library net

var server = net.createServer((socket) => {
  console.log('incoming connection');

  socket.on('data', (data) => {
    console.log('Data from client: ' + data);
  });
});

server.listen(6000);
