class LandAndBuildingsSummary {
  public landAndBuildingsSummaryElementsVisible(): this {
    cy.landAndBuildingsSummaryElementsVisible()

    return this
  }

  public selectLandAndBuildingsStartSection(): this {
    cy.selectLandAndBuildingsStartSection()

    return this
  }

  public landAndBuildingsSummaryCompleteElementsVisible(): this {
    cy.landAndBuildingsSummaryCompleteElementsVisible()

    return this
  }

  public submitLandAndBuildingsSummary(): this {
    cy.submitLandAndBuildingsSummary()

    return this
  }
}

const landAndBuildingsSummary = new LandAndBuildingsSummary()

export default landAndBuildingsSummary
