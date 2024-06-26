const express = require('express');
const keys = require('./config/keys.js');

const app = express();
app.use(express.json());
const port = 3000;

// Setting up DB
const mongoose = require('mongoose');
mongoose.connect(keys.mongoURI);

// Setup database models
require('./model/Account');

// Setup the routes
require('./routes/authenticationRoutes')(app);


app.listen(port, () => {
    console.log("Listening on " + keys.port);
})
