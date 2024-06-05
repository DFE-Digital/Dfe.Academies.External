class WhichTrustIsSchoolJoining {
  public whichTrustIsSchoolJoiningElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-heading-m').contains('Trust details')
    cy.get('h2').eq(1).contains('Which trust is the school joining?')
    cy.get('label').contains('Enter the name of the trust, its Companies House number, or its group Id')
    cy.get('#SearchQueryInput').should('exist')
    cy.get('#btnAdd').should('be.visible').contains('Save and continue')

    return this
  }

  public selectConfirmAndSubmitTrust(trustName: string): this {
    cy.get('.autocomplete__wrapper > #SearchQueryInput').click()
    cy.get('.autocomplete__wrapper > #SearchQueryInput').type(trustName)
    cy.get('#SearchQueryInput__option--0').click()
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()

    return this
  }
}

const whichTrustIsSchoolJoining = new WhichTrustIsSchoolJoining()

export default whichTrustIsSchoolJoining
