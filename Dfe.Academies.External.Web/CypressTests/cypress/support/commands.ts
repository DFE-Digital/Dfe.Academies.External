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
    null,
    continueOnFail,
  )
})
