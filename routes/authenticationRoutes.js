const mongoose = require('mongoose');
const Account = mongoose.model('accounts');

module.exports = app => {
    
    //Routes
    app.get('/account', async (req, res) => {
        const { rUsername, rPassword } = req.query

        if (rUsername == null || rPassword == null) {
            res.send("Invalid credentials");
            return;
        }

        var userAccount = await Account.findOne({ username: rUsername });
        if (userAccount == null) {
            //Create a new account
            console.log("Create new account..");

            var newAccount = new Account({
                username: rUsername,
                password: rPassword,

                lastAuthentication: Date.now()
            })

            await newAccount.save();

            res.send(newAccount);
            return;
        } else {
            if (rPassword == userAccount.password) {
                userAccount.lastAuthentication = Date.now();

                await userAccount.save();

                console.log("Retrieving account...");
                res.send(userAccount);
                return;
            }
        }

        res.send("Invalid credentials");
        return;
    })


    app.post('/account', async (req, res) => {
        var data = req.body

        var newAccount = new Account({
            username: data.rUsername,
            password: data.rPassword,

            lastAuthentication: Date.now()
        })

        await newAccount.save();
        res.send(newAccount);
        return;
    })
    
}

