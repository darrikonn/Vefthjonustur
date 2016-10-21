/* jshint esversion: 6 */

/*
 * Project includes
 */
const amqp = require('amqplib/callback_api');

/*
 * Application setup
 */
const keys = ['punch.addUser', 'punch.addPunch', 'punch.discount'];

/*
 * Subscriber
 */
// queue
/*amqp.connect('amqp://localhost', (err, conn) => {
  conn.createChannel((err, ch) => {
    const q = 'punchcardApi';

    ch.assertQueue(q, {durable: true});
    ch.prefetch(1);
    console.log(" [*] Waiting for messages in %s. To exit press CTRL+C", q);
    ch.consume(q, (msg) => {
      var secs = msg.content.toString().split('.').length - 1;

      console.log(" [x] Received %s", msg.content.toString());
      setTimeout(() => {
        console.log(" [x] Done");
        ch.ack(msg);
      }, secs * 1000);
    }, {noAck: false});
  });
});*/

// topic
amqp.connect('amqp://localhost', (err, conn) => {
  conn.createChannel((err, ch) => {
    const ex = 'punchcardApi';

    ch.assertExchange(ex, 'topic', {durable: false});

    ch.assertQueue('', {exclusive: true}, (err, q) => {
      console.log(' [*] Waiting for logs. To exit press CTRL+C');

      keys.forEach((key) => {
        ch.bindQueue(q.queue, ex, key);
      });

      ch.consume(q.queue, (msg) => {
        console.log(" [x] %s:'%s'", msg.fields.routingKey, msg.content.toString());
      }, {noAck: true});
    });
  });
});
