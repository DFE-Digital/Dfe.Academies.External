class LocalGovernanceArrangements {
  public localGovernanceArrangementsClickYes(): this {
    cy.get('#changesToLaGovernanceYes').click()

    return this
  }

  public enterlocalGovernanceArrangementsChanges(): this {
    const localGovernanceArrangements = 'What are the changes and how do they fit into the current lines of accountability in the trust? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et d'
    cy.get('#changesToLaGovernanceYes').click()
    cy.get('#ChangesToLaGovernanceExplained').click()
    cy.get('#ChangesToLaGovernanceExplained').type(localGovernanceArrangements)

    return this
  }

  public localGovernanceArrangementsSubmit(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const localGovernanceArrangements = new LocalGovernanceArrangements()

export default localGovernanceArrangements
