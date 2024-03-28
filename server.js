const express = require('express');
const keys = require('./config/keys.js');

const app = express();
const port = 3000;

const mongoose = require('mongoose');

mongoose.connect(keys.mongoURI);

//Routes
app.get('/auth', async (req,res) => {
    console.log(req.query);
    res.send("Hello world! " + Date.now());
})

app.listen(port, () => {
    console.log("Listening on " + keys.port);
})