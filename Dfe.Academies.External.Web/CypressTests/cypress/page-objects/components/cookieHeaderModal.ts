class CookieHeaderModal {
  public acceptAnalyticsCookies(): this {
    cy.get('[value=accept]').click()

    return this
  }
}

const cookieHeaderModal = new CookieHeaderModal()

export default cookieHeaderModal
