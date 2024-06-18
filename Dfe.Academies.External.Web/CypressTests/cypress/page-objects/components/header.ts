class Header {
  public govUkHeaderVisible(): this {
    cy.get('.govuk-header__logotype').should('be.visible')

    return this
  }

  public applyToBecomeAnAcademyHeaderLinkVisible(): this {
    cy.get('.govuk-header__content').contains('Apply to become an Academy')

    return this
  }
}

const header = new Header()

export default header
