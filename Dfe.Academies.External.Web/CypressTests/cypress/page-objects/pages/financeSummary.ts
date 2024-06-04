class FinanceSummary {
  public financeSummaryNotStartedElementsVisible(): this {
    cy.financeSummaryNotStartedElementsVisible()

    return this
  }

  public selectPreviousFinancialYrStartSection(): this {
    cy.get('a[class=govuk-button govuk-button--secondary]').eq(0).click()

    return this
  }

  public selectCurrentFinancialYrStartSection(): this {
    cy.get('a[class=govuk-button govuk-button--secondary]').eq(1).click()

    return this
  }

  public selectNextFinancialYrStartSection(): this {
    cy.selectNextFinancialYrStartSection()

    return this
  }

  public selectLoansStartSection(): this {
    cy.selectLoansStartSection()

    return this
  }

  public selectLeasesStartSection(): this {
    cy.selectLeasesStartSection()

    return this
  }

  public selectFinancialInvestigationsStartSection(): this {
    cy.selectFinancialInvestigationsStartSection()

    return this
  }

  public financeSummaryCompleteElementsVisible(): this {
    cy.financeSummaryCompleteElementsVisible()

    return this
  }

  public submitFinanceSummary(): this {
    cy.submitFinanceSummary()

    return this
  }
}

const financeSummary = new FinanceSummary()

export default financeSummary
