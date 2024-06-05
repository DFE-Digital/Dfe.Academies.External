class ReasonsForJoining {
  public reasonsForJoiningElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Conversion key details')
    cy.get('.govuk-heading-l').contains('Reasons for joining')

    cy.get('.govuk-label').contains('Why does the school want to join this trust in particular?')

    cy.get('#ApplicationJoinTrustReason').should('be.visible')

    cy.get('input[type=submit]').should('be.visible').contains('Save and continue')

    return this
  }

  public reasonsForJoiningInputAndSubmit(): this {
    const reasonsForJoining = 'Why does the school want to join this trust in particular? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.'
    cy.get('#ApplicationJoinTrustReason').type(reasonsForJoining)

    cy.get('input[type=submit]').click()

    return this
  }
}

const reasonsForJoining = new ReasonsForJoining()

export default reasonsForJoining
