class FinanceSummary {
  // TODO get better selector for element
  public startPreviousFinancialYear(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).click()

    return this
  }

  // TODO get better selectors for elements
  public checkFinanceSummaryCompleted(): this {
    cy.get('.govuk-body').eq(1).contains('31/03/2022')

    cy.get('.govuk-body').eq(3).contains('4999.99')

    cy.get('.govuk-body').eq(5).contains('Surplus')

    cy.get('.govuk-body').eq(7).contains('4998.98')

    cy.get('.govuk-body').eq(9).contains('Surplus')

    cy.get('.govuk-link').eq(2).contains('Change your answers')

    cy.get('.govuk-body').eq(11).contains('31/03/2023')

    cy.get('.govuk-body').eq(13).contains('99999.99')

    cy.get('.govuk-body').eq(15).contains('Deficit')

    cy.get('.govuk-body').eq(17).contains('Reason for the revenue deficit')

    cy.get('.govuk-body').eq(21).contains('Deficit')

    cy.get('.govuk-body').eq(25).contains('31/03/2024')

    cy.get('.govuk-body').eq(27).contains('199999.99')

    cy.get('.govuk-body').eq(29).contains('Deficit')

    cy.get('.govuk-body').eq(33).contains('199998.98')

    cy.get('.govuk-body').eq(35).contains('Deficit')

    cy.get('.govuk-body').eq(39).contains('No')

    cy.get('.govuk-body').eq(41).contains('No')

    cy.get('.govuk-body').eq(43).contains('No')

    return this
  }

  // TODO get better selector for element
  public saveAndReturnToApp(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const financeSummary = new FinanceSummary()

export default financeSummary
