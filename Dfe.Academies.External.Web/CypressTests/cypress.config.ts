const { defineConfig } = require ('cypress')
require("dotenv").config(); // Load the .env file
import { generateZapReport } from './cypress/plugins/generateZapReport'

module.exports = defineConfig({
  env: {
    URL: process.env.URL,
    LOGIN_USERNAME: process.env.LOGIN_USERNAME,
    LOGIN_PASSWORD: process.env.LOGIN_PASSWORD,
  },
  e2e: {
    supportFile: 'cypress/support/e2e.ts',
    specPattern: 'cypress/e2e',
    experimentalOriginDependencies: true,
    ChromeWebSecurity: false,
    waitForAnimations: true,
    waitForTransition: true,
    video: false,
    setupNodeEvents(on, config) {
      // implement node event listeners here
      on('before:run', (details) => {
        // Map .env variables to Cypress env
        config.env = process.env;
     });

     on('after:run', async () => {
      if(process.env.zap) {
        await generateZapReport()
      }
     })
   },
   },
 });
