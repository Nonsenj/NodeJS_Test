const express = require('express');

const app = express();
const port = 3000;

//Routes
app.get('/auth', async (req,res) => {
    console.log(req.query);
    res.send("Hello world! " + Date.now());
})

app.listen(port, () => {
    console.log("Listening on " + port);
})