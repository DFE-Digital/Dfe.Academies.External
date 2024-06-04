class AdditionalDetailsSummaryPage {
  public additionalDetailsSummaryNotStartedElementsVisible(): this {
    cy.additionalDetailsSummaryNotStartedElementsVisible()

    return this
  }

  public selectAdditionalDetailsStartSection(): this {
    cy.get('a[class=govuk-button govuk-button--secondary]').click()

    return this
  }

  public additionalDetailsSummaryCompleteElementsVisible(): this {
    cy.additionalDetailsSummaryCompleteElementsVisible()

    return this
  }

  public submitAdditionalDetailsSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const additionalDetailsSummaryPage = new AdditionalDetailsSummaryPage()

export default additionalDetailsSummaryPage
