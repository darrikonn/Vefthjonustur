const express = require('express'),
      bodyParser = require('body-parser'),
      services = require('./services');

const router = express.Router(),
      jsonParser = bodyParser.json();
router.use(bodyParser.urlencoded({extended: false}));

router.get('/courses', (req, res) => {
  services.getCourses((err, docs) => {
    if (err) {
      return res.status(500).json({'error': err});
    }
    return res.json(docs);
  });
});

router.get('/courses/:id', (req, res) => {
  services.getCourse({'_id': req.params.id}, (err, docs) => {
    if (err || docs === null) {
      return res.status(500).json({'error': err});
    }
    return res.json(docs);
  });
});

router.post('/courses', jsonParser, (req, res) => {
  services.addCourse(req.body, (err, dbres) => {
    if (err) {
      return res.status(500).json({'error': err});
    }
    return res.status(201).json(dbres);
  });
});

module.exports = router;
