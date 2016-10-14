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
      bodyParser = require('body-parser'),
      ObjectId = require('mongoose').Schema.ObjectId,
      services = require('./services.js');

/*
 * Application setup
 */
const app = express(),
      jsonParser = bodyParser.json();
app.use(bodyParser.urlencoded({ extended: false }));

/*
 * Api routes
 */
/*************** GETs ***************/
/*
 * GET: /api/companies
 * Examples: 
 *  i) 
 */
app.get('/api/companies', (req, res) => {
  services.getCompanies((err, docs) => {
    if (err) {
      return res.status(500).send('Unable to get companies');
    }

    res.send(docs);
  });
});

/*
 * GET: /api/companies/{id}
 * Examples: 
 *  i) 
 */
app.get('/api/companies/:id', (req, res) => {
  const id = parseInt(req.params.id);
  services.getCompany({'_id': new ObjectId(id)}, (err, docs) => {
    if (err) {
      return res.status(500).send('Unable to get company');
    }

    res.send(docs);
  });
});

/*
 * GET: /api/users
 * Examples: 
 *  i) 
 */
app.get('/api/users', (req, res) => {
  services.Users((err, docs) => {
    if (err) {
      return res.status(500).send('Unable to get users');
    }

    res.send(docs);
  });
});

/*************** POSTs ***************/
/*
 * POST: /api/companies
 * Examples:
 *  i)
 */
app.post('/api/companies', (req, res) => {

});

/*
 * POST: /api/users
 * Examples:
 *  i)
 */
app.post('/api/users', (req, res) => {

});

/*
 * POST: /api/my/punches
 * Examples:
 *  i)
 */
app.post('/api/my/punches', (req, res) => {

});

/*
 * Exports
 */
module.export = app;
