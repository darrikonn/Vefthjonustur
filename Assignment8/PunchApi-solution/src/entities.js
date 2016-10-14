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
      ObjectId = Schema.ObjectId;

/*
 * Schemas
 */
const userSchema = new Schema({
  name: String,
  token: {
    type: String,
    required: true
  },
  gender: {
    type: String,
    validate: {
      validator: (val) => {
        return val == 'm' || val == 'f' || val == 'o';
      },
      message: 'Gender not valid. Valid types are "m" (male), "f" (female) or "o" (other).'
    }
  }
});

const companySchema = new Schema({
  name: String,
  punchCount: {
    type: Number,
    default: 10,
             min: [0, 'Minimum count is 0.']
  }
});

const punchSchema = new Schema({
  company_id: {
    type: ObjectId,
    required: true
  },
  user_id: {
    type: ObjectId,
    required: true
  },
  created: {
    type: Date,
    default: new Date()
  },
  used: {
    type: Boolean,
    default: false
  }
});

/*
 * Exports
 */
module.exports = {
  Users: mongoose.model("Users", userSchema),
  Companies: mongoose.model("Companies", companySchema),
  Punches: mongoose.model("Punches", punchSchema)
};
