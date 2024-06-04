class TrustName {
  public FAMEnterTrustnameAndSubmit(): this {
    cy.get('#ProposedNameOfTrust').click()
    cy.get('#ProposedNameOfTrust').type('Plymouth')
    cy.get('input[type=submit]').click()

    return this
  }
}

const trustName = new TrustName()

export default trustName
