Cypress.Commands.add('excuteAccessibilityTests', () => {
  const wcagStandards = ['wcag22aa', 'wcag21aa']
  const impactLevel = ['critical', 'minor', 'moderate', 'serious']
  const continueOnFail = false
  cy.injectAxe()
  cy.checkA11y(
    null,
    {
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
