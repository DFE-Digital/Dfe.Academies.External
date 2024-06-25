class Home {
  public start(): this {
    // TODO get proper selector for this element
    cy.get('a').contains('Start now').click()

    return this
  }
}

const home = new Home()

export default home
