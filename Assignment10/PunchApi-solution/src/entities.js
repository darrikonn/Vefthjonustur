/* jshint esversion: 6 */

/*
 * author: Darri Steinn Konradsson
 * email: darrik13@ru.is
 * licence: MIT
 */

/*
 * Project includes
 */
const mongoose = require('mongoose');

/*
 * Application setup
 */
const Schema = mongoose.Schema,
      ObjectId = Schema.Types.ObjectId;

/*
 * Schemas
 */
const companySchema = Schema({
  name: {
    type: String,
    required: true
  },
  punchCount: {
    type: Number,
    default: 10,
    min: [1, 'Minimum count is 1.']
  },
  description: {
    type: String
  }
});

/*
 * Exports
 */
module.exports = {
  Companies: mongoose.model('Companies', companySchema)
};
