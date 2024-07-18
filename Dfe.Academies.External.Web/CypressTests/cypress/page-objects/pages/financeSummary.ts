class FinanceSummary {
  public startPreviousFinancialYear(): this {
    cy.get('[data-cy="startSectionButton"]').eq(0).click()

    return this
  }

  public checkFinanceSummaryCompleted(): this {
    cy.get('[data-cy="response"]').eq(0).contains('31/03/2022')

    cy.get('[data-cy="response"]').eq(1).contains('4999.99')

    cy.get('[data-cy="response"]').eq(2).contains('Surplus')

    cy.get('[data-cy="response"]').eq(3).contains('4998.98')

    cy.get('[data-cy="response"]').eq(4).contains('Surplus')

    cy.get('[data-cy="response"]').eq(5).contains('31/03/2023')

    cy.get('[data-cy="response"]').eq(6).contains('99999.99')

    cy.get('[data-cy="response"]').eq(7).contains('Deficit')

    cy.get('[data-cy="response"]').eq(8).contains('99998.98')

    cy.get('[data-cy="response"]').eq(9).contains('Deficit')

    cy.get('[data-cy="response"]').eq(10).contains('31/03/2024')

    cy.get('[data-cy="response"]').eq(11).contains('199999.99')

    cy.get('[data-cy="response"]').eq(12).contains('Deficit')

    cy.get('[data-cy="response"]').eq(13).contains('199998.98')

    cy.get('[data-cy="response"]').eq(14).contains('Deficit')

    cy.get('[data-cy="response"]').eq(15).contains('No')

    cy.get('[data-cy="response"]').eq(16).contains('No')

    cy.get('[data-cy="response"]').eq(17).contains('No')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const financeSummary = new FinanceSummary()

export default financeSummary
