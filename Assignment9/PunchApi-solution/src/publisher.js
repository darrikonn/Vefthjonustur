/* jshint esversion: 6 */

/*
 * Project includes
 */
const amqp = require('amqplib/callback_api');

/*
 * Application setup
 */
const ex = 'punchcardApi';

/*
 * Publisher using topic
 */
const send = (key, msg) => {
  amqp.connect('amqp://localhost', (err, conn) => {
    if (err) {
      console.log('Couldn\'t connect to the amqp server');
      return;
    }

    conn.createChannel((err, ch) => {
      if (err) {
        console.log('Couldn\'t create a channel');
        return;
      }

      ch.assertExchange(ex, 'topic', {durable: false});
      ch.publish(ex, key, new Buffer(JSON.stringify(msg)));
    });
  
    setTimeout(() => {
      conn.close();
    }, 500);
  });
};

module.exports = {
  send
};
