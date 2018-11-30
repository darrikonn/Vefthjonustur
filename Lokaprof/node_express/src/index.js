const express = require('express'),
      mongoose = require('mongoose'),
      api = require('./api');

const app = express(),
      url = 'mongodb://localhost:27017/app';
app.use('/api', api);

mongoose.connect(url, (err) => {
  if (err) {
    console.log('Couldn\'t connect');
    return;
  } else {
    mongoose.Promise = global.Promise;
    console.log('Connected to mongoose');

    app.listen(5000, () => {
      console.log('Connected to port 5000');
    });
  }
});
