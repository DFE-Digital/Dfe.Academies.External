class Home {
  public start(): this {
    cy.get('[data-cy="startNowButton"]').click()

    return this
  }

}

const home = new Home()

export default home
