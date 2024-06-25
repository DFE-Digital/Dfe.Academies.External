class GovernanceStructureSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  // TODO get better selector for element
  // TODO fix commented line
  public FAMGovernanceStructureSummaryCompleteElementsVisibleAndSubmit(): this {
    cy.get('.govuk-link').eq(1).contains('Change your answers')
    // cy.get('p').eq(2).contains('fiftyk.docx')
    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const governanceStructureSummary = new GovernanceStructureSummary()

export default governanceStructureSummary
