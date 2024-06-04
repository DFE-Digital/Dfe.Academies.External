class LocalGovernanceArrangements {
  public localGovernanceArrangementsElementsVisible(): this {
    cy.localGovernanceArrangementsElementsVisible()

    return this
  }

  public localGovernanceArrangementsClickYes(): this {
    cy.get('#changesToLaGovernanceYes').click()

    return this
  }

  public enterlocalGovernanceArrangementsChanges(): this {
    cy.enterlocalGovernanceArrangementsChanges()

    return this
  }

  public localGovernanceArrangementsSubmit(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const localGovernanceArrangements = new LocalGovernanceArrangements()

export default localGovernanceArrangements
