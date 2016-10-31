/*
 * Project includes
 */
const express = require('express');

/*
 * Application setup
 */
const app = express();

app.get('/api/hello', (req, res) => {
  res.send('Hello world\n');
});

app.listen(4000, () => {
  console.log('Server is running on port 4000')
});
