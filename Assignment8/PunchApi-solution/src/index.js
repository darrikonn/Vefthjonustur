/* jshint esversion: 6 */

/*
 * author: Darri Steinn Konradsson
 * email: darrik13@ru.is
 * licence: MIT
 */

/*
 * Project includes
 */
const express = require('express'),
      mongoose = require('mongoose'),
      api = require('./api');

/*
 * Application setup
 */
const app = express(),
      url = 'mongodb://localhost:27017/app';
//app.use('/api', api);

mongoose.connect(url, (err, db) => {
  if (err) {
    console.log('Couldn\'t connect to ' + url);
  }
});

mongoose.connection.once('open', () => {
  console.log('Connected to mongoose database on ' + url);

  let port;
  app.listen((port = process.env.PORT || 5000), () => {
    console.log('Web server started on port ' + port); 
  });
});
