const { defineConfig } = require ('cypress')
require("dotenv").config(); // Load the .env file


module.exports = defineConfig({
  env: {
    URL: process.env.URL,
    LOGIN_USERNAME: process.env.LOGIN_USERNAME,
    LOGIN_PASSWORD: process.env.LOGIN_PASSWORD,
  },
  e2e: {
    supportFile: 'Dfe.Academies.External.Web/CypressTests/cypress/support/e2e.ts',
    specPattern: 'Dfe.Academies.External.Web/CypressTests/cypress/e2e',
    experimentalOriginDependencies: true,
    ChromeWebSecurity: false,
    waitForAnimations: true,
    waitForTransition: true,
    setupNodeEvents(on, config) {
      // implement node event listeners here
      on('before:run', (details) => {
        // Map .env variables to Cypress env
        config.env = process.env;
     });
   },
   },
 });