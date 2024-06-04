class CookieHeaderModal {
  public clickAcceptAnalyticsCookies(): this {
    cy.get('[value=accept]').click()

    return this
  }

  public clickRejectAnalyticsCookies(): this {
    cy.get('[value=reject]').click()

    return this
  }

  public clickViewCookies(): this {
    cy.get('.govuk-button-group > .govuk-link').click()

    return this
  }
}

const cookieHeaderModal = new CookieHeaderModal()

export default cookieHeaderModal
