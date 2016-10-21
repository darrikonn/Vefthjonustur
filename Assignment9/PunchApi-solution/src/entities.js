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
const userSchema = Schema({
  name: {
    type: String,
    required: true
  },
  token: {
    type: String,
    required: true
  },
  gender: {
    type: String,
    required: true,
    validate: {
      validator: (val) => {
        return val == 'm' || val == 'f' || val == 'o';
      },
      message: '`{VALUE}` is not a valid gender. Please use `m` (male), `f` (female) or `o` (other).'
    }
  }
});

const companySchema = Schema({
  name: {
    type: String,
    required: true
  },
  punchCount: {
    type: Number,
    default: 10,
    min: [1, 'Minimum count is 1.']
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
    default: Date.now
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
  Users: mongoose.model('Users', userSchema),
  Companies: mongoose.model('Companies', companySchema),
  Punches: mongoose.model('Punches', punchSchema)
};
