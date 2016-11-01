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
      elasticsearch = require('elasticsearch'),
      api = require('./api');

/*
 * Application setup
 */
const app = express(),
      url = 'mongodb://localhost:27017/app';
app.use('/api', api);
const client = new elasticsearch.Client({
  host: 'localhost:9200',
  log: 'error'
});

mongoose.connect(url, (err) => {
  if (err) {
    console.log('Couldn\'t connect to ' + url);
    return;
  } else {
    mongoose.Promise = global.Promise;
    console.log('Connected to mongoose database on ' + url);

    client.indices.create({ 
      'index': 'punchy',
      'body': {
        'mappings': {
          'company': {
            'properties': {
              'name': { 'type': 'keyword' },
              'id': { 'type': 'keyword' },
              'description': { 'type': 'text' }
            }
          }
        }
      }
    }, (err, resp) => {
      if (!err) {
        console.log('create', resp);
      }

      let port;
      app.listen((port = process.env.PORT || 5000), () => {
        console.log('Web server started on port ' + port); 
      });
    });
  }
});
