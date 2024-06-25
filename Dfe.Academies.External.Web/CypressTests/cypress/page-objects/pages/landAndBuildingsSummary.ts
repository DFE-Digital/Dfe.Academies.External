class LandAndBuildingsSummary {
  public startLandAndBuildings(): this {
    cy.contains('Start section').click()

    return this
  }

  // TODO all of these elements require proper Cypress tags
  public checkLandAndBuildingsSummaryCompleted(): this {
    cy.get('p').eq(2).contains('As far as you\'re aware, who owns or holds the school\'s buildings and land?')

    cy.get('p').eq(4).contains('No')

    cy.get('p').eq(6).contains('No')

    cy.get('p').eq(8).contains('No')

    cy.get('p').eq(10).contains('No')

    cy.get('p').eq(12).contains('No')

    cy.get('p').eq(14).contains('No')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const landAndBuildingsSummary = new LandAndBuildingsSummary()

export default landAndBuildingsSummary
