class DeclarationSummary {
  public startDeclaration(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  // TODO all of these elements require proper Cypress tags
  public checkDeclarationSummaryCompleted(): this {
    cy.get('p').eq(2).contains('Yes')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const declarationSummary = new DeclarationSummary()

export default declarationSummary
