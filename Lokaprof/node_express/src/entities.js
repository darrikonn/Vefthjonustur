const mongoose = require('mongoose');

const Schema = mongoose.Schema,
      ObjectId = Schema.Types.ObjectId;

const courseSchema = new Schema({
  name: {
    type: String,
    required: true
  },
  teacher: {
    type: String,
    required: true
  }
});

module.exports = {
  Courses: mongoose.model('Courses', courseSchema)
};
