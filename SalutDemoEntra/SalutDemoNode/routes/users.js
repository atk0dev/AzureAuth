var express = require('express');
var router = express.Router();

/* GET users listing. */
router.get('/', function(req, res, next) {
  res.send('respond with a resource');
});

router.get('/id', async function (req, res, next) {
  res.render('id', {
    idTokenClaims: req.session.account?.idTokenClaims,    
  });
});

module.exports = router;
