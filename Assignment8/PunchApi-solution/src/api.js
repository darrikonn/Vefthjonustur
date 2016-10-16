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
      services = require('./services');

/*
 * Application setup
 */
const router = express.Router(),
      jsonParser = bodyParser.json(),
      adminToken = 'WubbaLubbaDubDub';
router.use(bodyParser.urlencoded({ extended: false }));

/*
 * Api routes
 */
/*************** GETs ***************/
/*
 * GET: /api/companies
 * Examples: 
 *  i) curl -i -X GET localhost:5000/api/companies
 * Returns: a list of all the companies
 */
router.get('/companies', (req, res) => {
  services.getCompanies((err, docs) => {
    if (err) {
      return res.status(500).send('Unable to get companies due to an unknown error!');
    }

    return res.json(docs);
  });
});

/*
 * GET: /api/companies/{id}
 * Examples: 
 *  i) curl -i -X GET localhost:5000/api/companies/1
 * Returns: a company with the given id
 */
router.get('/companies/:id', (req, res) => {
  services.getCompany({'_id': req.params.id}, (err, docs) => {
    if (err) {
      return res.status(404).send('Invalid ObjectId, and therefore the company is not found!');
    } else if (docs === null) {
      return res.status(404).send('Company not found!');
    }

    return res.json(docs);
  });
});

/*
 * GET: /api/users
 * Examples: 
 *  i) curl -i -X GET localhost:5000/api/users
 * Returns: a list of all users
 */
router.get('/users', (req, res) => {
  services.getUsers((err, docs) => {
    if (err) {
      return res.status(500).send('Unable to get users due to an unknown error!');
    }

    return res.json(docs);
  });
});

/*************** POSTs ***************/
/*
 * POST: /api/companies
 * Examples:
 *  i)
 */
router.post('/companies', jsonParser, (req, res) => {
  if (req.headers.authorization.split(' ')[1] !== adminToken) {
    return res.status(401).send('Not Authorized');
  }

  services.addCompany(req.body, (err, dbres) => {
    if (err) {
      return res.status(err.status).send(err.message);
    }
    return res.status(201).json(dbres);
  });
});

/*
 * POST: /api/users
 * Examples:
 *  i)
 */
router.post('/users', jsonParser,(req, res) => {
  if (req.headers.authorization.split(' ')[1] !== adminToken) {
    return res.status(401).send('Not Authorized');
  }

  services.addUser(req.body, (err, dbres) => {
    if (err) {
      return res.status(err.status).send(err.message);
    }
    return res.status(201).send(dbres);
  });
});

/*
 * POST: /api/my/punches
 * Examples:
 *  i)
 */
router.post('/my/punches', jsonParser, (req, res) => {

});

/*
 * Exports
 */
module.exports = router;
