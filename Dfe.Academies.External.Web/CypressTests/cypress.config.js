const { defineConfig } = require('cypress')
/*const {
  username,
  password
} = require('./config')
*/


module.exports = defineConfig({
  e2e: {
    url: 'https://s184t01-a2bextcdnendpoint-ezfhhdbpembpanh5.z01.azurefd.net',
    login_username: 'dan.good@education.gov.uk',
    login_password: 'P1ngO*1984',
    experimentalOriginDependencies: true,
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});
