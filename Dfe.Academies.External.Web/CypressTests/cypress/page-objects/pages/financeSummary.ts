class FinanceSummary {
  // TODO get better selector for element
  public startPreviousFinancialYear(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').eq(0).click()

    return this
  }

  // TODO get better selectors for elements
  public financeSummaryCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Finances')

    cy.get('.govuk-heading-m').eq(0).contains('Previous financial year')
    cy.get('.govuk-link').eq(1).contains('Change your answers')

    cy.get('b').eq(0).contains('End of previous financial year end date?')
    cy.get('.govuk-body').eq(1).contains('31/03/2022')

    cy.get('b').eq(1).contains('Revenue carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(3).contains('4999.99')

    cy.get('b').eq(2).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(5).contains('Surplus')

    cy.get('b').eq(3).contains('Capital carry forward at end of the previous financial year (31 March)')
    cy.get('.govuk-body').eq(7).contains('4998.98')

    cy.get('b').eq(4).contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(9).contains('Surplus')

    cy.get('.govuk-heading-m').eq(1).contains('Current financial year')
    cy.get('.govuk-link').eq(2).contains('Change your answers')

    cy.get('b').eq(5).contains('End of current financial year end date?')
    cy.get('.govuk-body').eq(11).contains('31/03/2023')

    cy.get('b').eq(6).contains('Revenue carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(13).contains('99999.99')
    cy.get('hr').eq(8).should('be.visible')

    cy.get('b').eq(7).contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(15).contains('Deficit')

    cy.get('b').contains('Capital carry forward at end of the current financial year (31 March)')
    cy.get('.govuk-body').eq(17).contains('A) plain the reason for the deficit, how the school plan to deal with it, and the recovery plan. Provide details of the financial forecast and/or the deficit recovery plan agreed with the local author')

    cy.get('b').contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(21).contains('Deficit')

    cy.get('.govuk-heading-m').eq(2).contains('Next financial year')
    cy.get('.govuk-link').eq(3).contains('Change your answers')

    cy.get('b').contains('End of next financial year end date?')
    cy.get('.govuk-body').eq(25).contains('31/03/2024')

    cy.get('b').contains('Revenue carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(27).contains('199999.99')

    cy.get('b').contains('Surplus or Deficit?')
    cy.get('.govuk-body').eq(29).contains('Deficit')

    cy.get('b').contains('Capital carry forward at end of the next financial year (31 March)')
    cy.get('.govuk-body').eq(33).contains('199998.98')

    cy.get('b').contains('Surplus or Deficit')
    cy.get('.govuk-body').eq(35).contains('Deficit')

    cy.get('.govuk-heading-m').eq(3).contains('Loans')
    cy.get('.govuk-link').eq(4).contains('Change your answers')

    cy.get('b').contains('Are there any existing loans?')
    cy.get('.govuk-body').eq(39).contains('No')

    cy.get('.govuk-heading-m').eq(4).contains('Leases')
    cy.get('.govuk-link').eq(5).contains('Change your answers')

    cy.get('b').contains('Are there any existing leases?')
    cy.get('.govuk-body').eq(41).contains('No')

    cy.get('.govuk-heading-m').eq(5).contains('Financial investigations')
    cy.get('.govuk-link').eq(6).contains('Change your answers')

    cy.get('b').contains('Finance ongoing investigations?')
    cy.get('.govuk-body').eq(43).contains('No')

    cy.get('.govuk-button').should('be.visible').contains('Back')

    return this
  }

  // TODO get better selector for element
  public submitFinanceSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const financeSummary = new FinanceSummary()

export default financeSummary
