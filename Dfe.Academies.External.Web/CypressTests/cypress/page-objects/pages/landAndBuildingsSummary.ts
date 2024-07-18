class LandAndBuildingsSummary {
  public startLandAndBuildings(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public checkLandAndBuildingsSummaryCompleted(): this {
    cy.get('[data-cy="response"]').eq(0).contains('Land owners')

    cy.get('[data-cy="response"]').eq(1).contains('No')

    cy.get('[data-cy="response"]').eq(2).contains('No')

    cy.get('[data-cy="response"]').eq(3).contains('No')

    cy.get('[data-cy="response"]').eq(4).contains('No')

    cy.get('[data-cy="response"]').eq(5).contains('No')

    cy.get('[data-cy="response"]').eq(6).contains('No')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const landAndBuildingsSummary = new LandAndBuildingsSummary()

export default landAndBuildingsSummary
