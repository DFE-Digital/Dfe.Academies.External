class WhichTrustIsSchoolJoining {
  // TODO get better selectors
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
