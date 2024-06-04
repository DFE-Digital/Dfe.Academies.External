class Declaration {
  public declarationElementsVisible(): this {
    cy.declarationElementsVisible()

    return this
  }

  public selectAgreementsVerifyAndSubmit(): this {
    cy.selectAgreements()
    cy.verifyAgreementsSelected()
    cy.get('input[type=submit]').click()

    return this
  }
}

const declaration = new Declaration()

export default declaration
