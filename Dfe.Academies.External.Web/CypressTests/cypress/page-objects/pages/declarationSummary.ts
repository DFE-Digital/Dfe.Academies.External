class DeclarationSummary {
  public startDeclaration(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public checkDeclarationSummaryCompleted(): this {
    cy.get('[data-cy="response"]').contains('Yes')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const declarationSummary = new DeclarationSummary()

export default declarationSummary
