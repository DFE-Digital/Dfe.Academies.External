class WhichTrustIsSchoolJoining {
  public whichTrustIsSchoolJoiningElementsVisible(): this {
    cy.whichTrustIsSchoolJoiningElementsVisible()

    return this
  }

  public selectConfirmAndSubmitTrust(): this {
    cy.selectTrustName()
    cy.get('#ConfirmSelection').click()
    cy.get('#btnAdd').click()

    return this
  }

  public changeTrustName(): this {
    cy.changeTrustName()

    return this
  }
}

const whichTrustIsSchoolJoining = new WhichTrustIsSchoolJoining()

export default whichTrustIsSchoolJoining
