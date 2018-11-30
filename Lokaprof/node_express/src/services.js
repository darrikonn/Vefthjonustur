const uuid = require('node-uuid'),
      mongoose = require('mongoose'),
      entities = require('./entities');

const getCourses = (cb) => {
  entities.Courses.find({}, (err, docs) => {
    if (err) {
      return cb(err);
    }
    return cb(null, docs);
  });
};

const getCourse = (query, cb) => {
  entities.Courses.find(query, (err, docs) => {
    if (err) {
      return cb(err);
    }
    return cb(null, docs);
  });
};

const addCourse = (model, cb) => {
  const course = new entities.Courses({
    'name': model.name,
    'teacher': model.teacher
  });
  course.save((err) => {
    if (err) {
      return cb(err);
    }
    return cb(null, {'_id': course._id});
  });
};

module.exports = {
  getCourses,
  getCourse,
  addCourse
};
