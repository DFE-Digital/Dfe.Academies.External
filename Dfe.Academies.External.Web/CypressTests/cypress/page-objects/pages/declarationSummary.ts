class DeclarationSummary {
  public startDeclaration(): this {
    cy.contains('Start section').click()

    return this
  }

  // TODO fix commented lines
  // TODO all of these elements require proper Cypress tags
  public checkDeclarationSummaryCompleted(): this {
    // cy.get('p').eq(2).contains('Yes')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const declarationSummary = new DeclarationSummary()

export default declarationSummary
