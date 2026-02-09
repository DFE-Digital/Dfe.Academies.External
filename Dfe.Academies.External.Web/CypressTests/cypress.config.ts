import { defineConfig } from 'cypress'
import { generateZapReport } from './cypress/plugins/generateZapReport'

export default defineConfig({
	watchForFileChanges: false,
  reporter: 'cypress-multi-reporters', // added this for slack messaging plugin
  reporterOptions: {
    reporterEnabled: 'mochawesome',
    mochawesomeReporterOptions: {
      reportDir: 'cypress/reports/mocha', // added this for slack messaging plugin
      quiet: true, 
      overwrite: false,
      html: false,
      json: true,
    },
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


		on("task", {
			log(message) {
				console.log(message);

				return null;
			},
		});

      on('after:run', async () => {
        if (process.env.ZAP) {
          await generateZapReport()
        }
      })
    },
  },
})
