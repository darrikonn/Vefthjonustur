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
 * Publisher
 */
// queue
/*amqp.connect('amqp://localhost', (err, conn) => {
  conn.createChannel((err, ch) => {
    const q = 'punchcardApi';
    let msg = process.argv.slice(2).join(' ') || "Hello World!";

    ch.assertQueue(q, {durable: true});
    ch.sendToQueue(q, new Buffer(msg), {persistent: true});
    console.log(" [x] Sent '%s'", msg);
  });
  setTimeout(() => { conn.close(); process.exit(0) }, 500);
});*/

// topic
const send = amqp.connect('amqp://localhost', (err, conn) => {
  conn.createChannel((err, ch) => {
    const ex = 'punchcardApi';
    var msg = 'Hello World!';

    ch.assertExchange(ex, 'topic', {durable: false});
    ch.publish(ex, keys[0], new Buffer(msg));
    console.log(" [x] Sent %s:'%s'", keys[0], msg);
  });

  setTimeout(() => { conn.close(); process.exit(0) }, 500);
});

module.exports = {
  send
}
