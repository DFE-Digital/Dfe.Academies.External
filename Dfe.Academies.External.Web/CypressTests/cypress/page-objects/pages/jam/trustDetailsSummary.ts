class TrustDetailsSummary {
  public JAMTrustDetailsSummarySelectStartSection(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  public JAMTrustDetailsSummarySaveAndReturnToApp(applicationId: string): this {
    cy.url().then((url) => {
      applicationId = url.substring(
        url.indexOf('=') + 1,
        url.lastIndexOf('&'),
      )
      cy.get(`a[href="/application-overview?appId=${applicationId}"]`).eq(1).click()
    })

    return this
  }
}

const trustDetailsSummary = new TrustDetailsSummary()

export default trustDetailsSummary
