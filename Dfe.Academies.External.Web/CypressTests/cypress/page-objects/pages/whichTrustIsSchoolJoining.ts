class WhichTrustIsSchoolJoining {
  public selectTrustName(trustName: string): this {
    cy.get('[data-cy="trustSearchBox"]').click()
    cy.get('[data-cy="trustSearchBox"]').type(trustName)
    // Select the first item in the list
    cy.get('#SearchQueryInput__option--0').click()
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()

    return this
  }
}

const whichTrustIsSchoolJoining = new WhichTrustIsSchoolJoining()

export default whichTrustIsSchoolJoining
