class KeyPeopleSummary {
  public selectAddKeyPerson(): this {
    cy.contains('Add a key person').click()

    return this
  }

  public checkKeyPeopleSummaryCompleted(keyPersonName: string): this {
    cy.get('[data-cy="keyPersonNameRole"]').eq(0).contains(`${keyPersonName} (Financial director)`)
    cy.get('[data-cy="keyPersonInformation"]').contains('15/10/1980')

    cy.get('.govuk-button').eq(1).should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const keyPeopleSummary = new KeyPeopleSummary()

export default keyPeopleSummary
