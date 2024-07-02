class KeyPeopleSummary {
  public selectAddKeyPerson(): this {
    cy.contains('Add a key person').click()

    return this
  }

  // TODO get better selector for elements
  public checkKeyPeopleSummaryCompleted(keyPersonName: string): this {
    cy.get('.govuk-heading-s').eq(0).contains(`${keyPersonName} (Financial director)`)
    cy.get('.app-task-list__item').contains('15/10/1980')

    cy.get('.govuk-button').eq(1).should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const keyPeopleSummary = new KeyPeopleSummary()

export default keyPeopleSummary
