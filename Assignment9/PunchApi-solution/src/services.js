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
 * Services
 */
const getCompanies = (cb) => {
  entities.Companies.find({}, (err, docs) => {
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

const getUser = (query, cb) => {
  entities.Users.findOne(query, (err, docs) => {
    if (err) {
      return cb(err);
    }

    return cb(null, docs);
  });
};

const getPunches = (query, cb) => {
  entities.Punches.find(query, (err, docs) => {
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
      if (err.name === 'ValidationError') {
        return cb({'status': 412, 'message': Object.keys(err.errors).map(e => 
              err.errors[e].message)});
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
    if (err) {
      if (err.name === 'ValidationError') {
        return cb({'status': 412, 'message': Object.keys(err.errors).map(e => 
              err.errors[e].message)});
      }
      return cb({'status': 500, 'message': 'Unable to add the user due to an unknown error'});
    }

    return cb(null, {'_id': user._id, 'token': user.token});
  });
};

const addPunch = (id, model, cb) => {
  const punch = new entities.Punches({
    'company_id': model.id,
    'user_id': id,
    'created': model.created,
    'used': model.used
  });
  punch.save((err) => {
    if (err) {
      if (err.name === 'ValidationError') {
        return cb({'status': 412, 'message': Object.keys(err.errors).map(e => 
              err.errors[e].message)});
      }
      return cb({'status': 500, 'message': 'Unable to add the user due to an unknown error'});
    }

    return cb(null, {'_id': punch._id});
  });
};

const markPunches = (punches, cb) => {
  entities.Punches.update({'_id': {'$in': punches.map(p => p._id)}}, {'used': true}, {multi: true}, 
      (err) => {
    if (err) {
      return cb(err);
    }

    return cb(null, {'discount': true});
  });
};

/*
 * Exports
 */
module.exports = {
  getCompanies,
  getCompany,
  getUsers,
  getUser,
  getPunches,
  addCompany,
  addUser,
  addPunch,
  markPunches
};
