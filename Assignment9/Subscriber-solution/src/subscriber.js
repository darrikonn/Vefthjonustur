/* jshint esversion: 6 */

/*
 * Project includes
 */
const amqp = require('amqplib/callback_api');

/*
 * Application setup
 */
const keys = ['punchcardapi.user.add', 'punchcardapi.punch.add', 'punchcardapi.punch.discount'];

/*
 * Subscriber using topic
 */
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
        switch (msg.fields.routingKey) {
          case keys[0]:
            console.log('User was added\n%s', msg.content);
            break;
          case keys[1]:
            console.log('User got a punch\n%s', msg.content);
            break;
          case keys[2]:
            console.log('User got discount\n%s', msg.content);
            break;
          default:
            console.log('Unknown log\n%s', msg.content);
            break;
        };
      }, {noAck: true});
    });
  });
});
