class TrustDetailsSummary {
  public JAMTrustDetailsSummaryElementsVisible(): this {
    cy.JAMTrustDetailsSummaryElementsVisible()

    return this
  }

  public JAMTrustDetailsSummarySelectStartSection(): this {
    cy.get('a[class=govuk-button govuk-button--secondary]').click()

    return this
  }

  public JAMTrustDetailsSummarySaveAndReturnToApp(): this {
    cy.JAMTrustDetailsSummarySaveAndReturnToApp()

    return this
  }
}

const trustDetailsSummary = new TrustDetailsSummary()

export default trustDetailsSummary
