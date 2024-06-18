class Home {
  public clickStartNow(): this {
    cy.get('.govuk-grid-column-two-thirds > .govuk-button').click()

    return this
  }
}

const home = new Home()

export default home
