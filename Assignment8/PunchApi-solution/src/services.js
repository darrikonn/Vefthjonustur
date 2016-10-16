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
      mongoose = require('mongoose'),
      entities = require('./entities');

/*
 * Application setup
 */

/*
 * Services
 */
const getCompanies = (cb) => {
  entities.Companies.find((err, docs) => {
    if (err) {
      return cb(err);
    }

    return cb(null, docs);
  });
};

const getCompany = (query, cb) => {
  entities.Companies.findOne(query, (err, docs) => {
    if (err) {
      return cb(err);
    }

    return cb(null, docs);
  });
};

const getUsers = (cb) => {
  entities.Users.find({}, '-token', (err, docs) => {
    if (err) {
      return cb(err);
    }

    return cb(null, docs);
  });
};

const addCompany = (model, cb) => {
  const company = new entities.Companies({
    'name': model.name,
    'punchCount': model.punchCount
  });

  company.save((err) => {
    if (err) {
    console.log(err);
      if (err.name === 'ValidationError') {
        return cb({'status': 412, 'message': err.message});
      }
      return cb({'status': 500, 'message': 'Unable to add the company due to an unknown error'});
    }

    return cb(null, {'_id': company._id});
  });
};

const addUser = (model, cb) => {
  const user = new entities.Users({
    'name': model.name,
    'gender': model.gender,
    'token': uuid.v1()
  });
  user.save((err) => {
    console.log(err);
    if (err) {
      if (err.name === 'ValidationError') {
        return cb({'status': 412, 'message': err.errors.gender.message}); // sja her
      }
      return cb({'status': 500, 'message': 'Unable to add the user due to an unknown error'});
    }

    return cb(null, {'_id': user._id, 'token': user.token});
  });
};

/*
 * Exports
 */
module.exports = {
  getCompanies,
  getCompany,
  getUsers,
  addCompany,
  addUser
};
