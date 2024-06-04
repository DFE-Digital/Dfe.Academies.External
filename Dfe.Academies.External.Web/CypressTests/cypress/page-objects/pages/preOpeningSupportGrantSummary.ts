class PreOpeningSupportGrantSummary {
  public preopeningSupportGrantSummaryElementsVisible(): this {
    cy.preopeningSupportGrantSummaryElementsVisible()

    return this
  }

  public selectPreopeningSupportGrantStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public preopeningSupportGrantSummaryCompleteElementsVisible(): this {
    cy.preopeningSupportGrantSummaryCompleteElementsVisible()

    return this
  }

  public submitPreopeningSupportGrantSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const preOpeningSupportGrantSummary = new PreOpeningSupportGrantSummary()

export default preOpeningSupportGrantSummary
