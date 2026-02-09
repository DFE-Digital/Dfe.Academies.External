import {Logger} from "../common/logger";

Cypress.Commands.add('executeAccessibilityTests', () => {
  const wcagStandards = [
    'wcag2a',
    'wcag2aa',
    'wcag21a',
    'wcag21aa',
    'wcag22aa',
  ]
  const impactLevel = ['critical', 'minor', 'moderate', 'serious']
  const continueOnFail = true
  Logger.log("Injecting Axe and checking accessibility");

  // Temporarily increase timeout so axe.run() has time to complete on complex pages
  // (see https://github.com/component-driven/cypress-axe/issues/160)
  const prevTimeout = Cypress.config('defaultCommandTimeout')
  Cypress.config('defaultCommandTimeout', 15000)

  cy.injectAxe()
  cy.checkA11y(
    null,
    {
      retries: 3,
      runOnly: {
        type: 'tag',
        values: wcagStandards,
      },
      includedImpacts: impactLevel,
    },
    (violations) => {
      cy.url().then((url) => {
        violations.forEach((violation) => {
          const nodes = violation.nodes.map((node) => node.target.join(', ')).join('\n    ')
          const message =
            `\n[${violation.impact?.toUpperCase()}] ${violation.id}: ${violation.description}\n` +
            `  Help: ${violation.helpUrl}\n` +
            `  Affected nodes:\n    ${nodes}`
          Logger.log(`A11y violation on ${url}: ${message}`)
        })
      })
    },
    continueOnFail,
  )

  // Restore the original timeout
  cy.then(() => Cypress.config('defaultCommandTimeout', prevTimeout))
})
