const { defineConfig } = require('cypress')
/*const {
  username,
  password
} = require('./config')
*/


module.exports = defineConfig({
  e2e: {
    url: Cypress.env('url'),
    login_username: Cypress.env('username'),
    login_password: Cypress.env('password'),
    experimentalOriginDependencies: true,
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});
