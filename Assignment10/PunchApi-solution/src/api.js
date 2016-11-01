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
      dateFormat = require('dateformat'),
      services = require('./services');

/*
 * Application setup
 */
const router = express.Router(),
      jsonParser = bodyParser.json(),
      ADMIN_TOKEN = 'WubbaLubbaDubDub',
      CONTENT_TYPE = 'application/json';
router.use(bodyParser.urlencoded({ extended: true }));

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
  const params = {
    page: req.query.page || 0,
    max: req.query.max || 20,
    search: req.query.search || '*'
  };

  services.getCompanies(params, (err, docs) => {
    if (err) {
      return res.status(500).json({'error': 'Unable to get companies due to an unknown error!'});
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
    if (err || docs === null) {
      return res.status(404).json({'error': 'Company not found!'});
    }

    return res.json(docs);
  });
});

/*************** POSTs ***************/
/*
 * POST: /api/companies
 * Examples:
 *  i) curl -i -H "Authorization: WubbaLubbaDubDub" -d "name=TeOgKaffi&punchCount=2" -X POST localhost:5000/api/companies
 * Returns: HTTP status code 201 with the company id.
 */
router.post('/companies', jsonParser, (req, res) => {
  if (req.headers.authorization !== ADMIN_TOKEN) {
    return res.status(401).json({'error': 'Not Authorized'});
  }
  if (req.headers['content-type'] !== CONTENT_TYPE) {
    return res.status(415).json({'error': 'Unsupported Media Type'});
  }

  services.addCompany(req.body, (err, dbres) => {
    if (err) {
      return res.status(err.status).json(err.message);
    }
    return res.status(201).json(dbres);
  });
});

/*
 * Exports
 */
module.exports = router;
