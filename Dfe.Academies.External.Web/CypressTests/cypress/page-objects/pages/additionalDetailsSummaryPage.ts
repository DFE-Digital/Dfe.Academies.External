class AdditionalDetailsSummaryPage {
  public startAdditionalDetails(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public checkAdditionalDetails(): this {
    cy.get('[data-cy="response"]').eq(0).contains('What will the school bring to the trust they are joining?')

    cy.get('[data-cy="response"]').eq(1).contains('No')

    cy.get('[data-cy="response"]').eq(2).contains('No')

    cy.get('[data-cy="response"]').eq(3).contains('No')

    cy.get('[data-cy="response"]').eq(4).contains('No')

    cy.get('[data-cy="response"]').eq(5).contains('Yes')

    cy.get('[data-cy="response"]').eq(6).contains('No')

    cy.get('[data-cy="response"]').eq(7).contains('Yes')

    cy.get('[data-cy="response"]').eq(8).contains('No')

    cy.get('[data-cy="response"]').eq(9).contains('Please provide a list of your main feeder schools')

    cy.get('[data-cy="response"]').eq(10).contains('fiftyk.pdf')

    cy.get('[data-cy="response"]').eq(11).contains('No')

    cy.get('[data-cy="response"]').eq(12).contains('No')

    cy.get('.govuk-button').should('be.visible').contains('Back')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const additionalDetailsSummaryPage = new AdditionalDetailsSummaryPage()

export default additionalDetailsSummaryPage
