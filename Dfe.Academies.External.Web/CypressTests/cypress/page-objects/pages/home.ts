class Home {
  public start(): this {
    cy.get('[data-cy="startNowButton"]').click()

    return this
  }

  public warningIcon(): this {
    cy.get('span[class="govuk-warning-text__icon"]')

    return this
  }

  public warningText(): this {
    cy.get('strong[class="govuk-warning-text__text"]')

    return this
  }
}

const home = new Home()

export default home
