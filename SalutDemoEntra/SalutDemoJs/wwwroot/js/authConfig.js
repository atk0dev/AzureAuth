 
const msalConfig = {
  auth: {
        clientId: "",
        authority: "",
        redirectUri: "https://localhost:7018/",
  },
  cache: {
    cacheLocation: "sessionStorage", 
    storeAuthStateInCookie: false, 
  }
};  

const loginRequest = {
  scopes: ["openid"]
};
  
