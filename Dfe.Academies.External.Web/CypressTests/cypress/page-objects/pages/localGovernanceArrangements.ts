class LocalGovernanceArrangements {
  public addGovernanceArragements(): this {
    cy.get('#changesToLaGovernanceYes').click()

    cy.get('#changesToLaGovernanceYes').click()
    cy.get('#ChangesToLaGovernanceExplained').click()
    cy.get('#ChangesToLaGovernanceExplained').type('What are the changes?')

    cy.get('input[type=submit]').click()
    return this
  }
}

const localGovernanceArrangements = new LocalGovernanceArrangements()

export default localGovernanceArrangements
