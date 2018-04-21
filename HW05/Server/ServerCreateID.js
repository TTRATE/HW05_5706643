
var express = require('express');
var app = express();
var mysql = require('mysql');
var connection = mysql.createConnection({
    host: 'pannawatdata.ck2codzcnj4m.ap-southeast-1.rds.amazonaws.com',
    user: 'pannawat',
    password: 'ttr987654321',
    database: 'HW05_IDdata'

});

connection.connect(function (err) {

    if (err) {
        console.log('Error Connecting', err.stack);
        return;
    }
    console.log('Connected as id', connection.threadId);
});

app.get('/user/add', function (req, res) {
    var userID = req.query.userID;
    var password = req.query.password;
    var score = req.query.score;

    var ID = [[userID, password, score]];
    AddUserID(ID, function (err, resualt) {
        res.end(resualt);
    });

});
app.get('/user/id', function (req, res) {

    ShowUserID(function (err, resualt) {
        res.end(resualt);
    })
})

app.get('/user/login/:userID/:password', function (req, res) {

    var userIDLog = req.params.userID;
    var passwordLog = req.params.password;

    //var logUser = [[userIDLog, passwordLog]];

    LoginUserID(userIDLog,passwordLog, function (err, resualt) {
        res.end(resualt);
    })
})

var server = app.listen(8081, function () {
    console.log('Server Running');

});

function AddUserID(ID, callback) {
    var sql = 'INSERT INTO user(userID,password,score) values ?';


    connection.query(sql, [ID], function (err) {
        var res = '[{"success" : "true"}]';
        if (err) {

            res = '{["success" : "false"]}';
            throw err;
        }

        callback(null, res);

    });
}
function ShowUserID(callback) {
    var sql = 'SELECT userID,score FROM user ORDER BY score DESC limit 5';


    connection.query(sql, function (err, rows, fields) {
        if (err) throw err;

        json = JSON.stringify(rows);

        callback(null, json);
    });
}
function LoginUserID(userIDsend,passwordsend, callback) {
    
    var json = '';
    var sql = 'SELECT count(id)AS off  FROM user WHERE userID = ? and password = ?'
    connection.query(sql, [userIDsend,passwordsend], function (err, rows, fields) {
        
        if (err) throw err;
        
        json = JSON.stringify(rows);

        callback(null, json);
    });
}