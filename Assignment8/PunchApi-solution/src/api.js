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

/*
 * GET: /api/users
 * Examples: 
 *  i) curl -i -X GET localhost:5000/api/users
 * Returns: a list of all users
 */
router.get('/users', (req, res) => {
  services.getUsers((err, docs) => {
    if (err) {
      return res.status(500).json({'error': 'Unable to get users due to an unknown error!'});
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
  if (req.headers.authorization !== adminToken) {
    return res.status(401).json({'error': 'Not Authorized'});
  }

  services.addCompany(req.body, (err, dbres) => {
    if (err) {
      return res.status(err.status).json(err.message);
    }
    return res.status(201).json(dbres);
  });
});

/*
 * POST: /api/users
 * Examples:
 *  i) curl -i -H "Authorization: WubbaLubbaDubDub" -d "name=Darri Steinn&gender=m" -X POST localhost:5000/api/users
 * Returns: HTTP status code 201 along with the id and token of the user
 */
router.post('/users', jsonParser,(req, res) => {
  if (req.headers.authorization !== adminToken) {
    return res.status(401).json({'error': 'Not Authorized'});
  }

  services.addUser(req.body, (err, dbres) => {
    if (err) {
      return res.status(err.status).json(err.message);
    }
    return res.status(201).json(dbres);
  });
});

/*
 * POST: /api/my/punches
 * Examples:
 *  i) curl -i -H "Authorization: 1" -d "id=1" -X POST localhost:5000/api/my/punches
 * Returns: {discount: true} if total punches equal the puncCount of the company,
 *  else if returns HTTP status code 201 along with the punch id.
 */
router.post('/my/punches', jsonParser, (req, res) => {
  services.getUser({'token': req.headers.authorization}, (uerr, user) => {
    if (uerr || user === null) {
      return res.status(401).json({'error': 'Not Authorized'});
    }
    
    services.getCompany({'_id': req.body.id}, (cerr, company) => {
      if (cerr || company === null) {
        return res.status(404).json({'error': 'Company not found!'});
      }

      services.addPunch(user._id, req.body, (err, dbpres) => {
        if (err) {
          return res.status(err.status).json(err.message);
        }

        // if the total amount of non-used punches if equal the company's punchCount,
        // then mark the punches as used and return discount true.
        services.getPunches({'company_id': req.body.id, 'user_id': user._id, 'used': false}, 
            (perr, punches) => {
          if (perr) {
            return res.status(500).json({'error': 'Unable to get punches due to an unknown error!'});
          }

          if (punches.length === company.punchCount) {
            services.markPunches(punches, (merr, dbmres) => {
              if (merr) {
                return res.status(500).json({'error': 'Unable to mark punches due to an unknown error!'});
              }

              return res.json(dbmres);
            });
          } else {
            return res.status(201).json(dbpres);
          }
        });
      });
    });
  });
});

/*
 * Exports
 */
module.exports = router;
