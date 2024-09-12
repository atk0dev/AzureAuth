// Create the main myMSALObj instance
// configuration parameters are located at authConfig.js
const myMSALObj = new Msal.UserAgentApplication(msalConfig);

function signIn() {
  myMSALObj.loginPopup(loginRequest)
    .then(loginResponse => {

      console.log("id_token acquired at: " + new Date().toString());
      console.log(loginResponse);
      
      var account = myMSALObj.getAccount();
      alert("Hello " + account.name + "!");
      
    }).catch(error => {
      console.log(error);
    });
}

