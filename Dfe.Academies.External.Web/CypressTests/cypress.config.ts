import { url, login_username, login_password, dfeSignInTestEnvURLForA2BDevAndA2BTest, dfeSignInTestEnvForgotPasswordCodeInputURLForA2BDevAndA2BTest, dfeSignInTestEnvCreateAccountForA2BDevAndA2BTest } from './config'

import { Cypress } from 'cypress';
 
const { defineConfig } = require('cypress')
/*const {
  username,
  password
} = require('./config')
*/


module.exports = defineConfig({
  e2e: {
    url: url,
    login_username: Cypress.env('username'),
    login_password: Cypress.env('password'),
    experimentalOriginDependencies: true,
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});
