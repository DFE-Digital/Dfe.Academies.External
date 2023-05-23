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
    supportFile: 'Dfe.Academies.External.Web/CypressTests/cypress/support/e2e.ts',
    url: url,
    login_username: login_username,
    login_password: login_password,
    experimentalOriginDependencies: true,
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },
});
