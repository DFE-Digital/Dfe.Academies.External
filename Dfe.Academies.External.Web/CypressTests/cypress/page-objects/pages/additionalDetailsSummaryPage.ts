class AdditionalDetailsSummaryPage {
  public startAdditionalDetails(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  // TODO all of these elements require proper Cypress tags
  public checkAdditionalDetails(): this {
    cy.get('p').eq(2).contains('What will the school bring to the trust they are joining?')

    cy.get('p').eq(4).contains('No')

    cy.get('p').eq(6).contains('No')

    cy.get('p').eq(8).contains('No')

    cy.get('p').eq(10).contains('No')

    cy.get('p').eq(12).contains('Yes')

    cy.get('p').eq(14).contains('No')

    cy.get('p').eq(16).contains('Yes')

    cy.get('p').eq(18).contains('No')

    cy.get('p').eq(20).contains('Please provide a list of your main feeder schools')

    cy.get('p').eq(22).contains('fiftyk.pdf')

    cy.get('p').eq(24).contains('No')

    cy.get('p').eq(26).contains('No')

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
