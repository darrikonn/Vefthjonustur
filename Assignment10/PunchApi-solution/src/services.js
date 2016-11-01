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
      elasticsearch = require('elasticsearch'),
      entities = require('./entities');

/*
 * Application setup
 */
const client = new elasticsearch.Client({
  host: 'localhost:9200',
  log: 'error'
});

/*
 * Services
 */
const getCompanies = (params, cb) => {
  client.search({
    'index': 'punchy',
    'type': 'company',
    '_source': ['id', 'name'],
    'size': params.max,
    'from': params.page*params.max,
    'body': {
      'sort': [{'name': {'order': 'asc'}}],
      'query': {
        'query_string': {
          'query': params.search,
          'fields': ['name', 'description']
        }
      }
    }
  }).then((doc) => {
    return cb(null, doc.hits.hits.map(d => d._source));
  }, (err) => {
    console.log(err);
    return cb(err);
  });
};

const getCompany = (query, cb) => {
  entities.Companies.findOne(query, '-__v', (err, docs) => {
    if (err) {
      return cb(err);
    }

    return cb(null, docs);
  });
};

const addCompany = (model, cb) => {
  getCompany({'name': model.name}, (err, docs) => {
    if (err) {
      return cb({'status': 500, 'message': 'Internal Server Error'});
    } else if (docs) {
      return cb({'status': 409, 'message': 'Company `' + model.name + '` already exists'});
    }

    // else add the company
    const company = new entities.Companies({
      'name': model.name,
      'punchCount': model.punchCount,
      'description': model.description || ''
    });
    company.save((err) => {
      if (err) {
        if (err.name === 'ValidationError') {
          return cb({'status': 412, 'message': Object.keys(err.errors).map(e => 
                err.errors[e].message)});
        }
        return cb({'status': 500, 'message': 'Unable to add the company due to an unknown error'});
      }

      // add to elasticsearch also
      client.index({
        'index': 'punchy',
        'type': 'company',
        'id': company._id.toString(),
        'body': {
          'id': company._id,
          'name': company.name,
          'punchCount': company.punchCount,
          'description': company.description
        }
      }).then((doc) => {
        return cb(null, {'_id': company._id});
      }, (err) => {
        console.log(err);
        return cb(null, {'_id': company._id});
      });
    });
  });
};

/*
 * Exports
 */
module.exports = {
  getCompanies,
  getCompany,
  addCompany
};
