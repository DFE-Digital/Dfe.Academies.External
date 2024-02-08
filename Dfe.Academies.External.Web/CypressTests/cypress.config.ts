import { defineConfig } from 'cypress'
import * as dotenv from "dotenv";
import { generateZapReport } from './cypress/plugins/generateZapReport'

dotenv.config();

export default defineConfig({
  env: {
    URL: process.env.URL,
    LOGIN_USERNAME: process.env.LOGIN_USERNAME,
    LOGIN_PASSWORD: process.env.LOGIN_PASSWORD,
  },
	userAgent: 'DfEAcademiesExternal/1.0 Cypress',
  e2e: {
    supportFile: 'cypress/support/e2e.ts',
    specPattern: 'cypress/e2e',
    experimentalOriginDependencies: true,
    chromeWebSecurity: false,
    waitForAnimations: true,
    video: false,
    setupNodeEvents(on, config) {
      // implement node event listeners here
      on('before:run', () => {
        // Map .env variables to Cypress env
        config.env = process.env;
     });

     on('after:run', async () => {
      if(process.env.ZAP) {
        await generateZapReport()
      }
     })
   },
   },
 });
