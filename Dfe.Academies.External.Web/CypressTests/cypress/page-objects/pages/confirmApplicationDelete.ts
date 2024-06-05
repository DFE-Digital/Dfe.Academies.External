class ConfirmApplicationDelete {
  public checkAppIDIsCorrectAndselectConfirmDelete(applicationId: string): this {
    cy.log(`Global Application ID = ${applicationId}`)

    cy.get('.govuk-caption-xl').contains(`Application reference: A2B_${applicationId}`)

    cy.get('#deleteButton').should('be.visible').contains('Yes, delete')
    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('No, take me back')

    cy.get('#deleteButton').should('be.visible').contains('Yes, delete').click()

    return this
  }
}

const confirmApplicationDelete = new ConfirmApplicationDelete()

export default confirmApplicationDelete
