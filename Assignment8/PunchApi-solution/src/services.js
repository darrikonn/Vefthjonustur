/* jshint esversion: 6 */

/*
 * author: Darri Steinn Konradsson
 * email: darrik13@ru.is
 * licence: MIT
 */

/*
 * Project includes
 */
const uuid = require('node-uuid'),
      entities = require('./entities.js');

/*
 * Application setup
 */

/*
 * Services
 */
const getCompanies = (cb) => {
  entities.Companies.find().toArray((err, docs) => {
    if (err) {
      cb(err);
      return;
    }

    cb(null, docs);
  });
};

const getCompany = (query, cb) => {
  entities.Companies.findOne(query).toArray((err, docs) => {
    if (err) {
      cb(err);
      return;
    }

    cb(null, docs);
  });
};

const getUsers = (cb) => {
  entities.Users.find().toArray((err, docs) => {
    if (err) {
      cb(err);
      return;
    }

    cb(null, docs);
  });
};

/*
 * Exports
 */
module.exports = {
  getCompanies,
  getCompany,
  getUsers
};
