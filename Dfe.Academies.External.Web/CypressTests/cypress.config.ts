import { defineConfig } from 'cypress'
import * as dotenv from 'dotenv'
import { generateZapReport } from './cypress/plugins/generateZapReport'

dotenv.config()

export default defineConfig({
  env: {
    URL: process.env.URL,
    LOGIN_USERNAME: process.env.LOGIN_USERNAME,
    LOGIN_PASSWORD: process.env.LOGIN_PASSWORD,
    SIGNIN_URL: process.env.SIGNIN_URL,
  },
  video: false,
  userAgent: 'DfEAcademiesExternal/1.0 Cypress',
  e2e: {
    supportFile: 'cypress/support/e2e.ts',
    specPattern: 'cypress/e2e',
    experimentalOriginDependencies: true,
    chromeWebSecurity: false,
    waitForAnimations: true,
    video: false,
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    setupNodeEvents(on, config) {
      // implement node event listeners here

      on('after:run', async () => {
        if (process.env.ZAP) {
          await generateZapReport()
        }
      })
    },
  },
})
