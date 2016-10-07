/* jshint esversion: 6 */

/*
 * author: Darri Steinn Konradsson
 * email: darrik13@ru.is
 * licence: MIT
 */

/*
 * project includes
 */
const express = require('express'),
      bodyParser = require('body-parser'),
      dateFormat = require('dateformat'),
      validator = require('validator');

/*
 * application setup
 */
const app = express(),
      jsonParser = bodyParser.json();
// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: false }));

/*
 * in memory database
 */
const companies = [],
      users = [];

/********** GETs **********/
/*
 * GET /api/companies
 * Examples:
 *  i) curl -i -X GET localhost:5000/api/companies
 */
app.get('/api/companies', (req, res) => {
  return res.json(companies);  
});

/*
 * GET /api/companies/{id}
 * Examples:
 *  i) curl -i -X GET localhost:5000/api/companies/0
 */
app.get('/api/companies/:id', (req, res) => {
  let id = parseInt(req.params.id);
  if (id >= companies.length || id < 0) {
    res.statusCode = 404;
    return res.send('404 Not Found: Company not found!');
  }

  return res.json(companies[id]);
});

/*
 * GET /api/users
 * Examples:
 *  i) curl -i -X GET localhost:5000/api/users
 */
app.get('/api/users', (req, res) => {
  return res.json(users);  
});

/*
 * GET /api/users/{id}/punches
 * With possibility of querying: /api/users/{id}/punches?company={cid}
 * Examples:
 *  i) curl -i -X GET localhost:5000/api/users/0/punches
 *  ii) curl -i -X GET localhost:5000/api/users/0/punches?company=1
 */
app.get('/api/users/:id/punches', (req, res) => {
  let id = parseInt(req.params.id);
  if (id >= users.length || id < 0) {
    res.statusCode = 404;
    return res.send('404 Not Found: User not found!');
  }

  // get query
  let company = req.query.company;
  if (company) {
    return res.json(users[id].punches.filter((p) => {return p.company == company;}));
  }

  return res.json(users[id].punches);
});

/********** POSTs **********/
/*
 * POST /api/companies
 * Examples:
 *  i) curl -i -X POST -d "name=veft&punchCount=10" localhost:5000/api/companies
 * Parameters:
 *  'name' and 'punchCount' are required
 */
app.post('/api/companies', jsonParser, (req, res) => {
  let model = req.body;
  if (!model || !model.name || !model.punchCount) {
    res.statusCode = 412;
    return res.send('412 Precondition Failed: "name" and "punchCount" are required!');
  } else if (!validator.isInt(model.punchCount)) {
    res.statusCode = 400;
    return res.send('400 Bad Request: "punchCount" is not a valid integer!');
  }

  // add the company
  let company = {
    name: model.name,
    punchCount: model.punchCount
  };
  companies.push(company);

  res.statusCode = 201;
  return res.send(company);
});

/*
 * POST /api/users
 * Examples:
 *  i) curl -i -X POST -d "name=Darri Konn&email=darrik13@ru.is" localhost:5000/api/users
 * Parameters:
 *  'name' and 'email' are required
 */
app.post('/api/users', jsonParser, (req, res) => {
  let model = req.body;
  if (!model || !model.name || !model.email) {
    res.statusCode = 412;
    return res.send('412 Precondition Failed: "name" and "email" are required!');
  } else if (!validator.isEmail(model.email)) {
    res.statusCode = 400;
    return res.send('400 Bad Request: "email" is not a valid email address!');
  }

  // add the user
  let user = {
    name: model.name,
    email: model.email,
    punches: []
  };
  users.push(user);

  res.statusCode = 201;
  return res.send(user);
});

/*
 * POST /api/users/{id}/punches
 * Examples:
 *  i) curl -i -X POST -d "company=1" localhost:5000/api/users/0/punches
 * Parameters:
 *  'company' id is required
 */
app.post('/api/users/:id/punches', jsonParser, (req, res) => {
  let id = parseInt(req.params.id);
  if (id >= users.length || id < 0) {
    res.statusCode = 404;
    return res.send('404 Not Found: User not found!');
  }

  let model = req.body;
  if (!model || !model.company) {
    res.statusCode = 412;
    return res.send('412 Precondition Failed: "company" id is required!');
  } else if (!validator.isInt(model.company)) {
    res.statusCode = 400;
    return res.send('400 Bad Request: "company" is not a valid integer!');
  } else if (model.company >= companies.length || model.company < 0) {
    res.statusCode = 404;
    return res.send('404 Not Found: Company not found!');
  }

  // add the punch
  let punch = {
    date: dateFormat(Date.now()),
    company: model.company
  };
  users[id].punches.push(punch);

  res.statusCode = 201;
  return res.send(punch);
});


app.listen(process.env.PORT || 5000);
