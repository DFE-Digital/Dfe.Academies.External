class DeclarationSummary {
  public declarationSummaryElementsVisible(): this {
    cy.declarationSummaryElementsVisible()

    return this
  }

  public declarationStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public declarationSummaryCompleteElementsVisible(): this {
    cy.declarationSummaryCompleteElementsVisible()

    return this
  }

  public submitDeclarationSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const declarationSummary = new DeclarationSummary()

export default declarationSummary
