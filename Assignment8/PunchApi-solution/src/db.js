/* jshint esversion: 6 */

/*
 * author: Darri Steinn Konradsson
 * email: darrik13@ru.is
 * licence: MIT
 */

/*
 * Project includes
 */
const mongoose = require('mongoose');

/*
 * Application setup
 */
let state = {
  db: null,
};

/*
 * Database functions
 */
const connect = (url, cb) => {
  if (state.db) {
    return cb();
  }

  mongoose.connect(url, (err, db) => {
    if (err) {
      return cb(err);
    }
    state.db = db;
    cb();
  });
};

const get = () => {
  return state.db;
};

const close = (cb) => {
  if (state.db) {
    state.db.close((err, result) => {
      state.db = null;
      state.mode = null;
      cb(err);
    });
  }
};

module.exports = {
  connect,
  get,
  close
};
